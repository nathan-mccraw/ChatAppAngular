using Core.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace API.Authentication
{
    public interface IJwtGenerator
    {
        dynamic GenerateAccessToken(UserSessionModel userSession);

        string GenerateRefreshToken(RefreshTokenModel refreshToken);

        JwtSecurityToken DecodeJwt(string encodedToken);
    }
}