using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using MysiePysieService.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace MysiePysieService.Database
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<bool> Add(User tEntity)
        {
            if(!CheckIfExists(tEntity.id) && !CheckIfExists(tEntity))
            {
                await _context.AddAsync(tEntity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        public async Task CreateUsersFromList(IEnumerable<User> users)
        {
            foreach(User user in users)
            {
                if (user == null)
                    continue;

                if (!CheckIfExists(user))
                {
                    await _context.AddAsync(user);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<bool> Delete(int id)
        {
            if (CheckIfExists(id))
            {
                _context.Users.Remove(_context.Users.Single(u => u.id == id));
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<bool> Delete(User tEntity)
        {
            if (CheckIfExists(tEntity))
            {
                _context.Users.Remove(tEntity);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<List<User>> GetAll()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.id == id);
        }

        public async Task<User> GetByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.username == username);
        }

        public async Task<bool> Update(User tEntity)
        {
            if (CheckIfExists(tEntity.id))
            {
                _context.Users.Update(tEntity);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public bool CheckIfExists(int id)
        {
            return _context.Users.Any(u => u.id == id);
        }

        public bool CheckIfExists(User entity)
        {
            var searchByEmail = from user in _context.Users
                        where user.email == entity.email
                        select user;

            var serachByUsername = from user in _context.Users
                                where user.username == entity.username
                                select user;

            if (searchByEmail.Any() || serachByUsername.Any())
            {
                return true;
            }

            return false;
        }

        public async Task<bool> Validate(string username, string password)
        {
            var query = from user in _context.Users
                        where user.username == username &&
                              user.password == password
                        select user;

            if(await query.AnyAsync())
            {
                return true;
            }

            return false;
        }

        public int GetLast()
        {
            var users = _context.Users.ToListAsync();
            int id = users.Result.Select(x => x.id).Max();
            return id;
        }
    }
}
