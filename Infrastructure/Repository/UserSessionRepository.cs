using Core.Entities;
using Core.InputValidationModels;
using Core.Interfaces;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class UserSessionRepository : IUserSessionRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public UserSessionRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public UserSessionEntity GetUserSession(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<UserSessionEntity>()
                                .First(usr => usr.UserId == id);
            };
        }

        public UserSessionEntity CreateNewUserSession(int userId)
        {
            var userSession = new UserSessionEntity { UserId = userId };
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transmit = session.BeginTransaction())
                {
                    session.Save(userSession);
                    transmit.Commit();
                }
            };

            return userSession;
        }

        public void DeleteUserSession(int userId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var userSession = session.Query<UserSessionEntity>().Where(x => x.UserId == userId).First();
                session.Delete(userSession);
            };
        }

        public bool IsUserSessionNotExpired(UserSessionEntity userSessionEntity)
        {
            if (userSessionEntity.TokenExpirationDate < DateTime.Now)
            {
                return true;
            }
            else
            {
                DeleteUserSession(userSessionEntity.UserId);
                return true;
            }
        }

        public void UpdateUserSession(int userId)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var userSess = session.Query<UserSessionEntity>()
                    .Where(x => x.UserId == userId)
                    .First();
                userSess.TokenExpirationDate = DateTime.Now.AddHours(.25);
                session.Update(userSess);
            }
        }
    }
}