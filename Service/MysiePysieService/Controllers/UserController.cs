using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MysiePysieService.Database;
using MysiePysieService.DTO;
using MysiePysieService.Models;
using SQLitePCL;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MysiePysieService.Controllers
{
    [Authorize]
    [Route("users")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        

        //GET : users
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            IList<User> users = await _userRepository.GetAll();

            if (users.Count != 0)
            {
                return Ok(users);
            }

            return NotFound("Brak użytkowników do wyświetlenia");
        }

        // GET users/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            var user = await _userRepository.GetById(id.Value);
            if (user != null)
            {
                return Ok(user);
            }

            return NotFound("User not found");
        }

        // POST /users
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody]UserDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            User newUser = new User()
            {
                id = user.id,
                firstname = user.firstname,
                lastname = user.lastname,
                email = user.email,
                username = user.username,
                password = user.password,
                phone = user.phone,
                userStatus = user.userStatus                
            };

            if (_userRepository.CheckIfExists(newUser))
            {
                return Conflict("User already exists");
            }

            bool created = await _userRepository.Add(newUser);

            if (created)
            {
                return Created("", newUser);
            }

            return Conflict();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> ValidateUser([FromBody]UserAuthenticationDTO userAuthentication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            User user = await _userRepository.GetByUsername(userAuthentication.username);

            if(user.userStatus == 0)
            {
                return BadRequest("User not allowed");
            }

            if (await _userRepository.Validate(userAuthentication.username, userAuthentication.password))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("VERY SECRET, MUCH HIDDEN");
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userAuthentication.username)
                }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                var tokenValue = tokenHandler.WriteToken(token);

                return Ok(tokenValue);
            }

            return BadRequest("Wrong password or username");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> LogoutUser()
        {
                return Ok("User logged out");
        }

        [HttpPost("fromList")]
        public async Task<IActionResult> CreateFromList([FromBody]UserListDTO users)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            List<User> newUsers = new List<User>();

            foreach(User user in users.users)
            {
                newUsers.Add(new User
                {
                    firstname = user.firstname,
                    lastname = user.lastname,
                    email = user.email,
                    username = user.username,
                    password = user.password,
                    phone = user.phone,
                    userStatus = user.userStatus
                });
            }
            await _userRepository.CreateUsersFromList(newUsers);
            return Ok(newUsers);
        }

        // PUT /users
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody]UpdateUserDataDTO user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid input");
            }

            User oldUser = await _userRepository.GetById(user.id);

            if (user.firstname != null) oldUser.firstname = user.firstname;


            if (user.lastname != null) oldUser.lastname = user.lastname;


            if (user.password != null) oldUser.password = user.password;


            if (user.phone != null) oldUser.phone = user.phone;

            if (user.userStatus != 0 && user.userStatus != oldUser.userStatus) oldUser.userStatus = user.userStatus;
 
            bool updated = await _userRepository.Update(oldUser);

            if (updated)
            {
                return Ok(oldUser);
            }

            return Conflict();
        }

        // DELETE users/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int? id)
        {
            if (!id.HasValue)
            {
                return BadRequest("Id must be given");
            }

            bool deleted = await _userRepository.Delete(id.Value);

            if (deleted)
            {
                return Ok("User deleted");
            }

            return NotFound("User not found");
        }

        [HttpGet("lastid")]
        public async Task<IActionResult> GetLastId()
        {
            var id = _userRepository.GetLast();
            if (id != 0)
            {
                return Ok(id);
            }

            return NotFound();
        }
    }
}

