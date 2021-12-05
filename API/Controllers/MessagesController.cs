using API.Hub;
using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using Core.Interfaces;
using Infrastructure.Specifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;
        private readonly IGenericRepository<MessageEntity> _messageRepo;
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IMessageService _messageService;
        private readonly IMapper _mapper;

        public MessagesController(IGenericRepository<MessageEntity> genRepo,
                                  IUserService userService,
                                  IUserSessionService userSessionService,
                                  IMessageService messageService,
                                  IHubContext<ChatHub,
                                  IChatClient> chatHub,
                                  IMapper mapper)
        {
            _messageRepo = genRepo;
            _userService = userService;
            _userSessionService = userSessionService;
            _messageService = messageService;
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
            if (_userSessionService.IsValidSession(inputMessage.UserSession) == false)
            {
                return Unauthorized("Please signin and try again");
            }
            else if (_userService.IsUserDeleted(inputMessage.UserSession.UserId))
            {
                return Unauthorized("This account have been deleted");
            }

            var updatedSession = _userSessionService.UpdateSession(inputMessage.UserSession);
            updatedSession.HasOtherActiveSessions = _userService.HasOtherActiveSessions(inputMessage.UserSession.UserId);

            //make new outgoing message model
            MessageModel outgoingMessage = _messageService.CreateMessage(inputMessage);

            //broadcast to other clients a new message has been posted
            _chatHub.Clients.All.ReceiveMessage(outgoingMessage);

            return Ok();
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