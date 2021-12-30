using API.Authentication;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValidateUserController : Controller
    {
        private readonly IUserSessionService _userSessionService;
        private readonly IUserService _userService;
        private readonly IJwtGenerator _jwtGen;

        public ValidateUserController(IUserSessionService userSessionService, IUserService userService, IJwtGenerator jwtGen)
        {
            _userSessionService = userSessionService;
            _userService = userService;
            _jwtGen = jwtGen;
        }

        [HttpGet]
        public ActionResult ValidateUser()
        {
            Request.Cookies.TryGetValue("Refresh_Token", out var encodedToken);
            var token = _jwtGen.DecodeJwt(encodedToken);
            RefreshTokenModel refreshToken = new()
            {
                SessionId = Int32.Parse(token.Claims.First(c => c.Type == "sessionId").Value),
                UserId = Int32.Parse(token.Claims.First(c => c.Type == "userId").Value)
            };

            var newEncodedToken = _jwtGen.GenerateRefreshToken(refreshToken);

            CookieOptions options = new();
            options.Expires = DateTime.UtcNow.AddHours(2);
            options.HttpOnly = true;

            Response.Cookies.Append("Refresh_Token", newEncodedToken, options);

            var updatedSession = _userSessionService.UpdateSession(refreshToken.UserId);
            updatedSession.HasOtherActiveSessions = _userService.DoesHaveOtherActiveSessions(refreshToken.UserId);
            return Ok(updatedSession);
        }
    }
}