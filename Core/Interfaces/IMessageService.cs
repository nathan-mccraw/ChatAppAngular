using Core.DTOs;
using Core.InputValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMessageService
    {
        public MessageModel CreateMessage(IncomingMessageModel inputMessage);

        public void DeleteMessage(int messageId);

        public IReadOnlyList<MessageModel> GetAllMessagesByChannel(string channelName);

        public MessageModel GetMessageById(int messageId);

        public MessageModel GetMessageIfNotDeletedById(int messageId);

        public IReadOnlyList<MessageModel> GetMessagesNotDeletedByChannel(string channelName);
    }
}