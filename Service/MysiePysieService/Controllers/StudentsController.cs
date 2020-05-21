using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MysiePysieService.Data;
using MysiePysieService.DTO;
using MysiePysieService.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MysiePysieService.Controllers
{
    [Route("students")]
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
            
            if(students != null)
            {
                return Ok(students);
            }

            return NotFound("Brak studentów do wyświetlenia");
        }

        // GET students/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> getStudent(int id)
        {
            var student = await _studentRepository.GetById(id);
            if(student != null)
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
            {
                return BadRequest("Invalid input");
            }

            if(student.id != 0)
            {
                return BadRequest("Id is auto incremented");
            }

            Student newStudent = new Student()
            {
                forename = student.forename,
                surname = student.surname,
                age = student.age,
                status = student.status
            };

            bool created = await _studentRepository.Add(newStudent);

            if (created)
            {
                return Created("", newStudent);
            }
            
            return Conflict();
        }

        // PUT /students/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateStudent(int id,[FromBody]StudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            Student updatedStudent = new Student()
            {
                id = id,
                forename = student.forename,
                surname = student.surname,
                age = student.age,
                status = student.status
            };

            bool updated = await _studentRepository.Update(updatedStudent);

            if (updated)
            {
                return Created("", updatedStudent);
            }

            return Conflict();
        }

        // DELETE students
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent([FromBody]StudentDTO student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            Student deleteStudent = new Student()
            {
                id = student.id,
                forename = student.forename,
                surname = student.surname,
                age = student.age,
                status = student.status
            };

            bool deleted = await _studentRepository.Delete(deleteStudent);

            if (!deleted)
            {
                return NotFound("Student not found");
            }

            return Ok("Student deleted");

        }
    }
}
