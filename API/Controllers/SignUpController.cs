using API.Authentication;
using Core.DTOs;
using Core.InputValidationModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IUserSessionService _userSessService;
        private readonly IUserService _userService;
        private readonly IJwtGenerator _jwtGen;

        public SignUpController(IUserSessionService userSessService,
                                IUserService userService,
                                IJwtGenerator jwtGenerator)
        {
            _userSessService = userSessService;
            _userService = userService;
            _jwtGen = jwtGenerator;
        }

        //POST api/SignUp
        [HttpPost]
        public ActionResult<UserSessionModel> SignUpNewUser(IncomingNewUserModel clientUser)
        {
            //If username already exists in db then return error
            if (_userService.DoesUsernameExist(clientUser.Username))
            {
                return BadRequest("Username Already Exists");
            }
            else
            {
                var newUser = _userService.CreateNewUser(clientUser);
                var newSession = _userSessService.CreateUserSession(newUser.Id);
                RefreshTokenModel refreshToken = new()
                {
                    SessionId = newSession.Id,
                    UserId = newUser.Id
                };

                string newEncodedToken = _jwtGen.GenerateRefreshToken(refreshToken);

                CookieOptions options = new();
                options.Expires = DateTime.UtcNow.AddHours(2);
                options.HttpOnly = true;

                Response.Cookies.Append("Refresh_Token", newEncodedToken, options);

                return Ok(newSession);
            }
        }

        //Get api/SignUp
        [HttpGet]
        public ActionResult<UserSessionModel> SignUpGuest()
        {
            var newGuest = _userService.CreateNewGuest();
            var newSession = _userSessService.CreateUserSession(newGuest.Id);
            RefreshTokenModel refreshToken = new()
            {
                SessionId = newSession.Id,
                UserId = newGuest.Id
            };

            string newEncodedToken = _jwtGen.GenerateRefreshToken(refreshToken);

            CookieOptions options = new();
            options.Expires = DateTime.UtcNow.AddHours(2);
            options.HttpOnly = true;

            Response.Cookies.Append("Refresh_Token", newEncodedToken, options);

            return Ok(newSession);
        }
    }
}