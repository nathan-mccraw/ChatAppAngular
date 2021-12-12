using API.Authentication;
using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
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

        [HttpPost]
        public ActionResult ValidateUser(UserSessionModel clientUser)
        {
            // if session is null and jwt is not expired and user has active sessions;
            // return new session
            if (clientUser.Id != 0 && _userSessionService.IsValidSession(clientUser))
            {
                var updatedSession = _userSessionService.UpdateSession(clientUser);
                updatedSession.HasOtherActiveSessions = _userService.HasOtherActiveSessions(clientUser.UserId);
                return Ok(_jwtGen.GenerateToken(clientUser));
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}