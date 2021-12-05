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
        private readonly IGenericRepository<UserSessionEntity> _sessionRepo;
        private readonly IMapper _mapper;

        public SignInController(IGenericRepository<UserEntity> userRepository,
                                IGenericRepository<UserSessionEntity> sessionRepo,
                                IMapper mapper)
        {
            _userRepo = userRepository;
            _sessionRepo = sessionRepo;
            _mapper = mapper;
        }

        // POST api/SignIn
        [HttpPost]
        public ActionResult<UserSessionModel> SignInUser([FromBody] SignInModel userAttempt)
        {
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            var storedUserEntity = _userRepo.GetEntityWithSpec(specs);
            if (storedUserEntity == null || storedUserEntity.Password != userAttempt.Password)
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
            else if (storedUserEntity.DateDeleted != null)
            {
                return BadRequest("This profile has been deleted.");
            }
            else if (storedUserEntity.UserSessions.Where(x => x.isActive == true).Any())
            {
                var activeSessions = storedUserEntity.UserSessions.Where(x => x.isActive);
                foreach (var sess in activeSessions)
                {
                    sess.TokenExpirationDate = DateTime.UtcNow;
                    _sessionRepo.UpdateEntityInDB(sess);
                }
            }

            var newSession = new UserSessionEntity()
            {
                UserId = storedUserEntity.Id
            };
            newSession = _sessionRepo.AddEntityToDB(newSession);
            var userSession = _mapper.Map<UserSessionEntity, UserSessionModel>(newSession);

            return Ok(userSession);
        }

        // PUT api/<SignIn>
        [HttpPut]
        public ActionResult<UserSessionModel> EditUserEntity([FromBody] IncomingUserProfile userAttempt)
        {
            var storedUserEntity = _userRepo.GetEntityByIdFromDB(userAttempt.User.UserId);
            if (storedUserEntity == null || storedUserEntity.Password != userAttempt.Password)
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }
            else if (storedUserEntity.DateDeleted != null)
            {
                return BadRequest("This account has been deleted");
            }

            if (userAttempt.Username != null)
            {
                var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
                if (_userRepo.GetEntityWithSpec(specs) != null)
                {
                    return BadRequest("Username already exists. Please choose another new username");
                }

                storedUserEntity.Username = userAttempt.Username;
            }

            if (userAttempt.FirstName != "")
            {
                storedUserEntity.FirstName = userAttempt.FirstName;
            }

            if (userAttempt.LastName != "")
            {
                storedUserEntity.LastName = userAttempt.LastName;
            }

            _userRepo.UpdateEntityInDB(storedUserEntity);

            var storedSessionEntity = _sessionRepo.GetEntityByIdFromDB(userAttempt.User.Id);
            if (storedSessionEntity.isActive)
            {
                storedSessionEntity.LastActive = DateTime.UtcNow;
                storedSessionEntity.TokenExpirationDate = DateTime.UtcNow.AddMinutes(15);
                _sessionRepo.UpdateEntityInDB(storedSessionEntity);
                return Ok(_mapper.Map<UserSessionEntity, UserSessionModel>(storedSessionEntity));
            }
            else
            {
                var newSession = new UserSessionEntity()
                {
                    UserId = storedUserEntity.Id
                };
                newSession = _sessionRepo.AddEntityToDB(newSession);
                var userSession = _mapper.Map<UserSessionEntity, UserSessionModel>(newSession);

                return Ok(userSession);
            }
        }

        // DELETE api/SignIn/5
        [HttpDelete]
        public ActionResult Delete(SignInModel userAttempt)
        {
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            var storedUserEntity = _userRepo.GetEntityWithSpec(specs);

            if (storedUserEntity == null || storedUserEntity.Password != userAttempt.Password)
            {
                return Unauthorized("The combination of entered Username and password is not valid");
            }

            try
            {
                var activeSessions = storedUserEntity.UserSessions.Where(x => x.isActive);
                foreach (var sess in activeSessions)
                {
                    sess.TokenExpirationDate = DateTime.UtcNow;
                    _sessionRepo.UpdateEntityInDB(sess);
                }

                storedUserEntity.DateDeleted = DateTime.Now;
                storedUserEntity.Password = new Guid().ToString();
                _userRepo.UpdateEntityInDB(storedUserEntity);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}