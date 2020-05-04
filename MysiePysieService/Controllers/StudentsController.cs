using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MysiePysieService.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult New()
        {
            return View("StudentForm");
        }

        public ViewResult Edit()
        {
            return View("StudentForm");
        }
    }
}