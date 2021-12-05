using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Specifications;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignUpController : ControllerBase
    {
        private readonly IGenericRepository<UserEntity> _userRepo;
        private readonly IGenericRepository<UserSessionEntity> _sessionRepo;
        private readonly IMapper _mapper;

        public SignUpController(IGenericRepository<UserEntity> userRepository, IGenericRepository<UserSessionEntity> sessionRepo, IMapper mapper)
        {
            _userRepo = userRepository;
            _sessionRepo = sessionRepo;
            _mapper = mapper;
        }

        //POST api/SignUp
        [HttpPost]
        public ActionResult<UserSessionModel> SignUpNewUser(UserEntity userAttempt)
        {
            //If username already exists in db then return error
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            if (_userRepo.GetEntityWithSpec(specs) != null)
            {
                return BadRequest("Username Already Exists");
            }
            else
            {
                var newUser = _userRepo.AddEntityToDB(userAttempt);
                var newSession = new UserSessionEntity()
                {
                    UserId = newUser.Id
                };
                newSession = _sessionRepo.AddEntityToDB(newSession);
                var userSession = _mapper.Map<UserSessionEntity, UserSessionModel>(newSession);

                return Ok(userSession);
            }
        }

        //Get api/SignUp
        [HttpGet]
        public ActionResult<UserSessionModel> SignUpGuest()
        {
            var newGuest = new UserEntity
            {
                Username = "Guest",
                Password = "guest",
                FirstName = "Guest",
                LastName = "Guest",
            };
            _userRepo.AddEntityToDB(newGuest);
            var spec = new GetUserEntityByUsernameSpec(newGuest.Username);
            var updatedUser = _userRepo.GetEntityWithSpec(spec);
            updatedUser.Username = "Guest" + updatedUser.Id;
            _userRepo.UpdateEntityInDB(updatedUser);

            var newSession = new UserSessionEntity()
            {
                UserId = updatedUser.Id
            };
            newSession = _sessionRepo.AddEntityToDB(newSession);
            var userSession = _mapper.Map<UserSessionEntity, UserSessionModel>(newSession);

            return Ok(userSession);
        }
    }
}