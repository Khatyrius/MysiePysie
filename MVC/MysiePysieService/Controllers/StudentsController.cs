using Microsoft.Ajax.Utilities;
using Microsoft.AspNetCore.Mvc;
using MysiePysieService.Helper;
using MysiePysieService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using HttpGetAttribute = System.Web.Http.HttpGetAttribute;
using RouteAttribute = System.Web.Http.RouteAttribute;

namespace MysiePysieService.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students
  
        public ActionResult Index()
        {
            IEnumerable<Student> students = GetHelper<Student>.GetAll("students");
            if(students == null) {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd serwera - brak danych w bazie.");
            }

            return View(students);
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