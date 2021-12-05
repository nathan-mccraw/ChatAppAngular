using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services
{
    public class UserSessionService : IUserSessionService
    {
        private readonly IGenericRepository<UserSessionEntity> _sessionRepo;
        private readonly IMapper _mapper;

        public UserSessionService(IGenericRepository<UserSessionEntity> sessionRepo, IMapper mapper)
        {
            _sessionRepo = sessionRepo;
            _mapper = mapper;
        }

        public bool IsValidSession(UserSessionModel clientSession)
        {
            var sessionStoredInDB = _sessionRepo.GetEntityByIdFromDB(clientSession.Id);
            return (sessionStoredInDB.UserToken == clientSession.UserToken && sessionStoredInDB.isActive);
        }

        public UserSessionModel UpdateSession(UserSessionModel clientSession)
        {
            var sessionStoredInDB = _sessionRepo.GetEntityByIdFromDB(clientSession.Id);
            sessionStoredInDB.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
            sessionStoredInDB.LastActive = DateTime.UtcNow;
            _sessionRepo.UpdateEntityInDB(sessionStoredInDB);

            return (_mapper.Map<UserSessionEntity, UserSessionModel>(sessionStoredInDB));
        }

        public UserSessionModel CreateUserSession(int userId)
        {
            var newSession = new UserSessionEntity()
            {
                UserId = userId
            };
            newSession = _sessionRepo.AddEntityToDB(newSession);
            return (_mapper.Map<UserSessionEntity, UserSessionModel>(newSession));
        }

        public void SignOut(UserSessionModel clientSession)
        {
            var sessionStoredInDB = _sessionRepo.GetEntityByIdFromDB(clientSession.Id);
            sessionStoredInDB.TokenExpirationDate = DateTime.UtcNow;
            _sessionRepo.UpdateEntityInDB(sessionStoredInDB);
        }
    }
}