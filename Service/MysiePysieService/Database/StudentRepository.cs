using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.EntityFrameworkCore;
using MysiePysieService.Database;
using MysiePysieService.Models;

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
            await _context.Students.AddAsync(student);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(Student student)
        {
            try
            {
                var studentExists = await EntityFrameworkQueryableExtensions.FirstAsync(_context.Students, s => s.id == student.id);
                if (studentExists == null)
                {
                    return false;
                }

                _context.Students.Remove(_context.Students.Single(s => s.id == student.id));
                await _context.SaveChangesAsync();
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public async Task<bool> Update(Student student)
        {
            try
            {
                _context.Students.Update(student);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Student> GetById(int id)
        {
            try
            {
                return await EntityFrameworkQueryableExtensions.FirstAsync(_context.Students, x => x.id == id);
            }
            catch(Exception e)
            {
                return null;
            }
        }

        public async Task<List<Student>> GetAll()
        {
            return await _context.Students.ToListAsync();
        }
    }
}