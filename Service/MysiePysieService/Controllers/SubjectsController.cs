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
    [Route("subjects")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {
        private ISubjectRepository _subjectRepository;

        public SubjectsController(ISubjectRepository subjectRepository)
        {
            _subjectRepository = subjectRepository;
        }
        [HttpGet]
        public async Task<IActionResult> GetSubjects()
        {
            IList<Subject> subjects = await _subjectRepository.GetAll();

            if (subjects.Count != 0)
            {
                return Ok(subjects);
            }

            return NotFound("Subjects not found");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubjectById(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            var subject = await _subjectRepository.GetById(id.Value);
            if(subject != null)
            {
                return Ok(subject);
            }

            return NotFound("Subject not found");
        }

        [HttpPost]
        public async Task<IActionResult> AddSubject([FromBody]SubjectDTO subject){
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            Subject newSubject = new Subject
            {
                name = subject.name,
                ECTSpoints = subject.ECTSpoints,
                teacher = subject.teacher
            };

            if (_subjectRepository.CheckIfExists(newSubject))
            {
                return Conflict("Subject already exists");
            }

            bool created = await _subjectRepository.Add(newSubject);

            if (created)
            {
                return Created("", newSubject);
            }

            return Conflict();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubject(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            var deleted = await _subjectRepository.Delete(id.Value);

            if (deleted)
            {
                return Ok("Subject deleted succesfuly");
            }

            return NotFound("Subject doesn't exist");
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSubject([FromBody]SubjectDTO subject)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            Subject updatedSubject = new Subject()
            {
                id = subject.id,
                name = subject.name,
                ECTSpoints = subject.ECTSpoints,
                teacher = subject.teacher
            };

            bool updated = await _subjectRepository.Update(updatedSubject);

            if (updated)
            {
                return Ok(updatedSubject);
            }

            return Conflict();
        }

        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            var id = _subjectRepository.GetLast();
            if (id != 0)
            {
                return Ok(id);
            }

            return NotFound();
        }
    }
}
