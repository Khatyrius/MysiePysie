using MysiePysieMVC.Helper;
using MysiePysieMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MysiePysieMVC.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Students

        public ActionResult Index()
        {
            IEnumerable<Student> students = GetHelper<Student>.GetAll("students");
            students = students.OrderBy(s => s.id);
            if (students == null)
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd serwera - brak danych w bazie.");
            }

            return View(students);
        }

        public ViewResult New()
        {
            return View("AddStudent");
        }

        public ViewResult Edit(int studentId)
        {
            Student student = GetHelper<Student>.GetById(Globals.STUDENTS_API_LINK, studentId);
            return View("EditStudent", student);
        }

        public ActionResult DeleteStudent(int studentId)
        {
            DeleteHelper.DeleteEntity(Globals.STUDENTS_API_LINK, studentId);
            return RedirectToAction("Index");
        }

        public ActionResult AddStudent(Student student)
        {
            PostHelper<Student>.PostEntity(Globals.STUDENTS_API_LINK, student);

            return RedirectToAction("Index");
        }

        public ActionResult EditStudent(Student student)
        {
            UpdateHelper<Student>.UpdateEntity(Globals.STUDENTS_API_LINK, student);

            return RedirectToAction("Index");
        }
    }
}