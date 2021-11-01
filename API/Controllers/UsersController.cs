using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;

        public UsersController(IUserRepository repository)
        {
            _repo = repository;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetUsers()
        {
            var users = await _repo.GetUsersListFromDB();
            return Ok(users);
        }

        // GET: api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUserById(int id)
        {
            return Ok(_repo.GetUserByIdFromDB(id));
        }

        // POST: api/<UsersController>
        [HttpPost]
        public void Post(UserEntity user)
        {
            _repo.PostUserToDB(user);
        }
    }
}