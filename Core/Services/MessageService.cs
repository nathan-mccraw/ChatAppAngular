using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}