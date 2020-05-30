using MysiePysieMVC.Helper;
using MysiePysieMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MysiePysieMVC.Controllers
{
    public class SubjectsController : Controller
    {
        // GET: Subjects
        public ViewResult Index()
        {
            IEnumerable<Subject> subjects = GetHelper<Subject>.GetAll("subjects");
            subjects = subjects.OrderBy(s => s.id);
            if (subjects == null) ModelState.AddModelError(string.Empty, "Wystąpił błąd serwera - brak danych w bazie.");
            return View(subjects);
        }

        public ViewResult New()
        {
            ViewData["TeacherList"] = GetTeachersList();
            return View("AddSubject");
        }

        public ViewResult Edit(int subjectId)
        {
            ViewData["TeacherList"] = GetTeachersList();
            Subject subject = GetHelper<Subject>.GetById(Globals.SUBJECTS_API_LINK, subjectId);
            return View("EditSubject", subject);
        }

        public ActionResult DeleteSubject(int subjectId)
        {
            DeleteHelper.DeleteEntity(Globals.SUBJECTS_API_LINK, subjectId);
            return RedirectToAction("Index");
        }

        public ActionResult AddSubject(Subject subject)
        {
            Teacher teacher = GetHelper<Teacher>.GetById(Globals.TEACHERS_API_LINK, subject.teacher.id);
            subject.teacher = teacher;
            PostHelper<Subject>.PostEntity(Globals.SUBJECTS_API_LINK, subject);

            return RedirectToAction("Index");
        }

        public ActionResult EditSubject(Subject subject)
        {
            Teacher teacher = GetHelper<Teacher>.GetById(Globals.TEACHERS_API_LINK, subject.teacher.id);
            subject.teacher = teacher;
            UpdateHelper<Subject>.UpdateEntity(Globals.SUBJECTS_API_LINK, subject);

            return RedirectToAction("Index");
        }

        private IEnumerable<SelectListItem> GetTeachersList()
        {
            var teachersList = GetHelper<Teacher>.GetAll(Globals.TEACHERS_API_LINK).ToList();

            var teachers = (from teach in teachersList
                            select new SelectListItem
                            {
                                Text = teach.forename + " " + teach.surname,
                                Value = teach.id.ToString()
                            }).ToList();

            IList<SelectListItem> items = new List<SelectListItem>();

            foreach (var teacher in teachers)
            {
                items.Add(teacher);
            }

            return items;
        }
    }
}