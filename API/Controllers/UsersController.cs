using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
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
    public class UsersController : ControllerBase
    {
        private readonly IGenericRepository<UserEntity> _repo;
        private readonly IMapper _mapper;

        public UsersController(IGenericRepository<UserEntity> repository, IMapper mapper)
        {
            _repo = repository;
            _mapper = mapper;
        }

        // GET: api/<UsersController>
        [HttpGet]
        public ActionResult<IReadOnlyList<UserModel>> GetUsers()
        {
            var users = _repo.GetAllEntitiesFromDB();
            return Ok(_mapper.Map<IReadOnlyList<UserEntity>, IReadOnlyList<UserModel>>(users));
        }

        // GET: api/<UsersController>/5
        [HttpGet("{id}")]
        public ActionResult<UserModel> GetUserById(int id)
        {
            var user = _repo.GetEntityByIdFromDB(id);
            return Ok(_mapper.Map<UserEntity, UserModel>(user));
        }

        // POST: api/<UsersController>
        //[HttpPost]
        //public void Post(UserEntity user)
        //{
        //    _repo.AddEntityToDB(user);
        //}
    }
}