using Core.DTOs;
using Core.InputValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IMessageRepository
    {
        Task<List<MessageModel>> GetMessagesFromDB();

        MessageModel GetMessageByIdFromDB(int id);

        MessageModel PostMessageToDB(IncomingMessageModel incomingMessage);

        void DeleteMessageFromDB(int id);
    }
}