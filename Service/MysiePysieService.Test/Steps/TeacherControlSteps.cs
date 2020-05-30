using MysiePysieService.DTO;
using MysiePysieService.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MysiePysieService.Test.Steps
{
    [Binding]
    [Scope(Feature = "TeacherControl")]
    public class TeacherControlSteps :BaseSteps
    {
        TeacherDTO _teacher;
        int _teacherId;
        List<Teacher> _teachers = new List<Teacher>();

        [Given(@"I create a new teacher with full info")]
        public void GivenICreateANewTeacherWithFullInfo(Table table)
        {
            _teacher = table.CreateInstance<TeacherDTO>();
        }
        
        [Given(@"There's an existing teacher in database")]
        public void GivenThereSAnExistingTeacherInDatabase(Table table)
        {
            var teacher = table.CreateInstance<TeacherDTO>();
            var uri = "http://localhost:8080/teachers";
            var teacherJson = JsonConvert.SerializeObject(teacher);
            var result = _client.PostAsync(uri, new StringContent(teacherJson, Encoding.UTF8, "application/json")).Result;
            Teacher teacherToList = JsonConvert.DeserializeObject<Teacher>(result.Content.ReadAsStringAsync().Result);
            _teachers.Add(teacherToList);
        }
        
        [Given(@"i have the teachers id")]
        public void GivenIHaveTheTaechersId()
        {
            if (_teachers.Any())
                _teacherId = _teachers.First().id;
            else
                _teacherId = 0;
        }
        
        [When(@"The client puts a request for teacher creation")]
        public void WhenTheClientPutsARequestForTeacherCreation()
        {
            var uri = "http://localhost:8080/teachers";
            var teacher = JsonConvert.SerializeObject(_teacher);
            var result = _client.PostAsync(uri, new StringContent(teacher, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;
            var teacherObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["teacherObject"] = teacherObject;

            Teacher teacherToList = JsonConvert.DeserializeObject<Teacher>((string)teacherObject);
            _teachers.Add(teacherToList);
        }
        
        [When(@"The client puts a request for a teacher with given id")]
        public void WhenTheClientPutsARequestForATeacherWithGivenId()
        {
            var uri = "http://localhost:8080/teachers/" + _teacherId;
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["teacherObject"] = result.Content.ReadAsStringAsync().Result;
        }
        
        [Then(@"the existing teacher info should match the given info")]
        public void ThenTheExistingStudentInfoShouldMatchTheGivenInfo()
        {
            var teacherJson = ScenarioContext.Current["teacherObject"];
            Teacher teacher = JsonConvert.DeserializeObject<Teacher>((string)teacherJson);
            Assert.IsNotNull(teacher);
            Assert.AreEqual(_teachers[0].forename, teacher.forename);
            Assert.AreEqual(_teachers[0].surname, teacher.surname);
            Assert.AreEqual(_teachers[0].age, teacher.age);
        }

        [When(@"The client puts a request for a teacher list")]
        public void WhenTheClientPutsARequestForATeacherList()
        {
            var uri = "http://localhost:8080/teachers";
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            var teacherObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["teacherObject"] = teacherObject;
        }

        [Then(@"the existing teachers list should match the exisitng teachers")]
        public void ThenTheExistingTeachersListShouldMatchTheExisitngTeachers()
        {
            var teacherJson = ScenarioContext.Current["teacherObject"];
            var teachers = JsonConvert.DeserializeObject<List<Teacher>>((string)teacherJson);
            Assert.IsNotNull(teachers);

            for (int i = 0; i < teachers.Count; i++)
            {
                Assert.AreEqual(_teachers[i].id, teachers[i].id);
                Assert.AreEqual(_teachers[i].forename, teachers[i].forename);
                Assert.AreEqual(_teachers[i].surname, teachers[i].surname);
                Assert.AreEqual(_teachers[i].age, teachers[i].age);
            }
        }

        [When(@"The client puts a request for teacher update")]
        public void WhenTheClientPutsARequestForTeacherUpdate(Table table)
        {
            var teacher = table.CreateInstance<TeacherDTO>();
            teacher.id = _teacherId;

            var uri = "http://localhost:8080/teachers";
            var teacherJson = JsonConvert.SerializeObject(teacher);
            var result = _client.PutAsync(uri, new StringContent(teacherJson, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["teacherObject"] = result.Content.ReadAsStringAsync().Result;
        }

        [Then(@"the returned teacher should match updated teacher info")]
        public void ThenTheReturnedTeacherShouldMatchUpdatedTeacherInfo()
        {
            var teacherJson = ScenarioContext.Current["teacherObject"];
            Teacher teacher = JsonConvert.DeserializeObject<Teacher>((string)teacherJson);
            Assert.IsNotNull(teacher);
            Assert.AreEqual(_teachers.First().id, teacher.id);
        }

        [When(@"The client puts a request for teacher deletion with given id")]
        public void WhenTheClientPutsARequestForTeacherDeletionWithGivenId()
        {
            var uri = "http://localhost:8080/teachers/" + _teacherId;
            var result = _client.DeleteAsync(uri).Result;
            var statusCode = result.StatusCode;
            ScenarioContext.Current["statusCode"] = statusCode;
        }

        [After]
        public void Cleanup()
        {
            GivenIAmAnAuthorizedUser();
            if (_teachers != null)
            {
                foreach (Teacher t in _teachers)
                {
                    var deleteTeacher = "http://localhost:8080/teachers/" + t.id;
                    _client.DeleteAsync(deleteTeacher);
                }
            }

            _teacher = null;
            _teacherId = 0;
            _teachers = null;
        }
    }
}
