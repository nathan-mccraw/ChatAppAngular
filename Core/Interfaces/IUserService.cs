using Core.DTOs;
using Core.InputValidationModels;

namespace Core.Interfaces
{
    public interface IUserService
    {
        public void CloseAllActiveSessions(int userId);

        public UserModel CreateNewGuest();

        public UserModel CreateNewUser(IncomingNewUserModel clientUser);

        public void DeleteUser(int userId);

        public bool DoesUsernameExist(string username);

        public bool HasOtherActiveSessions(int userId);

        public bool IsUserDeleted(int userId);

        public void UpdateUserProfile(IncomingUserProfileModel clientUser);

        public UserModel ValidateSignIn(IncomingSignInModel clientUser);
    }
}