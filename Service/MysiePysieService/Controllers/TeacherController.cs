using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MysiePysieService.Data;
using MysiePysieService.DTO;
using MysiePysieService.Models;

namespace MysiePysieService.Controllers
{
    [Authorize]
    [Route("teachers")]
    [ApiController]
    public class TeacherController : Controller
    {
        private ITeacherRepository _teacherRepository;

        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        // GET: /teachers
        [HttpGet]
        public async Task<IActionResult> GetTeachers()
        {
            var teachers = await _teacherRepository.GetAll();

            if (teachers.Count != 0)
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

        // POST /teachers
        [HttpPost]
        public async Task<IActionResult> AddTeacher([FromBody]TeacherDTO teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            if (teacher.age < 1)
                return BadRequest("Invalid input");

            Teacher newTeacher = new Teacher()
            {
                forename = teacher.forename,
                surname = teacher.surname,
                age = teacher.age
            };

            if (_teacherRepository.CheckIfExists(newTeacher))
            {
                return Conflict("Teacher already exists");
            }

            bool created = await _teacherRepository.Add(newTeacher);

            if (created)
            {
                return Created("", newTeacher);
            }

            return Conflict();
        }

        // PUT /teachers
        [HttpPut]
        public async Task<IActionResult> UpdateTeacher([FromBody]TeacherDTO teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            if (teacher.age < 1)
                return BadRequest("Invalid input");

            Teacher updatedTeacher = new Teacher()
            {
                id = teacher.id,
                forename = teacher.forename,
                surname = teacher.surname,
                age = teacher.age,
            };

            bool updated = await _teacherRepository.Update(updatedTeacher);

            if (updated)
            {
                return Ok(updatedTeacher);
            }

            return Conflict();
        }

        // DELETE teachers
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeacher(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            bool deleted = await _teacherRepository.Delete(id.Value);

            if (deleted)
            {
                return Ok("Teacher deleted");
            }

            return NotFound("Teacher not found");
        }

        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            var id = _teacherRepository.GetLast();
            if (id != 0)
            {
                return Ok(id);
            }

            return NotFound();
        }
    }
}

