using MysiePysieService.Data;
using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MysiePysieService.Database
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task CreateUsersFromList(IEnumerable<User> users);
        Task<bool> Validate(string username, string password);
        Task<User> GetByUsername(string username);
    }
}
