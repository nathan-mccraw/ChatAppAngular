using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using Core.Entities;
using Core.Interfaces;
using Core.DTOs;
using Core.InputValidationModels;

namespace Infrastructure.Repository
{
    public class MessageRepository : IMessageRepository
    {
        private readonly ISessionFactory _sessionFactory;
        private readonly IUserRepository _userRepo;
        private readonly IUserSessionRepository _userSessRepo;

        public MessageRepository(ISessionFactory sessionFactory, IUserRepository userRepository, IUserSessionRepository userSessionRepository)
        {
            _sessionFactory = sessionFactory;
            _userRepo = userRepository;
            _userSessRepo = userSessionRepository;
        }

        public Task<List<MessageModel>> GetMessagesFromDB()
        {
            using (var session = _sessionFactory.OpenSession())
            {
                return session.Query<MessageEntity>()
                    .Select(message => new MessageModel(message))
                    .ToListAsync();
            };
        }

        public MessageModel GetMessageByIdFromDB(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var message = session.Query<MessageEntity>()
                                .First(msg => msg.MessageId == id);

                return new MessageModel(message);
            };
        }

        public MessageModel PostMessageToDB(IncomingMessageModel incomingMessage)
        {
            UserSessionEntity userSess;
            try
            {
                userSess = _userSessRepo.GetUserSession(incomingMessage.User.UserId);
            }
            catch (ArgumentNullException)
            {
                throw new ArgumentNullException("No Session Exists");
            }

            //Validate user info attached to incoming message is really them and they have an active session
            if (userSess.UserToken == incomingMessage.User.UserToken && _userSessRepo.IsUserSessionNotExpired(userSess))
            {
                //update usersession with new expiration date
                _userSessRepo.UpdateUserSession(incomingMessage.User.UserId);

                //find UserEntity that sent message
                var userEntity = _userRepo.GetUserEntity(incomingMessage.User.UserId);

                //make new messageEntity to post in DB
                MessageEntity message = new() { Text = incomingMessage.Text, User = userEntity };

                //post message to DB
                using (var session = _sessionFactory.OpenSession())
                {
                    using (var transmit = session.BeginTransaction())
                    {
                        session.Save(message);
                        transmit.Commit();
                    }
                };

                //turn message into DTO and return
                return new MessageModel(message);
            }
            else if (userSess.UserToken != incomingMessage.User.UserToken)
            {
                throw new InvalidOperationException("You tried to post a message as different user! Please logout and login again");
            }
            else if (!_userSessRepo.IsUserSessionNotExpired(userSess))
            {
                throw new InvalidOperationException("Your session has expired, please login again");
            }
            else
            {
                throw new ApplicationException("Unknown Error");
            }
        }

        public void DeleteMessageFromDB(int id)
        {
            using (var session = _sessionFactory.OpenSession())
            {
                var message = session.Query<MessageEntity>().Where(x => x.MessageId == id).FirstOrDefault();
                session.Delete(message);
            };
        }
    }
}