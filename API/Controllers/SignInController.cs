using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.InputValidationModels;
using Core.Interfaces;
using Infrastructure.Specifications;
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
        private readonly IGenericRepository<UserEntity> _userRepo;
        private readonly IMapper _mapper;

        public SignInController(IGenericRepository<UserEntity> userRepository, IMapper mapper)
        {
            _userRepo = userRepository;
            _mapper = mapper;
        }

        // POST api/SignIn
        [HttpPost]
        public ActionResult<UserSessionModel> SignInUser([FromBody] SignInModel userAttempt)
        {
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            var matchingUsernameInDb = _userRepo.GetEntityWithSpec(specs);
            if (matchingUsernameInDb == null || matchingUsernameInDb.Password != userAttempt.Password)
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
            else if (matchingUsernameInDb.DateDeleted != null)
            {
                return BadRequest("This profile has been deleted.");
            }
            else
            {
                matchingUsernameInDb.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                matchingUsernameInDb.LastActive = DateTime.UtcNow;
                var UpdatedUserEntity = _userRepo.UpdateEntityInDB(matchingUsernameInDb);
                return Ok(_mapper.Map<UserEntity, UserSessionModel>(UpdatedUserEntity));
            }
        }

        // PUT api/<SignIn>
        [HttpPut]
        public ActionResult<UserSessionModel> EditUserEntity([FromBody] IncomingUserProfile userAttempt)
        {
            var spec = new GetUserEntityByUserTokenSpec(userAttempt.User.UserToken);
            var currentUserEntity = _userRepo.GetEntityWithSpec(spec);
            if (currentUserEntity == null || currentUserEntity.Password != userAttempt.Password)
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
            else if (currentUserEntity.DateDeleted != null)
            {
                return BadRequest("This account has been deleted");
            }

            currentUserEntity.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
            currentUserEntity.LastActive = DateTime.UtcNow;

            if (userAttempt.Username != null)
            {
                var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
                if (_userRepo.GetEntityWithSpec(specs) != null)
                {
                    return BadRequest("Username already exists. Please put a new username");
                }

                currentUserEntity.Username = userAttempt.Username;
            }

            if (userAttempt.FirstName != "")
            {
                currentUserEntity.FirstName = userAttempt.FirstName;
            }

            if (userAttempt.LastName != "")
            {
                currentUserEntity.LastName = userAttempt.LastName;
            }

            var UpdatedUserEntity = _userRepo.UpdateEntityInDB(currentUserEntity);

            return Ok(_mapper.Map<UserEntity, UserSessionModel>(UpdatedUserEntity));
        }

        // DELETE api/SignIn/5
        [HttpDelete]
        public ActionResult Delete(SignInModel userAttempt)
        {
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            var matchingUsernameInDb = _userRepo.GetEntityWithSpec(specs);

            if (matchingUsernameInDb == null || matchingUsernameInDb.Password != userAttempt.Password)
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }

            try
            {
                matchingUsernameInDb.DateDeleted = DateTime.Now;
                matchingUsernameInDb.Password = new Guid().ToString();
                _userRepo.UpdateEntityInDB(matchingUsernameInDb);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}