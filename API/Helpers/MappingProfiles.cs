using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<MessageEntity, MessageModel>();

            //CreateMap<IncomingMessageModel, MessageEntity>()
            //    .ForMember(d => d.User.UserId, o => o.MapFrom(s => s.User.UserId));

            CreateMap<UserEntity, UserModel>();

            CreateMap<UserSessionEntity, UserSessionModel>();
            CreateMap<UserSessionModel, UserSessionEntity>();
        }
    }
}