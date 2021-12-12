using API.Authentication;
using API.Hub;
using Core.DTOs;
using Core.InputValidationModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;
        private readonly IMessageService _messageService;
        private readonly IJwtGenerator _jwtGen;

        public MessagesController(IUserService userService,
                                  IUserSessionService userSessionService,
                                  IMessageService messageService,
                                  IHubContext<ChatHub, IChatClient> chatHub,
                                  IJwtGenerator jwtGen)
        {
            _userService = userService;
            _userSessionService = userSessionService;
            _messageService = messageService;
            _chatHub = chatHub;
            _jwtGen = jwtGen;
        }

        //GET api/Messages/channelName
        [HttpGet("{channelName}")]
        public ActionResult<IReadOnlyList<MessageModel>> GetMessagesNotDeletedByChannel(string channelName)
        {
            try
            {
                return Ok(_messageService.GetMessagesNotDeletedByChannel(channelName));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST api/Messages
        [HttpPost]
        public ActionResult<UserSessionModel> PostMessage(IncomingMessageModel inputMessage)
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

            return Ok(_jwtGen.GenerateToken(updatedSession));
        }

        // DELETE api/Messages/5
        [HttpDelete("{id}")]
        public ActionResult<UserSessionModel> DeleteMessage([FromBody] MessageModel clientMessage, [FromHeader] UserSessionModel clientSession)
        {
            if (_userSessionService.IsValidSession(clientSession) == false)
            {
                return Unauthorized("Please signin and try again");
            }
            else if (_userService.IsUserDeleted(clientSession.UserId))
            {
                return Unauthorized("This account have been deleted");
            }

            var updatedSession = _userSessionService.UpdateSession(clientSession);
            updatedSession.HasOtherActiveSessions = _userService.HasOtherActiveSessions(clientSession.UserId);

            try
            {
                _messageService.DeleteMessage(clientMessage.Id);
                return Ok(_jwtGen.GenerateToken(updatedSession));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}