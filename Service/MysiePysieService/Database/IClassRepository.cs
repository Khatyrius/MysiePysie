using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MysiePysieService.Data
{
    public interface IClassRepository : IGenericRepository<Class>
    {
        public Task<bool> AddStudentToClass(int classId, int studentId);
        public Task<bool> RemoveStudentFromClass(int classId, int studentId);
    }
}
