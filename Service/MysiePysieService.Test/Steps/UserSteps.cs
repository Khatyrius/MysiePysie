using Gherkin.Ast;
using Microsoft.EntityFrameworkCore.Internal;
using MysiePysieService.DTO;
using MysiePysieService.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MysiePysieService.Test.Steps
{
    [Binding]
    [Scope(Feature = "User")]
    public class UserSteps:BaseSteps
    {
        UserDTO _user;
        int _userId;
        List<User> _users = new List<User>();

        [Given(@"I create a new user")]
        public void GivenICreateANewUser(Table table)
        {
            _user = table.CreateInstance<UserDTO>();
        }


        [Given(@"There's an existing user in database")]
        public void GivenThereSAnExistingUserInDatabase(Table table)
        {
            var user = table.CreateInstance<UserDTO>();
            var uri = "http://localhost:8080/users";
            var userJson = JsonConvert.SerializeObject(user);
            var result = _client.PostAsync(uri, new StringContent(userJson, Encoding.UTF8, "application/json")).Result;

            User userToList = JsonConvert.DeserializeObject<User>(result.Content.ReadAsStringAsync().Result);
            _users.Add(userToList);
        }
        
        [Given(@"i have the clients id")]
        public void GivenIHaveTheClientsId()
        {
            if (_users.Any())
            {
                _userId = _users.First().id;
            }
            else
                _userId = 0;
        }
        
        [When(@"The client puts a request for account creation")]
        public void WhenTheClientPutsARequestForAccountCreation()
        {
            var uri = "http://localhost:8080/users";
            var user = JsonConvert.SerializeObject(_user);
            var result = _client.PostAsync(uri, new StringContent(user, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["userObject"] = result.Content.ReadAsStringAsync().Result;

            User userToList = JsonConvert.DeserializeObject<User>(result.Content.ReadAsStringAsync().Result);
            _users.Add(userToList);
        }
        
        [When(@"The client puts a request for a login")]
        public void WhenTheClientPutsARequestForALogin()
        {
            UserAuthenticationDTO userAuth = new UserAuthenticationDTO
            {
                username = _users.First().username,
                password = _users.First().password
            };

            var user = JsonConvert.SerializeObject(userAuth);
            var uri = "http://localhost:8080/users/login";
            var result = _client.PostAsync(uri, new StringContent(user, Encoding.UTF8, "application/json")).Result;
            var token = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = result.StatusCode;
            ScenarioContext.Current["token"] = token;
        }
        
        [When(@"The client puts a request for user update")]
        public void WhenTheClientPutsARequestForUserUpdate(Table table)
        {
            var updatedUser = table.CreateInstance<UpdateUserDataDTO>();
            updatedUser.id = _userId;
            var user = JsonConvert.SerializeObject(updatedUser);
            var uri = "http://localhost:8080/users";
            var result = _client.PutAsync(uri, new StringContent(user, Encoding.UTF8, "application/json")).Result;
            var userObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = result.StatusCode;
            ScenarioContext.Current["userObject"] = userObject;
        }
        
        [When(@"The client puts a request for user deletion")]
        public void WhenTheClientPutsARequestForUserDeletion()
        {
            var uri = "http://localhost:8080/users/" + _userId;
            var result = _client.DeleteAsync(uri).Result;
            ScenarioContext.Current["statusCode"] = result.StatusCode;
            _users = null;
        }
        
        [Then(@"the created user should be returned")]
        public void ThenTheCreatedUserShouldBeReturned()
        {
            var userJson = ScenarioContext.Current["userObject"];
            User user = JsonConvert.DeserializeObject<User>((string)userJson);
            Assert.IsNotNull(user);
            Assert.AreEqual(_user.username, user.username);
            Assert.AreEqual(_user.firstname, user.firstname);
            Assert.AreEqual(_user.lastname, user.lastname);
            Assert.AreEqual(_user.password, user.password);
            Assert.AreEqual(_user.phone, user.phone);
            Assert.AreEqual(_user.email, user.email);
            Assert.AreEqual(_user.userStatus, user.userStatus);
        }
        
        [Then(@"a bearer token should be returned")]
        public void ThenABearerTokenShouldBeReturned()
        {
            var token = ScenarioContext.Current["token"];
            Assert.IsTrue(!string.IsNullOrEmpty(token.ToString()));
        }
        
        [Then(@"the updated user id and username should match existing user")]
        public void ThenTheUpdatedUserIdAndUsernameShouldMatchExistingUser()
        {
            var userJson = ScenarioContext.Current["userObject"];
            User user = JsonConvert.DeserializeObject<User>((string)userJson);
            Assert.IsNotNull(user);
            Assert.AreEqual(_users.First().id, user.id);
            Assert.AreEqual(_users.First().username, user.username);
        }

        [When(@"The client puts a request for a single user")]
        public void WhenTheClientPutsARequestForASingleUser()
        {
            var uri = "http://localhost:8080/users/" + _userId;
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            var userObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["userObject"] = result.Content.ReadAsStringAsync().Result;
        }

        [Then(@"the existing user is returned")]
        public void ThenTheExistingUserIsReturned()
        {
            var userJson = ScenarioContext.Current["userObject"];
            User user = JsonConvert.DeserializeObject<User>((string)userJson);
            Assert.IsNotNull(user);
            Assert.AreEqual(_userId, user.id);
            Assert.AreEqual(_users.First().username, user.username);
            Assert.AreEqual(_users.First().firstname, user.firstname);
            Assert.AreEqual(_users.First().lastname, user.lastname);
            Assert.AreEqual(_users.First().password, user.password);
            Assert.AreEqual(_users.First().phone, user.phone);
            Assert.AreEqual(_users.First().email, user.email);
            Assert.AreEqual(_users.First().userStatus, user.userStatus);
        }

        [When(@"The client puts a request for a student list")]
        public void WhenTheClientPutsARequestForAStudentList()
        {
            var uri = "http://localhost:8080/users";
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            var userObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["userObject"] = result.Content.ReadAsStringAsync().Result;
        }

        [Then(@"a list of existing users is returned")]
        public void ThenAListOfExistingUsersIsReturned()
        {
            var userJson = ScenarioContext.Current["userObject"];
            var users = JsonConvert.DeserializeObject<List<User>>((string)userJson);
            Assert.IsNotNull(users);

            for (int i = 0; i < users.Count - 1; i++)
            {
                Assert.AreEqual(_users[i].id, users[i + 1].id);
                Assert.AreEqual(_users[i].username, users[i + 1].username);
                Assert.AreEqual(_users[i].firstname, users[i + 1].firstname);
                Assert.AreEqual(_users[i].lastname, users[i + 1].lastname);
                Assert.AreEqual(_users[i].password, users[i + 1].password);
                Assert.AreEqual(_users[i].phone, users[i + 1].phone);
                Assert.AreEqual(_users[i].email, users[i + 1].email);
                Assert.AreEqual(_users[i].userStatus, users[i + 1].userStatus);
            }
        }


        [After]
        public void Cleanup()
        {
            GivenIAmAnAuthorizedUser();
            if (_users != null)
            {
                foreach (User u in _users)
                {
                    var uri = "http://localhost:8080/users/" + u.id;
                    var result = _client.DeleteAsync(uri).Result;
                }
            }

            _user = null;
            _userId = 0;
            _users = null;
        }
    }
}
