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
        private readonly IMapper _mapper;

        public MessagesController(IGenericRepository<MessageEntity> genRepo,
                                  IGenericRepository<UserEntity> userRepo,
                                  IHubContext<ChatHub,
                                  IChatClient> chatHub,
                                  IMapper mapper)
        {
            _messageRepo = genRepo;
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
            var spec = new GetUserEntityByUserTokenSpec(inputMessage.User.UserToken);
            var storedUserEntity = _userRepo.GetEntityWithSpec(spec);

            if (storedUserEntity == null || storedUserEntity.UserToken != inputMessage.User.UserToken)
            {
                return Unauthorized("Please signin to post a message");
            }
            else if (storedUserEntity.TokenExpirationDate < DateTime.UtcNow)
            {
                return Unauthorized("You have been signed out due to inactivity. Please sign back in to post a message.");
            }

            if (storedUserEntity.UserToken == inputMessage.User.UserToken
                && storedUserEntity.TokenExpirationDate > DateTime.UtcNow)
            {
                //update usersession with new expiration date
                storedUserEntity.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                storedUserEntity.LastActive = DateTime.UtcNow;

                //make new messageEntity to post in DB
                MessageEntity message = new() { Text = inputMessage.Text, User = storedUserEntity };

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
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteMessage(int id)
        {
            _messageRepo.DeleteEntityFromDB(id);
        }
    }
}