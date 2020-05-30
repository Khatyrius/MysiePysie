using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using MysiePysieService.Database;
using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MysiePysieService.Data
{
    public class StudentRepository : IStudentRepository
    {
        private readonly DataContext _context;

        public StudentRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Student student)
        {
            if (!CheckIfExists(student.id))
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Student student)
        {
                if (CheckIfExists(student.id))
                {
                    _context.Students.Remove(_context.Students.Single(s => s.id == student.id));
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
        }

        public async Task<bool> Update(Student student)
        {
                if (CheckIfExists(student.id) && !CheckIfExists(student))
                {
                    _context.Students.Update(student);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
        }

        public async Task<Student> GetById(int id)
        {
                return await _context.Students.Include(s => s.@class).FirstOrDefaultAsync(s => s.id == id);
        }

        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.Include(s => s.@class).ToListAsync();
        }

        public int GetLast()
        {
            var students = _context.Students.ToListAsync();
            int id = students.Result.Select(x => x.id).Max();
            return id;
        }

        public async Task<bool> Delete(int id)
        {
                if (CheckIfExists(id))
                {
                    _context.Students.Remove(_context.Students.Single(s => s.id == id));
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
        }

        public bool CheckIfExists(int id)
        {
            return _context.Students.Any(e => e.id == id);
        }

        public bool CheckIfExists(Student entity)
        {
            var query = from st in _context.Students
                        where st.forename == entity.forename &&
                        st.surname == entity.surname &&
                        st.age == entity.age
                        select st;

            return query.Any();
        }
    }
}