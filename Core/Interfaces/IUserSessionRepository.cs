using Core.Entities;
using Core.InputValidationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IUserSessionRepository
    {
        public Task<UserSessionEntity> GetUserSession(int id);

        public bool IsUserSessionNotExpired(UserSessionEntity userSessionEntity);

        public void UpdateUserSession(int userId);

        public Task<UserSessionEntity> CreateNewUserSession(int userId);

        public void DeleteUserSession(int userId);
    }
}