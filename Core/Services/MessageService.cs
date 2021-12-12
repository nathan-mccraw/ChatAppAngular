using AutoMapper;
using Core.DBSpecifications;
using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using Core.Interfaces;
using System;
using System.Collections.Generic;

namespace Core.Services
{
    public class MessageService : IMessageService
    {
        private readonly IGenericRepository<MessageEntity> _messageRepo;
        private readonly IGenericRepository<ChannelEntity> _channelRepo;
        private readonly IGenericRepository<UserEntity> _userRepo;
        private readonly IMapper _mapper;

        public MessageService(IGenericRepository<MessageEntity> messageRepo,
                              IGenericRepository<ChannelEntity> channelRepo,
                              IGenericRepository<UserEntity> userRepo,
                              IMapper mapper)
        {
            _messageRepo = messageRepo;
            _channelRepo = channelRepo;
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public MessageModel CreateMessage(IncomingMessageModel inputMessage)
        {
            var channel = _channelRepo.GetEntityByIdFromDB(inputMessage.ChannelId);
            var user = _userRepo.GetEntityByIdFromDB(inputMessage.UserSession.UserId);
            MessageEntity message = new() { Text = inputMessage.Text, Channel = channel, User = user };
            _messageRepo.AddEntityToDB(message);

            return (_mapper.Map<MessageEntity, MessageModel>(message));
        }

        public void DeleteMessage(int messageId)
        {
            var message = _messageRepo.GetEntityByIdFromDB(messageId);
            message.DateDeleted = DateTime.Now;
            _messageRepo.UpdateEntityInDB(message);
        }

        public IReadOnlyList<MessageModel> GetAllMessagesByChannel(string channelName)
        {
            var spec = new GetAllMessagesByChannelSpec(channelName);
            var messages = _messageRepo.GetEntitiesWithSpec(spec);
            return _mapper.Map<IReadOnlyList<MessageEntity>, IReadOnlyList<MessageModel>>(messages);
        }

        public MessageModel GetMessageById(int messageId)
        {
            var message = _messageRepo.GetEntityByIdFromDB(messageId);
            return _mapper.Map<MessageEntity, MessageModel>(message);
        }

        public MessageModel GetMessageIfNotDeletedById(int messageId)
        {
            var message = _messageRepo.GetEntityByIdFromDB(messageId);
            if (message.DateDeleted == null)
            {
                return _mapper.Map<MessageEntity, MessageModel>(message);
            }
            else
            {
                throw new ApplicationException("This message has been deleted");
            }
        }

        public IReadOnlyList<MessageModel> GetMessagesNotDeletedByChannel(string channelName)
        {
            var spec = new GetMessagesNotDeletedByChannelSpec(channelName);
            var messages = _messageRepo.GetEntitiesWithSpec(spec);
            return _mapper.Map<IReadOnlyList<MessageEntity>, IReadOnlyList<MessageModel>>(messages);
        }
    }
}