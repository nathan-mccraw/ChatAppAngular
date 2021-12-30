using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<IReadOnlyList<UserModel>> GetUsers()
        {
            return Ok(_userService.GetUsersNotDeleted());
        }

        // GET: api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUserById(int id)
        {
            return Ok(_userService.GetUserNotDeletedById(id));
        }
    }
}