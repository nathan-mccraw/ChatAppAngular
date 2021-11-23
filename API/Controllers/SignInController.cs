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
        private readonly IGenericRepository<UserSessionEntity> _userSessRepo;
        private readonly IMapper _mapper;

        public SignInController(IGenericRepository<UserEntity> userRepository, IGenericRepository<UserSessionEntity> userSessionRepo, IMapper mapper)
        {
            _userRepo = userRepository;
            _userSessRepo = userSessionRepo;
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
            else if (matchingUsernameInDb.UserSession == null)
            {
                return BadRequest("No session for that user exists. Please contact site adminstrator");
            }
            else
            {
                var userSessionEntity = matchingUsernameInDb.UserSession;
                userSessionEntity.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                userSessionEntity.LastActive = DateTime.UtcNow;
                var sessionInDB = _userSessRepo.UpdateEntityInDB(userSessionEntity);
                return Ok(_mapper.Map<UserSessionEntity, UserSessionModel>(sessionInDB));
            }
        }

        // PUT api/<SignIn>
        [HttpPut]
        public ActionResult<UserSessionModel> EditUserEntity([FromBody] UserEntity userAttempt)
        {
            var currentUserEntity = _userRepo.GetEntityByIdFromDB(userAttempt.Id);
            if (currentUserEntity == null || currentUserEntity.Password != userAttempt.Password)
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
            else if (currentUserEntity.UserSession == null)
            {
                return BadRequest("No session for that user exists. Please contact site adminstrator");
            }

            var userSessionEntity = currentUserEntity.UserSession;
            userSessionEntity.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
            userSessionEntity.LastActive = DateTime.UtcNow;
            var sessionInDB = _userSessRepo.UpdateEntityInDB(userSessionEntity);

            if (userAttempt.Username != null)
            {
                var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
                if (_userRepo.GetEntitiesWithSpec(specs) != null)
                {
                    return BadRequest("Username already exists. Please put a new username");
                }

                currentUserEntity.Username = userAttempt.Username;
            }

            if (userAttempt.FirstName != null)
            {
                currentUserEntity.FirstName = userAttempt.FirstName;
            }

            if (userAttempt.LastName != null)
            {
                currentUserEntity.LastName = userAttempt.LastName;
            }

            _userRepo.UpdateEntityInDB(currentUserEntity);

            return Ok(_mapper.Map<UserSessionEntity, UserSessionModel>(sessionInDB));
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

            var userSessionEntity = matchingUsernameInDb.UserSession;
            _userSessRepo.DeleteEntityFromDB(userSessionEntity.Id);
            _userRepo.DeleteEntityFromDB(matchingUsernameInDb.Id);

            return Ok();
        }
    }
}