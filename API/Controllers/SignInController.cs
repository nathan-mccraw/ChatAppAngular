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
            if (matchingUsernameInDb.Password == userAttempt.Password)
            {
                var userSession = _userSessRepo.GetEntityByIdFromDB(matchingUsernameInDb.Id);
                if (userSession != null && userSession.TokenExpirationDate > DateTime.UtcNow)
                {
                    //update token expiration
                    userSession.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                    return Ok(userSession);
                }
                else
                {
                    if (userSession != null)
                    {
                        _userSessRepo.DeleteEntityFromDB(userSession.Id);
                    }

                    userSession = new UserSessionEntity { UserId = matchingUsernameInDb.Id, Username = matchingUsernameInDb.Username };
                    userSession = _userSessRepo.AddEntityToDB(userSession);
                    return Ok(_mapper.Map<UserSessionEntity, UserSessionModel>(userSession));
                }
            }
            else
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
        }

        // PUT api/<SignIn>
        [HttpPut]
        public ActionResult<UserSessionModel> EditUserEntity([FromBody] UserEntity userAttempt)
        {
            var currentUserEntity = _userRepo.GetEntityByIdFromDB(userAttempt.Id);
            if (currentUserEntity.Password != userAttempt.Password)
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }

            var userSession = _userSessRepo.GetEntityByIdFromDB(currentUserEntity.Id);
            if (userSession != null && userSession.TokenExpirationDate > DateTime.UtcNow)
            {
                userSession.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                userSession.LastActive = DateTime.UtcNow;
                _userSessRepo.UpdateEntityInDB(userSession);
            }
            else
            {
                if (userSession != null)
                {
                    _userSessRepo.DeleteEntityFromDB(userSession.Id);
                }

                userSession = new UserSessionEntity { UserId = currentUserEntity.Id };
                userSession = _userSessRepo.AddEntityToDB(userSession);
            }

            if(userAttempt.Username != null)
            {
                var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
                if (_userRepo.GetEntitiesWithSpec(specs) != null)
                {
                    return BadRequest("Username already exists. Please put a new username");
                }

                currentUserEntity.Username = userAttempt.Username;
                    
            }

            if(userAttempt.FirstName != null)
            {
                currentUserEntity.FirstName = userAttempt.FirstName;
            }

            if(userAttempt.LastName != null)
            {
                currentUserEntity.LastName = userAttempt.LastName;
            }

            _userRepo.UpdateEntityInDB(currentUserEntity);

            return Ok(new UserSessionModel
            {
                UserId = userSession.UserId,
                Username = currentUserEntity.Username,
                UserToken = userSession.UserToken,
                TokenExpirationDate = userSession.TokenExpirationDate,
                LastActive = userSession.LastActive
            });
        }

        // DELETE api/SignIn/5
        [HttpDelete]
        public ActionResult Delete(SignInModel userAttempt)
        {
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            var mathcingUsernameInDb = _userRepo.GetEntityWithSpec(specs);
            if (mathcingUsernameInDb.Password == userAttempt.Password)
            {
                var userSession = _userSessRepo.GetEntityByIdFromDB(mathcingUsernameInDb.Id);
                if (userSession != null)
                {
                    _userSessRepo.DeleteEntityFromDB(userSession.Id);
                }

                _userRepo.DeleteEntityFromDB(mathcingUsernameInDb.Id);

                return Ok();
            }
            else
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
        }
    }
}