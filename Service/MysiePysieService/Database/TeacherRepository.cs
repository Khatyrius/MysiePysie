using Microsoft.EntityFrameworkCore;
using MysiePysieService.Database;
using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MysiePysieService.Data
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly DataContext _context;

        public TeacherRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Teacher teacher)
        {
            if (!CheckIfExists(teacher.id))
            {
                await _context.Teachers.AddAsync(teacher);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Teacher teacher)
        {
                if (CheckIfExists(teacher.id))
                {
                    _context.Teachers.Remove(_context.Teachers.Single(t => t.id == teacher.id));
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
        }

        public async Task<bool> Delete(int id)
        {
                if (CheckIfExists(id))
                {
                    _context.Teachers.Remove(_context.Teachers.Single(t => t.id == id));
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
        }

        public async Task<List<Teacher>> GetAll()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetById(int id)
        {
            return await _context.Teachers.FirstOrDefaultAsync(t => t.id == id);
        }

        public async Task<bool> Update(Teacher teacher)
        {
                if (CheckIfExists(teacher.id) && !CheckIfExists(teacher))
                {
                    _context.Teachers.Update(teacher);
                    await _context.SaveChangesAsync();
                    return true;
                }

                return false;
        }

        public bool CheckIfExists(int id)
        {
            return _context.Teachers.Any(t => t.id == id);
        }

        public bool CheckIfExists(Teacher entity)
        {
            var query = from teacher in _context.Teachers
                        where teacher.surname == entity.surname && teacher.forename == entity.forename
                        select teacher;

            return query.Any();
                        
        }

        public int GetLast()
        {
            var teachers = _context.Teachers.ToListAsync();
            int id = teachers.Result.Select(x => x.id).Max();
            return id;
        }
    }
}