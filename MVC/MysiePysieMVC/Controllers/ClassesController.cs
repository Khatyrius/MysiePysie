using MysiePysieMVC.Helper;
using MysiePysieMVC.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MysiePysieMVC.Controllers
{
    public class ClassesController : Controller
    {
        // GET: Classes
        public ViewResult Index()
        {
            IEnumerable<Class> classes = GetHelper<Class>.GetAll("classes");
            classes = classes.OrderBy(c => c.id);
            if (classes == null)
            {
                ModelState.AddModelError(string.Empty, "Wystąpił błąd serwera - brak danych w bazie.");
            }
            return View(classes);
        }

        public ViewResult ViewStudents(int classId, string className)
        {
            IEnumerable<Student> students = GetHelper<Student>.GetAll("students").Where(s => s.@class != null && s.@class.id == classId);
            SetClassName(classId);
            ViewData["classId"] = classId;
            return View("Students", students);
        }

        public ViewResult New()
        {
            return View("AddClass");
        }

        public ActionResult AddClass(Class @class)
        {
            @class.students = new List<Student>();
            PostHelper<Class>.PostEntity(Globals.CLASSES_API_LINK, @class);

            return RedirectToAction("Index");
        }

        public ViewResult Edit(int classId)
        {
            Class @class = GetHelper<Class>.GetById(Globals.CLASSES_API_LINK, classId);
            return View("EditClass", @class);
        }

        public ActionResult EditClass(Class @class)
        {
            Class oldClass = GetHelper<Class>.GetById(Globals.CLASSES_API_LINK, @class.id);
            var oldName = oldClass.name;
            oldClass.name = @class.name;
            UpdateHelper<Class>.UpdateEntity(Globals.CLASSES_API_LINK, oldClass);

            TempData["message"] = "Pomyślnie dokonano edycji klasy " + oldName + " o id: " + @class.id;
            return RedirectToAction("Index");
        }

        public ActionResult DeleteClass(int classId)
        {
            Class @class = GetHelper<Class>.GetById(Globals.CLASSES_API_LINK, classId);
            foreach (Student student in @class.students)
            {
                GetHelper<Class>.Remove(Globals.CLASSES_REMOVE_API_LINK, classId, student.id);
            }

            DeleteHelper.DeleteEntity(Globals.CLASSES_API_LINK, classId);

            TempData["message"] = "Pomyślnie usunięto klasę " + @class.name + " o id: " + classId;
            return RedirectToAction("Index");
        }

        public ActionResult AddStudent(int classId)
        {
            ViewData["classId"] = classId;
            SetClassName(classId);
            ViewData["StudentList"] = GetStudentsList();
            return View("AddStudentToClass");
        }

        public ActionResult AddStudentToClass(int classId, int studentId)
        {
            GetHelper<Class>.Add(Globals.CLASSES_ADD_API_LINK, classId, studentId);

            var className = GetHelper<Class>.GetById(Globals.CLASSES_API_LINK, classId).name;
            Student student = GetHelper<Student>.GetById(Globals.STUDENTS_API_LINK, studentId);
            var studentName = student.forename + " " + student.surname;
            TempData["message"] = "Pomyślnie dodano studenta " + studentName + " do klasy " + className;

            return RedirectToAction("Index");
        }

        public ActionResult RemoveStudentFromClass(int classId, int studentId)
        {
            GetHelper<Class>.Remove(Globals.CLASSES_REMOVE_API_LINK, classId, studentId);

            var className = GetHelper<Class>.GetById(Globals.CLASSES_API_LINK, classId).name;
            Student student = GetHelper<Student>.GetById(Globals.STUDENTS_API_LINK, studentId);
            var studentName = student.forename + " " + student.surname;
            TempData["message"] = "Pomyślnie usunięto studenta " + studentName + " z klasy " + className;

            return RedirectToAction("Index");
        }

        private void SetClassName(int classId)
        {
            ViewData["className"] = GetHelper<Class>.GetById(Globals.CLASSES_API_LINK, classId).name;
        }

        private IEnumerable<SelectListItem> GetStudentsList()
        {
            var studentsList = GetHelper<Student>.GetAll(Globals.STUDENTS_API_LINK).ToList();

            var students = (from stud in studentsList
                            select new SelectListItem
                            {
                                Text = stud.forename + " " + stud.surname,
                                Value = stud.id.ToString()
                            }).ToList();

            IList<SelectListItem> items = new List<SelectListItem>();

            foreach (var student in students)
            {
                items.Add(student);
            }

            return items;
        }
    }
}