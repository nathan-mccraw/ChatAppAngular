using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignInController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public SignInController(IUserRepository userRepository)
        {
            _userRepo = userRepository;
        }

        // POST api/SignIn
        [HttpPost]
        public object SignInUser([FromBody] SignInModel userAttempt)
        {
            return _userRepo.SignIn(userAttempt);
        }

        // PUT api/<SignIn>
        [HttpPut]
        public ActionResult<UserModel> EditUserEntity([FromBody] UserEntity userAttempt)
        {
            return Ok(_userRepo.EditUser(userAttempt)); ;
        }

        // DELETE api/SignIn/5
        [HttpDelete]
        public ActionResult Delete(SignInModel userAttempt)
        {
            return Ok(_userRepo.DeleteUserFromDB(userAttempt));
        }
    }
}