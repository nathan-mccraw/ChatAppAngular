using Core.DTOs;

namespace Core.Interfaces
{
    public interface IUserSessionService
    {
        UserSessionModel CreateUserSession(int userId);

        bool IsValidSession(UserSessionModel clientSession);

        UserSessionModel UpdateSession(UserSessionModel clientSession);

        void SignOut(UserSessionModel clientSession);
    }
}