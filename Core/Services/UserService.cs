using AutoMapper;
using Core.DBSpecifications;
using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Services
{
    public class UserService : IUserService
    {
        private readonly IGenericRepository<UserEntity> _userRepo;
        private readonly IGenericRepository<UserSessionEntity> _sessionRepo;
        private readonly IMapper _mapper;

        public UserService(IGenericRepository<UserEntity> userRepo, IGenericRepository<UserSessionEntity> sessionRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _sessionRepo = sessionRepo;
            _mapper = mapper;
        }

        public void CloseAllActiveSessions(int userId)
        {
            var user = _userRepo.GetEntityByIdFromDB(userId);
            var activeSessions = user.UserSessions.Where(session => session.isActive);
            foreach (var sess in activeSessions)
            {
                sess.TokenExpirationDate = DateTime.UtcNow;
                _sessionRepo.UpdateEntityInDB(sess);
            }
        }

        public UserModel CreateNewGuest()
        {
            var newGuest = new UserEntity
            {
                Username = "Guest",
                Password = "guest",
                FirstName = "Guest",
                LastName = "Guest",
                Location = "N/A",
                DateOfBirth = DateTime.UtcNow
            };
            newGuest = _userRepo.AddEntityToDB(newGuest);
            newGuest.Username = "Guest" + newGuest.Id;
            _userRepo.UpdateEntityInDB(newGuest);

            return (_mapper.Map<UserEntity, UserModel>(newGuest));
        }

        public UserModel CreateNewUser(IncomingNewUserModel clientUser)
        {
            var newUserEntity = new UserEntity
            {
                Username = clientUser.Username,
                Password = clientUser.Password,
                FirstName = clientUser.FirstName,
                LastName = clientUser.LastName
            };

            newUserEntity = _userRepo.AddEntityToDB(newUserEntity);

            return (_mapper.Map<UserEntity, UserModel>(newUserEntity));
        }

        public void DeleteUser(int userId)
        {
            var user = _userRepo.GetEntityByIdFromDB(userId);
            user.DateDeleted = DateTime.UtcNow;
            user.Password = new Guid().ToString();
            _userRepo.UpdateEntityInDB(user);

            CloseAllActiveSessions(userId);
        }

        public bool DoesUsernameExist(string username)
        {
            var specs = new GetUserEntityByUsernameSpec(username);
            return (_userRepo.GetEntityWithSpec(specs) != null);
        }

        public UserModel GetUserById(int userId)
        {
            var user = _userRepo.GetEntityByIdFromDB(userId);
            return (_mapper.Map<UserEntity, UserModel>(user));
        }

        public UserModel GetUserNotDeletedById(int userId)
        {
            var spec = new GetUserNotDeletedByIdSpec(userId);
            var user = _userRepo.GetEntityWithSpec(spec);
            return (_mapper.Map<UserEntity, UserModel>(user));
        }

        public IReadOnlyList<UserModel> GetUsersNotDeleted()
        {
            var spec = new GetUsersNotDeletedSpec();
            var users = _userRepo.GetEntitiesWithSpec(spec);
            return (_mapper.Map<IReadOnlyList<UserEntity>, IReadOnlyList<UserModel>>(users));
        }

        public bool DoesHaveOtherActiveSessions(int userId)
        {
            var userEntity = _userRepo.GetEntityByIdFromDB(userId);
            if (userEntity.UserSessions.Where(session => session.isActive == true).Count() > 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUserDeleted(int userId)
        {
            var user = _userRepo.GetEntityByIdFromDB(userId);
            return (user.DateDeleted != null);
        }

        public void UpdateUserProfile(IncomingUserProfileModel clientUser)
        {
            var storedUserEntity = _userRepo.GetEntityByIdFromDB(clientUser.UserSession.UserId);

            if (clientUser.Username != null)
            {
                storedUserEntity.Username = clientUser.Username;
            }

            if (clientUser.FirstName != "")
            {
                storedUserEntity.FirstName = clientUser.FirstName;
            }

            if (clientUser.LastName != "")
            {
                storedUserEntity.LastName = clientUser.LastName;
            }

            _userRepo.UpdateEntityInDB(storedUserEntity);
        }

        public UserModel ValidateSignIn(IncomingSignInModel clientUser)
        {
            var specs = new GetUserEntityByUsernameSpec(clientUser.Username);
            var storedUserEntity = _userRepo.GetEntityWithSpec(specs);
            if (clientUser.Password != storedUserEntity.Password)
            {
                throw new ApplicationException("The combination of entered Username and password is not valid");
            }
            else if (storedUserEntity.DateDeleted != null)
            {
                throw new ApplicationException("This profile has been deleted.");
            }
            else
            {
                return _mapper.Map<UserEntity, UserModel>(storedUserEntity);
            }
        }
    }
}