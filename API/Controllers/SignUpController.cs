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
        private readonly IMapper _mapper;

        public SignUpController(IGenericRepository<UserEntity> userRepository, IMapper mapper)
        {
            _userRepo = userRepository;
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
                var userSession = _mapper.Map<UserEntity, UserSessionModel>(newUser);

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
            var spec = new GetUserEntityByUserTokenSpec(newGuest.UserToken);
            var updatedUser = _userRepo.GetEntityWithSpec(spec);
            updatedUser.Username = "Guest" + updatedUser.Id;
            _userRepo.UpdateEntityInDB(updatedUser);
            var userSession = _mapper.Map<UserEntity, UserSessionModel>(updatedUser);

            return Ok(userSession);
        }
    }
}