using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MysiePysieService.Database;
using MysiePysieService.Models;

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
            await _context.Teachers.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Teacher teacher)
        {
            try
            {
                var teacherExists = await EntityFrameworkQueryableExtensions.FirstAsync(_context.Teachers, t => t.id == teacher.id);
                if (teacherExists == null)
                {
                    return false;
                }

                _context.Teachers.Remove(_context.Teachers.Single(t => t.id == teacher.id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public Task<List<Teacher>> FindBySubjectTaught(string subject)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Teacher>> GetAll()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<Teacher> GetById(int id)
        {
            return await EntityFrameworkQueryableExtensions.FirstAsync(_context.Teachers, x => x.id == id);
        }

        public async Task<bool> Update(Teacher teacher)
        {
            try
            {
                _context.Teachers.Update(teacher);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}