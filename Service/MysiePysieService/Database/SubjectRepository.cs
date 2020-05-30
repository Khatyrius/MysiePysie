using Microsoft.EntityFrameworkCore;
using MysiePysieService.Database;
using MysiePysieService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Net.Cache;
using System.Threading.Tasks;

namespace MysiePysieService.Data
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly DataContext _context;

        public SubjectRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> Add(Subject tEntity)
        {
           tEntity.teacher = await CheckIfCreateNewTeacher(tEntity.teacher);

            if (!CheckIfExists(tEntity.id) && !CheckIfExists(tEntity))
            {
                await _context.Subjects.AddAsync(tEntity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(int id)
        {
            if (CheckIfExists(id))
            {
                _context.Subjects.Remove(_context.Subjects.Single(s => s.id == id));
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<bool> Delete(Subject tEntity)
        {
            if (CheckIfExists(tEntity.id))
            {
                _context.Subjects.Remove(_context.Subjects.Single(s => s.id == tEntity.id));
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public async Task<List<Subject>> GetAll()
        {
            return await _context.Subjects.Include(s => s.teacher).ToListAsync();
        }

        public async Task<Subject> GetById(int id)
        {
                return await _context.Subjects.Include(s => s.teacher).FirstOrDefaultAsync(s => s.id == id);
        }

        public async Task<bool> Update(Subject tEntity)
        {
            tEntity.teacher = await CheckIfCreateNewTeacher(tEntity.teacher);

            if (CheckIfExists(tEntity.id) && !CheckIfExists(tEntity))
            {
                _context.Subjects.Update(tEntity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public bool CheckIfExists(int id)
        {
            return _context.Subjects.Any(s => s.id == id);
        }

        public bool CheckIfExists(Subject entity)
        {
            var query = from sub in _context.Subjects
                        where sub.ECTSpoints == entity.ECTSpoints &&
                              sub.name == entity.name &&
                              sub.teacher.forename == entity.teacher.forename &&
                              sub.teacher.surname == entity.teacher.surname &&
                              sub.teacher.age == entity.teacher.age
                        select sub;

            var query2 = from sub in _context.Subjects
                         where sub.name == entity.name
                         select sub;

            return (query.Any() && query2.Any());
        }

        private async Task<Teacher> CheckIfCreateNewTeacher(Teacher teacher)
        {
            var query = from teach in _context.Teachers
                        where teach.forename == teacher.forename &&
                              teach.surname == teacher.surname &&
                              teach.age == teacher.age
                        select teach;

            if (await query.AnyAsync())
            {
                teacher = await query.FirstAsync();
                return teacher;
            }

            teacher.id = 0;
            return teacher;
        }

        public int GetLast()
        {
            var subjects = _context.Subjects.ToListAsync();
            int id = subjects.Result.Select(x => x.id).Max();
            return id;
        }
    }
}