using Core.DTOs;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Authentication
{
    public class JwtGenerator : IJwtGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly string JwtSecret;

        public JwtGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
            JwtSecret = _configuration["JwtSecret"];
        }

        public dynamic GenerateToken(UserSessionModel userSession)
        {
            var claims = new List<Claim>
            {
                new Claim(type: "id", value: userSession.Id.ToString()),
                new Claim(type: "userId", value: userSession.UserId.ToString()),
                new Claim(type: "userToken", value: userSession.UserToken.ToString()),
                new Claim(type: "HasOtherActiveSessions", value: userSession.HasOtherActiveSessions.ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.UtcNow.AddHours(2)).ToUnixTimeSeconds().ToString())
            };

            var token = CreateJwtToken(claims);

            return new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        private JwtSecurityToken CreateJwtToken(List<Claim> payload)
        {
            return new JwtSecurityToken(
                    new JwtHeader(
                        new SigningCredentials(
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSecret)),
                            SecurityAlgorithms.HmacSha256)),
                    new JwtPayload(payload));
        }
    }
}