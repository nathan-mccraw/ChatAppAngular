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

        public ValidateUserController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        [HttpGet]
        public ActionResult ValidateUser(UserSessionModel clientUser)
        {
            if (_userSessionService.IsValidSession(clientUser))
            {
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}