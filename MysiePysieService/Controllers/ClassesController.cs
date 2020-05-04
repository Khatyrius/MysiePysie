using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MysiePysieService.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult New()
        {
            return View("ClassForm");
        }

        public ViewResult Edit()
        {
            return View("ClassForm");
        }
    }
}