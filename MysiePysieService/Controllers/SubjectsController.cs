using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MysiePysieService.Controllers
{
    public class SubjectsController : Controller
    {
        // GET: Subjects
        public ViewResult Index()
        {
            return View();
        }

        public ViewResult New()
        {
            return View("SubjectForm");
        }

        public ViewResult Edit()
        {
            return View("SubjectForm");
        }
    }
}