using API.DTOs;
using AutoMapper;
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
        private readonly IGenericRepository<UserSessionEntity> _userSessRepo;
        private readonly IMapper _mapper;

        public SignUpController(IGenericRepository<UserEntity> userRepository, IGenericRepository<UserSessionEntity> userSessionRepository, IMapper mapper)
        {
            _userRepo = userRepository;
            _userSessRepo = userSessionRepository;
            _mapper = mapper;
        }

        // POST api/<SignUp>
        [HttpPost]
        public ActionResult<UserSessionModel> Post(UserEntity userAttempt)
        {
            //If username already exists in db then return error
            var specs = new GetUserEntityByUsernameSpec(userAttempt.Username);
            if (_userRepo.GetEntityWithSpec(specs) == null)
            {
                return BadRequest("Username Already Exists");
            }
            else
            {
                var newUser = _userRepo.AddEntityToDB(userAttempt);
                var newUserSession = new UserSessionEntity { UserId = newUser.UserId };
                newUserSession = _userSessRepo.AddEntityToDB(newUserSession);
                var sessionToReturn = _mapper.Map<UserSessionEntity, UserSessionModel>(newUserSession);

                return Ok(sessionToReturn);
            }
        }
    }
}