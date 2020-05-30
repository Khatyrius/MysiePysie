using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MysiePysieService.Data;
using MysiePysieService.DTO;
using MysiePysieService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MysiePysieService.Controllers
{
    [Authorize]
    [Route("classes")]
    [ApiController]
    public class ClassesController : ControllerBase
    {
        private IClassRepository _classRepository;

        public ClassesController(IClassRepository classRepository)
        {
            _classRepository = classRepository;
        }

        // GET: classes
        [HttpGet]
        public async Task<IActionResult> GetClasses()
        {
            IList<Class> classes = await _classRepository.GetAll();

            if (classes != null)
            {
                return Ok(classes);
            }

            return NotFound("Classes not found");
        }

        // GET: classes/id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetClass(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            var @class = await _classRepository.GetById(id.Value);

            if (@class == null)
            {
                return NotFound();
            }

            return Ok(@class);
        }

        // PUT: classes
        [HttpPut]
        public async Task<IActionResult> UpdateClass([FromBody]ClassDTO @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            Class updatedClass = new Class()
            {
                id = @class.id,
                name = @class.name,
                students = @class.students
            };

            bool updated = await _classRepository.Update(updatedClass);

            if (updated)
            {
                return Ok(updatedClass);
            }

            return Conflict("Class doesn't exist");
        }

        // POST: classes
        [HttpPost]
        public async Task<IActionResult> AddClass([FromBody]ClassDTO @class)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            Class newClass = new Class()
            {
                id = @class.id,
                name = @class.name,
                students = @class.students
            };

            if (_classRepository.CheckIfExists(newClass))
            {
                return Conflict("Class already exists");
            }

            bool created = await _classRepository.Add(newClass);

            if (created)
            {
                return Created("", newClass);
            }

            return Conflict();
        }

        // Add student to class
        [HttpGet("add/{classId}/{studentId}")]
        public async Task<IActionResult> AddStudentToClass(int? classId, int? studentId)
        {
            if (!classId.HasValue || !studentId.HasValue)
            {
                return BadRequest("Id must be given");
            }

            var added = await _classRepository.AddStudentToClass(classId.Value, studentId.Value);

            if (added)
                return Ok("Added student to class");

            return BadRequest("Student or class doesn't exist");
        }

        // Remove student from class
        [HttpGet("remove/{classId}/{studentId}")]
        public async Task<IActionResult> RemoveStudentFromClass(int? classId, int? studentId)
        {
            if (!classId.HasValue || !studentId.HasValue)
            {
                return BadRequest("Id must be given");
            }

            var added = await _classRepository.RemoveStudentFromClass(classId.Value, studentId.Value);

            if (added)
                return Ok("Removed student from class");

            return BadRequest("Student or class doesn't exist");
        }



        // DELETE: api/Classes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClass(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            var deleted = await _classRepository.Delete(id.Value);

            if (deleted)
            {
                return Ok("Class deleted");
            }

            return NotFound("Class not found");
        }

        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            var id = _classRepository.GetLast();
            if (id != 0)
            {
                return Ok(id);
            }

            return NotFound();
        }
    }
}

