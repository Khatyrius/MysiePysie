using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Moq;
using MysiePysieService.Controllers;
using MysiePysieService.Database;
using MysiePysieService.DTO;
using MysiePysieService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MysiePysieService.Test.UnitTests.ControllersTests
{
    class UserControllerTest
    {
        [Test]
        public async Task Get_User_Token()
        {
            //Given
            UserAuthenticationDTO user = new UserAuthenticationDTO()
            {
                username = "test",
                password = "test",
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Validate(user.username, user.password)).ReturnsAsync(true);
            userRepositoryMock.Setup(x => x.GetByUsername(It.IsAny<string>())).ReturnsAsync(new User
            {
                username = "test",
                password = "test",
                userStatus = 1
            });
            //When
            UserController userController = new UserController(userRepositoryMock.Object);
            var result = await userController.ValidateUser(user) as ObjectResult;
            var resultToken = result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.IsFalse(string.IsNullOrEmpty((string)resultToken));
        }

        [Test]
        public async Task Add_New_User()
        {
            //Given
            UserDTO user = new UserDTO()
            {
                firstname = "firstname",
                lastname = "lastname",
                email = "email",
                password = "password",
                phone = "phone",
                username = "username",
                userStatus = 1
            };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Add(It.IsAny<User>())).ReturnsAsync(true);

            //When
            var controller = new UserController(userRepositoryMock.Object);
            var result = await controller.AddUser(user) as ObjectResult;
            var actualResult = (User)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual(user.firstname, actualResult.firstname);
            Assert.AreEqual(user.lastname, actualResult.lastname);
            Assert.AreEqual(user.email, actualResult.email);
            Assert.AreEqual(user.password, actualResult.password);
            Assert.AreEqual(user.phone, actualResult.phone);
            Assert.AreEqual(user.username, actualResult.username);
            Assert.AreEqual(user.userStatus, actualResult.userStatus);
        }

        [Test]
        public async Task Get_User_List()
        {
            var users = new List<User>()
            {
                new User()
                {
                    firstname = "firstname",
                    lastname = "lastname",
                    email = "email",
                    password = "password",
                    phone = "phone",
                    username = "username",
                 userStatus = 1
                },
                new User()
                {
                    firstname = "tfirstname",
                    lastname = "tlastname",
                    email = "temail",
                    password = "tpassword",
                    phone = "tphone",
                    username = "tusername",
                    userStatus = 2
                }
            };


            //Given
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(users);

            //When
            var controller = new UserController(userRepositoryMock.Object);
            var result = await controller.GetUsers() as ObjectResult;
            var actualResult = (List<User>)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(users[0].firstname, actualResult[0].firstname);
            Assert.AreEqual(users[0].lastname, actualResult[0].lastname);
            Assert.AreEqual(users[0].email, actualResult[0].email);
            Assert.AreEqual(users[0].password, actualResult[0].password);
            Assert.AreEqual(users[0].phone, actualResult[0].phone);
            Assert.AreEqual(users[0].username, actualResult[0].username);
            Assert.AreEqual(users[0].userStatus, actualResult[0].userStatus);

            Assert.AreEqual(users[1].firstname, actualResult[1].firstname);
            Assert.AreEqual(users[1].lastname, actualResult[1].lastname);
            Assert.AreEqual(users[1].email, actualResult[1].email);
            Assert.AreEqual(users[1].password, actualResult[1].password);
            Assert.AreEqual(users[1].phone, actualResult[1].phone);
            Assert.AreEqual(users[1].username, actualResult[1].username);
            Assert.AreEqual(users[1].userStatus, actualResult[1].userStatus);
        }

        [Test]
        public async Task Get_User_By_Id()
        {
            //Given
            int userId = 5;

            User user = new User()
            {
                id = userId,
                firstname = "test",
                lastname = "test",
                email = "test@mail",
                userStatus = 1,
                password = "test",
                phone = "5038205829",
                username = "test"
            };


            var userMockrepository = new Mock<IUserRepository>();
            userMockrepository.Setup(x => x.GetById(userId)).ReturnsAsync(user);

            //When
            var controller = new UserController(userMockrepository.Object);
            var result = await controller.GetUser(userId) as ObjectResult;
            var actualResult = (User)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(userId, actualResult.id);
            Assert.AreEqual(user.firstname, actualResult.firstname);
            Assert.AreEqual(user.lastname, actualResult.lastname);
            Assert.AreEqual(user.email, actualResult.email);
            Assert.AreEqual(user.password, actualResult.password);
            Assert.AreEqual(user.phone, actualResult.phone);
            Assert.AreEqual(user.username, actualResult.username);
            Assert.AreEqual(user.userStatus, actualResult.userStatus);
        }

        [Test]
        public async Task Delete_User()
        {
            //Given
            int userId = 3;
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Delete(userId)).ReturnsAsync(true);

            //When
            UserController controller = new UserController(userRepositoryMock.Object);
            var result = await controller.DeleteUser(userId) as ObjectResult;
            var actualResult = result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task Get_Last_Id()
        {
            //Given
            int userId = 3;
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.GetLast()).Returns(userId);

            //When
            UserController controller = new UserController(userRepositoryMock.Object);
            var result = await controller.GetLastId() as ObjectResult;
            var actualResult = result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(userId, actualResult);
        }
    }
}
