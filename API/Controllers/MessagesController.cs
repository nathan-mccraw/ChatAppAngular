using API.Hub;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InputValidationModels;
using Microsoft.AspNetCore.Authorization;
using Core.Entities;
using AutoMapper;
using Core.DTOs;
using Infrastructure.Specifications;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;
        private readonly IGenericRepository<MessageEntity> _messageRepo;
        private readonly IGenericRepository<UserEntity> _userRepo;
        private readonly IGenericRepository<UserSessionEntity> _sessionRepo;
        private readonly IGenericRepository<ChannelEntity> _channelRepo;
        private readonly IMapper _mapper;

        public MessagesController(IGenericRepository<MessageEntity> genRepo,
                                  IGenericRepository<UserEntity> userRepo,
                                  IGenericRepository<UserSessionEntity> sessionRepo,
                                  IGenericRepository<ChannelEntity> channelRepo,
                                  IHubContext<ChatHub,
                                  IChatClient> chatHub,
                                  IMapper mapper)
        {
            _messageRepo = genRepo;
            _userRepo = userRepo;
            _sessionRepo = sessionRepo;
            _channelRepo = channelRepo;
            _chatHub = chatHub;
            _mapper = mapper;
        }

        // GET: api/messages
        [HttpGet]
        public ActionResult<IReadOnlyList<MessageModel>> GetMessages()
        {
            var spec = new GetMessagesNotDeleted();
            var messages = _messageRepo.GetEntitiesWithSpec(spec);
            return Ok(_mapper.Map<IReadOnlyList<MessageEntity>, IReadOnlyList<MessageModel>>(messages));
        }

        // GET api/Messages/5
        [HttpGet("{id}")]
        public ActionResult<MessageModel> GetMessageById(int id)
        {
            var message = _messageRepo.GetEntityByIdFromDB(id);
            return Ok(_mapper.Map<MessageEntity, MessageModel>(message));
        }

        //POST api/Messages
        [HttpPost]
        public ActionResult PostMessage(IncomingMessageModel inputMessage)
        {
            var storedUserEntity = _userRepo.GetEntityByIdFromDB(inputMessage.User.UserId);
            var activeSessions = storedUserEntity.UserSessions.Where(session => session.isActive);

            if (storedUserEntity == null)
            {
                return Unauthorized("Please signin to post a message");
            }
            else if (!activeSessions.Any())
            {
                return Unauthorized("You have been signed out due to inactivity. Please sign back in to post a message.");
            }

            if (activeSessions.Where(session => session.UserToken == inputMessage.User.UserToken).Any())
            {
                foreach (var sess in activeSessions)
                {
                    if (sess.Id != inputMessage.User.Id)
                    {
                        sess.TokenExpirationDate = DateTime.UtcNow;
                        sess.LastActive = DateTime.UtcNow;
                    }
                    else
                    {
                        sess.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                        sess.LastActive = DateTime.UtcNow;
                    }
                    _sessionRepo.UpdateEntityInDB(sess);
                }

                //get ChannelEntity (put channel validation here)
                var channel = _channelRepo.GetEntityByIdFromDB(inputMessage.ChannelId);

                //make new messageEntity to post in DB
                MessageEntity message = new() { Text = inputMessage.Text, Channel = channel, User = storedUserEntity };

                //post message to DB
                _messageRepo.AddEntityToDB(message);

                //make new outgoing message model
                MessageModel outgoingMessage = _mapper.Map<MessageEntity, MessageModel>(message);

                //broadcast to other clients a new message has been posted
                _chatHub.Clients.All.ReceiveMessage(outgoingMessage);

                return Ok();
            }
            else
            {
                throw new ApplicationException("Unknown Error");
            }
        }

        // DELETE api/Messages/5
        [HttpDelete("{id}")]
        public ActionResult DeleteMessage(int id)
        {
            try
            {
                var message = _messageRepo.GetEntityByIdFromDB(id);
                message.DateDeleted = DateTime.Now;
                _messageRepo.UpdateEntityInDB(message);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}