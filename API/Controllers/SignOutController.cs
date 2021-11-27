using Core.DTOs;
using Core.Entities;
using Core.Interfaces;
using Infrastructure.Specifications;
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
    public class SignOutController : Controller
    {
        private readonly IGenericRepository<UserEntity> _userRepo;

        public SignOutController(IGenericRepository<UserEntity> usersRepo)
        {
            _userRepo = usersRepo;
        }

        // PUT: SignOut
        [HttpPut]
        public ActionResult SignOutUser(UserSessionModel UserSession)
        {
            var spec = new GetUserEntityByUserTokenSpec(UserSession.UserToken);
            var storedUserEntity = _userRepo.GetEntityWithSpec(spec);

            if (storedUserEntity == null || storedUserEntity.UserToken != UserSession.UserToken)
            {
                return BadRequest("User session does not exist");
            }

            storedUserEntity.TokenExpirationDate = DateTime.UtcNow;
            _userRepo.UpdateEntityInDB(storedUserEntity);
            return Ok("Signed Out");
        }

        //    // POST: SignOut/Create
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Create(IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: SignOut/Edit/5
        //    public ActionResult Edit(int id)
        //    {
        //        return View();
        //    }

        //    // POST: SignOut/Edit/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Edit(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }

        //    // GET: SignOut/Delete/5
        //    public ActionResult Delete(int id)
        //    {
        //        return View();
        //    }

        //    // POST: SignOut/Delete/5
        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public ActionResult Delete(int id, IFormCollection collection)
        //    {
        //        try
        //        {
        //            return RedirectToAction(nameof(Index));
        //        }
        //        catch
        //        {
        //            return View();
        //        }
        //    }
    }
}