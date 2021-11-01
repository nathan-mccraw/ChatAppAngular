using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private IUserRepository _userRepo;

        public SignUpController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        // POST api/<SignUp>
        [HttpPost]
        public ActionResult<UserModel> Post(UserEntity userAttempt)
        {
            return Ok(_userRepo.CreateUser(userAttempt));
        }
    }
}