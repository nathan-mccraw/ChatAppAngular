//using Core.DTOs;
//using Core.Entities;
//using Core.InputValidationModels;
//using Core.Interfaces;
//using NHibernate;
//using NHibernate.Linq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BC = BCrypt.Net.BCrypt;

//namespace Infrastructure.Repository
//{
//    public class UserRepository : IUserRepository
//    {
//        private readonly ISessionFactory _sessionFactory;

//        public UserRepository(ISessionFactory sessionFactory)
//        {
//            _sessionFactory = sessionFactory;
//        }

//        public async Task<UserModel> CreateUser(UserEntity userAttempt)
//        {
//            using (var session = _sessionFactory.OpenSession())
//            {
//                bool usernameExist = await session.Query<UserEntity>().Where(x => x.Username == userAttempt.Username).AnyAsync();
//                if (usernameExist)
//                {
//                    throw new ApplicationException("Username Exist, Please choose another username");
//                }
//                else
//                {
//                    string hashedPassword = BC.HashPassword(userAttempt.Password);
//                    userAttempt.Password = hashedPassword;
//                    using (var transmit = session.BeginTransaction())
//                    {
//                        await session.SaveAsync(userAttempt);
//                        await transmit.CommitAsync();
//                    }
//                    return new UserModel(userAttempt);
//                }
//            }
//        }

//        public async Task<string> DeleteUserFromDB(SignInModel userAttempt)
//        {
//            using (var session = _sessionFactory.OpenSession())
//            {
//                var userEntity = await session.Query<UserEntity>().Where(x => x.Username == userAttempt.Username).FirstOrDefaultAsync();
//                if (userEntity.Password == userAttempt.Password)
//                {
//                    await session.DeleteAsync(userEntity);
//                    return ("Deleted User!");
//                }
//                else
//                {
//                    return ("Invalid password");
//                }
//            }
//        }

//        public async Task<UserModel> EditUser(UserEntity userAttempt)
//        {
//            using (var session = _sessionFactory.OpenSession())
//            {
//                var userEntity = await session.Query<UserEntity>().Where(x => x.UserId == userAttempt.UserId).FirstOrDefaultAsync();
//                if (userEntity.Password == userAttempt.Password)
//                {
//                    if (userAttempt.Username != "")
//                        userEntity.Username = userAttempt.Username;
//                    if (userAttempt.FirstName != "")
//                        userEntity.FirstName = userAttempt.FirstName;
//                    if (userAttempt.LastName != "")
//                        userEntity.LastName = userAttempt.LastName;

//                    using (var transmit = session.BeginTransaction())
//                    {
//                        await session.SaveAsync(userEntity);
//                        await transmit.CommitAsync();
//                    }

//                    return new UserModel(userEntity);
//                }
//                else
//                {
//                    throw new ApplicationException("Invalid Password");
//                }
//            }
//        }

//        public async Task<UserModel> GetUserByIdFromDB(int id)
//        {
//            try
//            {
//                using (var session = _sessionFactory.OpenSession())
//                {
//                    var user = await session.Query<UserEntity>().Where(x => x.UserId == id).FirstOrDefaultAsync();
//                    return new UserModel(user);
//                };
//            }
//            catch (ArgumentNullException ex)
//            {
//                throw ex;
//            }
//        }

//        public async Task<UserEntity> GetUserEntity(int id)
//        {
//            using (var session = _sessionFactory.OpenSession())
//            {
//                return await session.Query<UserEntity>().Where(x => x.UserId == id).FirstOrDefaultAsync();
//            }
//        }

//        public async Task<List<UserModel>> GetUsersListFromDB()
//        {
//            using (var session = _sessionFactory.OpenSession())
//            {
//                return await session.Query<UserEntity>()
//                    .Select(user => new UserModel(user))
//                    .ToListAsync();
//            };
//        }

//        public void PostUserToDB(UserEntity user)
//        {
//            using (var session = _sessionFactory.OpenSession())
//            {
//                using (var transmit = session.BeginTransaction())
//                {
//                    session.Save(user);
//                    transmit.Commit();
//                }
//            };
//        }

//        public async Task<UserModel> SignIn(SignInModel userAttempt)
//        {
//            using (var session = _sessionFactory.OpenSession())
//            {
//                var userEntity = await session.Query<UserEntity>().Where(x => x.Username == userAttempt.Username).FirstOrDefaultAsync();
//                if (userEntity.Password == userAttempt.Password)
//                {
//                    UserSessionEntity userSession = new();
//                    userSession.UserId = userEntity.UserId;

//                    using (var transmit = session.BeginTransaction())
//                    {
//                        await session.SaveAsync(userSession);
//                        await transmit.CommitAsync();
//                    }

//                    UserModel userModel = new UserModel(userEntity);
//                    userModel.UserToken = userSession.UserToken;

//                    return userModel;
//                }
//                else
//                {
//                    throw new ApplicationException("Wrong Password");
//                }
//            }
//        }
//    }
//}