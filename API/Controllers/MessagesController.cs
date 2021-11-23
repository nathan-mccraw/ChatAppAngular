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

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;
        private readonly IGenericRepository<MessageEntity> _messageRepo;
        private readonly IGenericRepository<UserSessionEntity> _userSessRepo;
        private readonly IGenericRepository<UserEntity> _userRepo;
        private readonly IMapper _mapper;

        public MessagesController(IGenericRepository<MessageEntity> genRepo,
                                  IGenericRepository<UserSessionEntity> userSessRepo,
                                  IGenericRepository<UserEntity> userRepo,
                                  IHubContext<ChatHub,
                                  IChatClient> chatHub,
                                  IMapper mapper)
        {
            _messageRepo = genRepo;
            _userSessRepo = userSessRepo;
            _userRepo = userRepo;
            _chatHub = chatHub;
            _mapper = mapper;
        }

        // GET: api/messages
        [HttpGet]
        public ActionResult<IReadOnlyList<MessageModel>> GetMessages()
        {
            var messages = _messageRepo.GetAllEntitiesFromDB();
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
            var storedSession = _userSessRepo.GetEntityByIdFromDB(inputMessage.User.Id);

            if (storedSession == null)
            {
                return Unauthorized("Please signin to post a message");
            }

            if (storedSession.UserToken == inputMessage.User.UserToken
                && storedSession.TokenExpirationDate > DateTime.UtcNow)
            {
                //update usersession with new expiration date
                var updateUserSession = inputMessage.User;
                updateUserSession.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                var userSessionToAdd = _mapper.Map<UserSessionModel, UserSessionEntity>(updateUserSession);
                _userSessRepo.UpdateEntityInDB(userSessionToAdd);

                //make new messageEntity to post in DB
                MessageEntity message = new() { Text = inputMessage.Text, UserId = updateUserSession.UserId };

                //post message to DB
                _messageRepo.AddEntityToDB(message);

                //maek new outgoing message model
                MessageModel outgoingMessage = _mapper.Map<MessageEntity, MessageModel>(message);

                //broadcast to other clients a new message has been posted
                _chatHub.Clients.All.ReceiveMessage(outgoingMessage);

                return Ok();
            }
            else if (storedSession.TokenExpirationDate <= DateTime.UtcNow)
            {
                return Unauthorized("You have been logged out due to inactivity. Please signin again to post a message.");
            }
            else
            {
                throw new ApplicationException("Unknown Error");
            }
        }

        // DELETE api/Messages/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteMessage(int id)
        {
            _messageRepo.DeleteEntityFromDB(id);
        }
    }
}