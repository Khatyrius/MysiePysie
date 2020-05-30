using MysiePysieMVC.Helper;
using MysiePysieMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MysiePysieMVC.Controllers
{
    public class TeachersController : Controller
    {
        // GET: Teachers

        public ViewResult Index()
        {
            IEnumerable<Teacher> teachers = GetHelper<Teacher>.GetAll("teachers");
            teachers = teachers.OrderBy(t => t.id);
            if (teachers == null)
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd serwera - brak danych w bazie.");
            }

            return View(teachers);
        }

        public ViewResult New()
        {
            return View("AddTeacher");
        }

        public ViewResult Edit(int teacherId)
        {
            Teacher teacher = GetHelper<Teacher>.GetById(Globals.TEACHERS_API_LINK, teacherId);
            return View("EditTeacher", teacher);
        }

        public ActionResult DeleteTeacher(int teacherId)
        {
            DeleteHelper.DeleteEntity(Globals.TEACHERS_API_LINK, teacherId);
            return RedirectToAction("Index");
        }

        public ActionResult AddTeacher(Teacher teacher)
        {
            PostHelper<Teacher>.PostEntity(Globals.TEACHERS_API_LINK, teacher);

            return RedirectToAction("Index");
        }

        public ActionResult EditTeacher(Teacher teacher)
        {
            UpdateHelper<Teacher>.UpdateEntity(Globals.TEACHERS_API_LINK, teacher);

            return RedirectToAction("Index");
        }
    }
}