using API.Hub;
using Core.Interfaces;
using Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.InputValidationModels;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;
        private readonly IMessageRepository _repo;

        public MessagesController(IMessageRepository repo, IHubContext<ChatHub, IChatClient> chatHub)
        {
            _repo = repo;
            _chatHub = chatHub;
        }

        // GET: api/messages
        [HttpGet]
        public async Task<ActionResult<List<MessageModel>>> GetMessages()
        {
            var messages = await _repo.GetMessagesFromDB();
            return Ok(messages);
        }

        // GET api/Messages/5
        [HttpGet("{id}")]
        public ActionResult<MessageModel> GetMessageById(int id)
        {
            var message = _repo.GetMessageByIdFromDB(id);
            return Ok(message);
        }

        //POST api/Messages
        [HttpPost]
        public async Task PostMessage(IncomingMessageModel incomingMessage)
        {
            MessageModel outgoingMessage = await _repo.PostMessageToDB(incomingMessage);
            await _chatHub.Clients.All.ReceiveMessage(outgoingMessage);
        }

        // DELETE api/Messages/5
        [Authorize]
        [HttpDelete("{id}")]
        public void DeleteMessage(int id)
        {
            _repo.DeleteMessageFromDB(id);
        }
    }
}