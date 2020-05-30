using MysiePysieMVC.Helper;
using MysiePysieMVC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MysiePysieMVC.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            IEnumerable<UserModel> users = GetHelper<UserModel>.GetAll(Globals.USER_API_LINK);
            users = users.OrderBy(s => s.id);
            if (users == null)
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd serwera - brak danych w bazie.");
            }

            return View(users);
        }

        public ActionResult Delete(int userId)
        {
            DeleteHelper.DeleteEntity(Globals.USER_API_LINK, userId);

            return RedirectToAction("Index");
        }

        public ActionResult LoginUser()
        {
            return View("Login");
        }
        public ActionResult Login(string username, string password)
        {
            UserAuth user = new UserAuth()
            { 
                username = username,
                password = password
            };

            var auth = PostHelper<UserAuth>.GetToken(Globals.USER_VALIDATE_API_LINK, user);
            if (auth != null && auth != "Wrong password or username")
            {
                Globals.Token = auth;
            }

            return RedirectToAction("Index","Home");
        }

        public ActionResult Logout()
        {
            Globals.Token = null;
            return RedirectToAction("Index", "Home");
        }
    }
}