using Core.DTOs;
using Core.InputValidationModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IUserSessionService _userSessService;
        private readonly IUserService _userService;

        public SignUpController(IUserSessionService userSessService,
                                IUserService userService)
        {
            _userSessService = userSessService;
            _userService = userService;
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

                return Ok(newSession);
            }
        }

        //Get api/SignUp
        [HttpGet]
        public ActionResult<UserSessionModel> SignUpGuest()
        {
            var newGuest = _userService.CreateNewGuest();
            var newSession = _userSessService.CreateUserSession(newGuest.Id);

            return Ok(newSession);
        }
    }
}