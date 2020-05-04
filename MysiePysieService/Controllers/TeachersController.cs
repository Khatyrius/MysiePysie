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
            return View();
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