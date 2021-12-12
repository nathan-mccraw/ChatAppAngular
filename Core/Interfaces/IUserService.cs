using Core.DTOs;
using Core.InputValidationModels;
using System.Collections.Generic;

namespace Core.Interfaces
{
    public interface IUserService
    {
        public void CloseAllActiveSessions(int userId);

        public UserModel CreateNewGuest();

        public UserModel CreateNewUser(IncomingNewUserModel clientUser);

        public void DeleteUser(int userId);

        public bool DoesUsernameExist(string username);

        public UserModel GetUserById(int userId);

        public UserModel GetUserNotDeletedById(int userId);

        public IReadOnlyList<UserModel> GetUsersNotDeleted();

        public bool HasOtherActiveSessions(int userId);

        public bool IsUserDeleted(int userId);

        public void UpdateUserProfile(IncomingUserProfileModel clientUser);

        public UserModel ValidateSignIn(IncomingSignInModel clientUser);
    }
}