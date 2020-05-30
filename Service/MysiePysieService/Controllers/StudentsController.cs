using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysiePysieService.Data;
using MysiePysieService.DTO;
using MysiePysieService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MysiePysieService.Controllers
{
    [Authorize]
    [Route("students")]
    [ApiController]
    public class StudentsController : Controller
    {
        private IStudentRepository _studentRepository;

        public StudentsController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        // GET: /students
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            IList<Student> students = await _studentRepository.GetAll();

            if (students.Count != 0)
            {
                return Ok(students);
            }

            return NotFound("Brak studentów do wyświetlenia");
        }

        // GET students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStudent(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            var student = await _studentRepository.GetById(id.Value);
            if (student != null)
            {
                return Ok(student);
            }

            return NotFound("Student not found");
        }

        // POST /students
        [HttpPost]
        public async Task<IActionResult> AddStudent([FromBody]StudentDTO student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input");

            if (student.age < 1)
                return BadRequest("Invalid input");

            Student newStudent = new Student()
            {
                forename = student.forename,
                surname = student.surname,
                age = student.age,
                status = student.status,
                @class = student.@class
            };

            if (_studentRepository.CheckIfExists(newStudent))
            {
                return Conflict("Student already exists");
            }

            bool created = await _studentRepository.Add(newStudent);

            if (created)
            {
                return Created("", newStudent);
            }

            return Conflict();
        }

        // PUT /students
        [HttpPut]
        public async Task<IActionResult> UpdateStudent([FromBody]StudentDTO student)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid input");

            if (student.age < 1)
                return BadRequest("Invalid input");

            Student updatedStudent = new Student()
            {
                id = student.id,
                forename = student.forename,
                surname = student.surname,
                age = student.age,
                status = student.status,
                @class = student.@class
            };

            bool updated = await _studentRepository.Update(updatedStudent);

            if (updated)
            {
                return Ok(updatedStudent);
            }

            return Conflict();
        }

        // DELETE students/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudent(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            bool deleted = await _studentRepository.Delete(id.Value);

            if (deleted)
            {
                return Ok("Student deleted");
            }

            return NotFound("Student not found");
        }

        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId() {
            var id = _studentRepository.GetLast();
                if (id != 0)
                {
                    return Ok(id);
                }

            return NotFound();
            }
        }
}