using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using Core.Interfaces;
using NHibernate;
using NHibernate.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net.BCrypt;

namespace Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ISessionFactory _sessionFactory;

        public UserRepository(ISessionFactory sessionFactory)
        {
            _sessionFactory = sessionFactory;
        }

        public UserModel CreateUser(UserEntity userAttempt)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                bool usernameExist = session.Query<UserEntity>().Where(x => x.Username == userAttempt.Username).Any();
                if (usernameExist)
                {
                    throw new ApplicationException("Username Exist, Please choose another username");
                }
                else
                {
                    string hashedPassword = BC.HashPassword(userAttempt.Password);
                    userAttempt.Password = hashedPassword;
                    using (var transmit = session.BeginTransaction())
                    {
                        session.Save(userAttempt);
                        transmit.Commit();
                    }
                    return new UserModel(userAttempt);
                }
            }
        }

        public string DeleteUserFromDB(SignInModel userAttempt)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var userEntity = session.Query<UserEntity>().Where(x => x.Username == userAttempt.Username).FirstOrDefault();
                if (userEntity.Password == userAttempt.Password)
                {
                    session.Delete(userEntity);
                    return ("Deleted User!");
                }
                else
                {
                    return ("Invalid password");
                }
            }
        }

        public UserModel EditUser(UserEntity userAttempt)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var userEntity = session.Query<UserEntity>().Where(x => x.UserId == userAttempt.UserId).FirstOrDefault();
                if (userEntity.Password == userAttempt.Password)
                {
                    if (userAttempt.Username != "")
                        userEntity.Username = userAttempt.Username;
                    if (userAttempt.FirstName != "")
                        userEntity.FirstName = userAttempt.FirstName;
                    if (userAttempt.LastName != "")
                        userEntity.LastName = userAttempt.LastName;

                    using (var transmit = session.BeginTransaction())
                    {
                        session.Save(userEntity);
                        transmit.Commit();
                    }

                    return new UserModel(userEntity);
                }
                else
                {
                    throw new ApplicationException("Invalid Password");
                }
            }
        }

        public UserModel GetUserByIdFromDB(int id)
        {
            try
            {
                using (var session = _sessionFactory.OpenSession())
                {
                    var user = session.Query<UserEntity>().Where(x => x.UserId == id).First();
                    return new UserModel(user);
                };
            }
            catch (ArgumentNullException ex)
            {
                throw ex;
            }
        }

        public UserEntity GetUserEntity(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<UserEntity>().Where(x => x.UserId == id).First();
            }
        }

        public Task<List<UserModel>> GetUsersListFromDB()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<UserEntity>()
                    .Select(user => new UserModel(user))
                    .ToListAsync();
            };
        }

        public void PostUserToDB(UserEntity user)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                using (var transmit = session.BeginTransaction())
                {
                    session.Save(user);
                    transmit.Commit();
                }
            };
        }

        public UserModel SignIn(SignInModel userAttempt)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var userEntity = session.Query<UserEntity>().Where(x => x.Username == userAttempt.Username).FirstOrDefault();
                if (userEntity.Password == userAttempt.Password)
                {
                    UserSessionEntity userSession = new();
                    userSession.UserId = userEntity.UserId;

                    using (var transmit = session.BeginTransaction())
                    {
                        session.Save(userSession);
                        transmit.Commit();
                    }

                    UserModel userModel = new UserModel(userEntity);
                    userModel.UserToken = userSession.UserToken;

                    return userModel;
                }
                else
                {
                    throw new ApplicationException("Wrong Password");
                }
            }
        }
    }
}