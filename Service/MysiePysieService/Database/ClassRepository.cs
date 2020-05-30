using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using MysiePysieService.Controllers;
using MysiePysieService.Database;
using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MysiePysieService.Data
{
    public class ClassRepository : IClassRepository
    {
        private readonly DataContext _context;

        public ClassRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(Class tEntity)
        {
            if(tEntity.students != null)
                tEntity.students = PopulateClass(tEntity.students);

            if (!CheckIfExists(tEntity) && !CheckIfExists(tEntity.id))
            {
                await _context.Classes.AddAsync(tEntity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task<bool> Delete(int id)
        {
            if (CheckIfExists(id))
            {
                _context.Classes.Remove(_context.Classes.Single(c => c.id == id));
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Class tEntity)
        {
            if (CheckIfExists(tEntity))
            {
                _context.Classes.Remove(tEntity);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> Update(Class tEntity)
        {
            if(tEntity.students != null)
                tEntity.students = PopulateClass(tEntity.students);

            if (CheckIfExists(tEntity.id))
            {
                _context.Classes.Update(tEntity);
                await _context.SaveChangesAsync();

                return true;
            }

            return false;
        }

        public async Task<List<Class>> GetAll()
        {
            return await _context.Classes.Include(c => c.students).ToListAsync();
        }

        public async Task<Class> GetById(int id)
        {
            return await _context.Classes.Include(c => c.students).FirstOrDefaultAsync(c => c.id == id);
        }
        public bool CheckIfExists(int id)
        {
            return _context.Classes.Any(c => c.id == id);
        }

        public bool CheckIfExists(Class entity)
        {
            var query = from cl in _context.Classes
                        where cl.name == entity.name
                        select cl;


            return query.Any();
        }
        private ICollection<Student> PopulateClass(ICollection<Student> students)
        {
            List<Student> populatedClass = new List<Student>();

            foreach (Student student in students)
            {
                populatedClass.Add(CheckIfCreateNewStudent(student));
            }

            return populatedClass;
        }
        private Student CheckIfCreateNewStudent(Student student)
        {
            var query = from stud in _context.Students
                        where stud.forename == student.forename &&
                              stud.surname == student.surname &&
                              stud.age == student.age &&
                              stud.status == student.status
                        select stud;

            if(query.Any())
            {
                return query.First();
            }

            student.id = 0;

            return student;
        }

        public async Task<bool> AddStudentToClass(int classId, int studentId)
        {
            Class @class = GetById(classId).Result;
            if (CheckIfExists(@class))
            {
                List<Student> students = @class.students.ToList();
                Student student = await _context.Students.FirstOrDefaultAsync(s => s.id == studentId);
                if (student == null)
                {
                    return false;
                } 

                students.Add(student);
                @class.students = students;
                await Update(@class);

                return true;
            }

            return false;

        }

        public async Task<bool> RemoveStudentFromClass(int classId, int studentId)
        {
            Class @class = GetById(classId).Result;
            if (CheckIfExists(@class))
            {
                List<Student> students = @class.students.ToList();
                Student student = await _context.Students.FirstOrDefaultAsync(s => s.id == studentId);
                students.Remove(student);
                @class.students = students;
                await Update(@class);

                return true;
            }

            return false;
        }

        public int GetLast()
        {
            var classes = _context.Classes.ToListAsync();
            int id = classes.Result.Select(x => x.id).Max();
            return id;
        }
    }
}