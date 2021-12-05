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
            CreateMap<MessageEntity, MessageModel>()
                .ForMember(d => d.UserId, o => o.MapFrom(s => s.User.Id))
                .ForMember(d => d.Username, o => o.MapFrom(s => s.User.Username))
                .ForMember(d => d.ChannelId, o => o.MapFrom(s => s.Channel.Id));

            CreateMap<UserEntity, UserModel>()
                .ForMember(d => d.LastActive, o => o.MapFrom(s => s.UserSessions.OrderByDescending(x => x.LastActive).FirstOrDefault().LastActive));

            CreateMap<UserEntity, UserSessionModel>()
                .ForMember(d => d.UserToken, o => o.MapFrom(s => s.UserSessions.OrderByDescending(x => x.LastActive).FirstOrDefault().UserToken))
                .ForMember(d => d.Id, o => o.MapFrom(s => s.UserSessions.OrderByDescending(x => x.LastActive).FirstOrDefault().Id));

            CreateMap<UserSessionEntity, UserSessionModel>();

            //CreateMap<UserSessionEntity, UserSessionModel>()
            //    .ForMember(d => d.UserId, o => o.MapFrom(s => s.User.Id))
            //    .ForMember(d => d.Username, o => o.MapFrom(s => s.User.Username));
        }
    }
}