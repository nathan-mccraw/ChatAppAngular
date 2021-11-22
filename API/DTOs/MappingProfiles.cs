using AutoMapper;
using Core.Entities;
using Core.InputValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MessageEntity, MessageModel>()
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User.Username));

            CreateMap<MessageModel, MessageEntity>()
                .ForMember(d => d.User.UserId, o => o.MapFrom(s => s.UserId));

            CreateMap<IncomingMessageModel, MessageEntity>()
                .ForMember(d => d.User.UserId, o => o.MapFrom(s => s.User.UserId));

            CreateMap<UserEntity, UserModel>();

            CreateMap<UserSessionEntity, UserSessionModel>();
        }
    }
}