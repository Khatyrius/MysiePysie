using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MysiePysieService.Data;
using MysiePysieService.DTO;
using MysiePysieService.Models;

namespace MysiePysieService.Controllers
{
    [Route("teachers")]
    public class TeacherController : Controller
    {
        private ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        // GET: /teachers
        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
            var teachers = await _teacherRepository.GetAll();

            if (teachers != null)
            {
                return Ok(teachers);
            }

            return NotFound("Brak nauczycieli do wyświetlenia");
        }

        // GET teachers/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeacher(int id)
        {
            var teacher = await _teacherRepository.GetById(id);
            if (teacher != null)
            {
                return Ok(teacher);
            }

            return NotFound("Teacher not found");
        }

        // POST /students
        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody]TeacherDTO teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            if (teacher.id != 0)
            {
                return BadRequest("Id is auto incremented");
            }

            Teacher newTeacher = new Teacher()
            {
                forename = teacher.forename,
                surname = teacher.surname,
                age = teacher.age
            };

            bool created = await _teacherRepository.Add(newTeacher);

            if (created)
            {
                return Created("", newTeacher);
            }

            return Conflict();
        }

        // PUT /teachers/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromBody]TeacherDTO teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            Teacher updatedTeacher = new Teacher()
            {
                id = id,
                forename = teacher.forename,
                surname = teacher.surname,
                age = teacher.age,
            };

            bool updated = await _teacherRepository.Update(updatedTeacher);

            if (updated)
            {
                return Created("", updatedTeacher);
            }

            return Conflict();
        }

        // DELETE teachers
        [HttpDelete]
        public async Task<IActionResult> DeleteStudent([FromBody]TeacherDTO teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            Teacher deleteTeacher = new Teacher()
            {
                id = teacher.id,
                forename = teacher.forename,
                surname = teacher.surname,
                age = teacher.age,
            };

            bool deleted = await _teacherRepository.Delete(deleteTeacher);

            if (!deleted)
            {
                return NotFound("Teacher not found");
            }

            return Ok("Teacher deleted");

        }
    }
}
