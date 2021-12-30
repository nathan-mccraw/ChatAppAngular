using Core.DTOs;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SignOutController : Controller
    {
        private readonly IUserSessionService _userSessionService;

        public SignOutController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
        }

        // PUT: SignOut
        [HttpPut]
        [Authorize]
        public ActionResult SignOutUser(UserSessionModel clientSession)
        {
            if (_userSessionService.IsValidSession(clientSession))
            {
                _userSessionService.SignOut(clientSession);
                Response.Cookies.Delete("Refresh_Token");
                return Ok("Signed Out");
            }
            else
            {
                return BadRequest("User session does not exist");
            }
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