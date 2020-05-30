using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using MysiePysieService.DTO;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using TechTalk.SpecFlow;

namespace MysiePysieService.Test.Steps
{
    [Binding]
    public class BaseSteps
    {
        public HttpClient _client;
        public TestServer _server;

        [Before]
        public void SetUp()
        {
            _server = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            _client = _server.CreateClient();
        }

        [Given(@"I am an authorized user")]
        public void GivenIAmAnAuthorizedUser()
        {
            UserAuthenticationDTO userAuth = new UserAuthenticationDTO
            {
                username = "TestUser",
                password = "Test"
            };

            var user = JsonConvert.SerializeObject(userAuth);

            var uri = "http://localhost:8080/users/login";

            var token = _client.PostAsync(uri, new StringContent(user, Encoding.UTF8, "application/json")).Result.Content.ReadAsStringAsync().Result;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.ToString());
        }

        [Then(@"a Unauthorized status code should be returned")]
        public void ThenAUnauthorizedStatusCodeShouldBeReturned()
        {
            var statusCode = ScenarioContext.Current["statusCode"];
            Assert.AreEqual(HttpStatusCode.Unauthorized, statusCode);
        }

        [Then(@"a Created status code should be returned")]
        public void ThenACreatedStatusCodeShouldBeReturned()
        {
            var statusCode = ScenarioContext.Current["statusCode"];
            Assert.AreEqual(HttpStatusCode.Created, statusCode);
        }

        [Then(@"a Ok status code should be returned")]
        public void ThenAOkStatusCodeShouldBeReturned()
        {
            var statusCode = ScenarioContext.Current["statusCode"];
            Assert.AreEqual(HttpStatusCode.OK, statusCode);
        }

        [Then(@"a BadRequest status code should be returned")]
        public void ThenABadRequestStatusCodeShouldBeReturned()
        {
            var statusCode = ScenarioContext.Current["statusCode"];
            Assert.AreEqual(HttpStatusCode.BadRequest, statusCode);
        }

        [Then(@"a Conflict status code should be returned")]
        public void ThenAConflistStatusCodeShouldBeReturned()
        {
            var statusCode = ScenarioContext.Current["statusCode"];
            Assert.AreEqual(HttpStatusCode.Conflict, statusCode);
        }

        [Then(@"a Not Found status code should be returned")]
        public void ThenANotFoundStatusCodeShouldBeReturned()
        {
            var statusCode = ScenarioContext.Current["statusCode"];
            Assert.AreEqual(HttpStatusCode.NotFound, statusCode);
        }



    }
}
