using MysiePysieService.Helper;
using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MysiePysieService.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers

        public ViewResult Index()
        {
            IEnumerable<Teacher> teachers = GetHelper<Teacher>.GetAll("teachers");
            if (teachers == null)
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd serwera - brak danych w bazie.");
            }

            return View(teachers);
        }

        public ViewResult New()
        {
            return View("TeacherForm");
        }

        public ViewResult Edit()
        {
            return View("TeacherForm");
        }
    }
}