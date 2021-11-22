using API.DTOs;
using AutoMapper;
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
            var mathcingUsernameInDb = _userRepo.GetEntityWithSpec(specs);
            if (mathcingUsernameInDb.Password == userAttempt.Password)
            {
                var userSession = _userSessRepo.GetEntityByIdFromDB(mathcingUsernameInDb.UserId);
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
                        _userSessRepo.DeleteEntityFromDB(userSession.SessionId);
                    }

                    userSession = new UserSessionEntity { UserId = mathcingUsernameInDb.UserId };
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
        public ActionResult<UserModel> EditUserEntity([FromBody] UserEntity userAttempt)
        {
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            var mathcingUsernameInDb = _userRepo.GetEntityWithSpec(specs);
            if (mathcingUsernameInDb.Password == userAttempt.Password)
            {
                var userSession = _userSessRepo.GetEntityByIdFromDB(mathcingUsernameInDb.UserId);
                if (userSession != null && userSession.TokenExpirationDate > DateTime.UtcNow)
                {
                    userSession.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                    return Ok(userSession);
                }
                else
                {
                    if (userSession != null)
                    {
                        _userSessRepo.DeleteEntityFromDB(userSession.SessionId);
                    }

                    userSession = new UserSessionEntity { UserId = mathcingUsernameInDb.UserId };
                    userSession = _userSessRepo.AddEntityToDB(userSession);
                    return Ok(_mapper.Map<UserSessionEntity, UserSessionModel>(userSession));
                }

                //edit existing userEntity using generic put and building new specs
            }
            else
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
        }

        // DELETE api/SignIn/5
        [HttpDelete]
        public ActionResult Delete(SignInModel userAttempt)
        {
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            var mathcingUsernameInDb = _userRepo.GetEntityWithSpec(specs);
            if (mathcingUsernameInDb.Password == userAttempt.Password)
            {
                var userSession = _userSessRepo.GetEntityByIdFromDB(mathcingUsernameInDb.UserId);
                if (userSession != null)
                {
                    _userSessRepo.DeleteEntityFromDB(userSession.SessionId);
                }

                _userRepo.DeleteEntityFromDB(mathcingUsernameInDb.UserId);

                return Ok();
            }
            else
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
        }
    }
}