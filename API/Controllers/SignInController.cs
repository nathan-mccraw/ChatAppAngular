using Core.DTOs;
using Core.InputValidationModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserSessionService _userSessionService;

        public SignInController(IUserService userService, IUserSessionService userSessionService)
        {
            _userService = userService;
            _userSessionService = userSessionService;
        }

        // POST api/SignIn
        [HttpPost]
        public ActionResult<UserSessionModel> SignInUser([FromBody] IncomingSignInModel clientUser)
        {
            try
            {
                var user = _userService.ValidateSignIn(clientUser);
                var newSession = _userSessionService.CreateUserSession(user.Id);
                newSession.HasOtherActiveSessions = _userService.HasOtherActiveSessions(user.Id);

                return Ok(newSession);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<SignIn>
        [HttpPut]
        public ActionResult<UserSessionModel> EditUserEntity([FromBody] IncomingUserProfileModel clientUser)
        {
            if (_userSessionService.IsValidSession(clientUser.UserSession) == false)
            {
                return Unauthorized("Please signin and try again");
            }
            else if (_userService.IsUserDeleted(clientUser.UserSession.UserId))
            {
                return Unauthorized("This account have been deleted");
            }
            else if (clientUser.Username != null && _userService.DoesUsernameExist(clientUser.Username))
            {
                return BadRequest("Username already exists. Please choose another new username");
            }

            _userService.UpdateUserProfile(clientUser);
            var updatedSession = _userSessionService.UpdateSession(clientUser.UserSession);
            updatedSession.HasOtherActiveSessions = _userService.HasOtherActiveSessions(clientUser.UserSession.UserId);
            return (updatedSession);
        }

        // DELETE api/SignIn/5
        [HttpDelete]
        public ActionResult Delete(IncomingSignInModel clientUser)
        {
            try
            {
                var user = _userService.ValidateSignIn(clientUser);
                _userService.DeleteUser(user.Id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}