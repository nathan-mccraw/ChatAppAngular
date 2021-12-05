using AutoMapper;
using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSessions : Controller
    {
        private readonly IGenericRepository<UserEntity> _userRepo;
        private readonly IMapper _mapper;

        public UserSessions(IGenericRepository<UserEntity> userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IReadOnlyList<UserEntity>> GetSessions()
        {
            var usersWithSessions = _userRepo.GetAllEntitiesFromDB();
            return Ok(usersWithSessions);
        }
    }
}