using Core.Entities;

namespace Core.Interfaces
{
    public interface IUserVerify
    {
        bool IsSessionValid(UserSessionEntity user);
    }
}