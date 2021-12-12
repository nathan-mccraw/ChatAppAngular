using Core.DTOs;

namespace API.Authentication
{
    public interface IJwtGenerator
    {
        dynamic GenerateToken(UserSessionModel userSession);
    }
}