using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using MysiePysieService.DTO;
using MysiePysieService.Models;
using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Helpers;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace MysiePysieService.Test.Steps
{
    [Binding]
    [Scope(Feature = "StudentControl")]
    public class StudentControlSteps : BaseSteps
    {
        StudentDTO _student;
        int _studId;
        List<Student> _students = new List<Student>();
        
        [Given(@"I create a new student with full info")]
        public void GivenICreateANewStudentWithFullInfo(Table table)
        {
            _student = table.CreateInstance<StudentDTO>();
        }
        
        [When(@"The client puts a request for student creation")]
        public void WhenTheClientPutsARequestForStudentCreation()
        {
            var uri = "http://localhost:8080/students";
            var student = JsonConvert.SerializeObject(_student);
            var result = _client.PostAsync(uri, new StringContent(student, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;
            
            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["studentObject"] = result.Content.ReadAsStringAsync().Result;
        }
        
        [Then(@"the created student info should be returned")]
        public void ThenTheCreatedStudentInfoShouldBeReturned()
        {
            var studentJson = ScenarioContext.Current["studentObject"];
            Student student = JsonConvert.DeserializeObject<Student>((string)studentJson);
            Assert.IsNotNull(student);
            Assert.AreEqual(_student.forename, student.forename);
            Assert.AreEqual(_student.surname, student.surname);
            Assert.AreEqual(_student.age, student.age);
            Assert.AreEqual(_student.status, student.status);

            _students.Add(student);
        }

        [Given(@"I create a new student with missing info")]
        public void GivenICreateANewStudentWithMissingInfo(Table table)
        {
            _student = table.CreateInstance<StudentDTO>();
        }

        [Given(@"There's a an existing student in database")]
        public void GivenThereSAAnExistingStudentInDatabase(Table table)
        {
            var student = table.CreateInstance<StudentDTO>();
            var uri = "http://localhost:8080/students";
            var studentJson = JsonConvert.SerializeObject(student);
            var result = _client.PostAsync(uri, new StringContent(studentJson, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;

            Student studentToList = JsonConvert.DeserializeObject<Student>(result.Content.ReadAsStringAsync().Result);
            _students.Add(studentToList);
        }

        [When(@"I create a new student with same forename, surname and age")]
        public void WhenICreateANewStudentWithSameForenameSurnameAndAge(Table table)
        {
            _student = table.CreateInstance<StudentDTO>();
        }

        [Given(@"I have the students id")]
        public void GivenIHaveTheStudentId()
        {
            if (_students.Any())
                _studId = _students.First().id;
            else
                _studId = 0;
        }

        [When(@"The client puts a request for a student with given id")]
        public void WhenTheClientPutsARequestForAStudentWithGiven()
        {
            var uri = "http://localhost:8080/students/"+_studId;
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            var studentObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["studentObject"] = result.Content.ReadAsStringAsync().Result;
        }

        [Then(@"student with existing student data is returned")]
        public void ThenStudentWithExistingStudentDataIsReturned()
        {
            var studentJson = ScenarioContext.Current["studentObject"];
            Student student = JsonConvert.DeserializeObject<Student>((string)studentJson);
            Assert.IsNotNull(student);
            Assert.AreEqual(_students.First().forename, student.forename);
            Assert.AreEqual(_students.First().surname, student.surname);
            Assert.AreEqual(_students.First().age, student.age);
            Assert.AreEqual(_students.First().status, student.status);
        }

        [When(@"The client puts a request for a student list")]
        public void WhenTheClientPutsARequestForAStudentList()
        {
            var uri = "http://localhost:8080/students";
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            var studentObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["studentObject"] = result.Content.ReadAsStringAsync().Result;
        }

        [Then(@"a list of existing students is returned")]
        public void ThenAListOfExistingStudentsIsReturned()
        {
            var studentJson = ScenarioContext.Current["studentObject"];
            var students = JsonConvert.DeserializeObject<List<Student>>((string)studentJson);
            Assert.IsNotNull(students);

            for(int i = 0; i< students.Count; i++)
            {
                Assert.AreEqual(_students[i].id, students[i].id);
                Assert.AreEqual(_students[i].forename, students[i].forename);
                Assert.AreEqual(_students[i].surname, students[i].surname);
                Assert.AreEqual(_students[i].age, students[i].age);
                Assert.AreEqual(_students[i].status, students[i].status);
            }            
        }

        [When(@"The client puts a request for a student update")]
        public void WhenIUpdateTheExistingStudent(Table table)
        {
            var student = table.CreateInstance<StudentDTO>();
            student.id = _studId;

            var uri = "http://localhost:8080/students";
            var studentJson = JsonConvert.SerializeObject(student);
            var result = _client.PutAsync(uri, new StringContent(studentJson, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["studentObject"] = result.Content.ReadAsStringAsync().Result;
        }

        [Then(@"the updated student info should be returned")]
        public void ThenTheUpdatedStudentInfoShouldBeRetured()
        {
            var studentJson = ScenarioContext.Current["studentObject"];
            Student student = JsonConvert.DeserializeObject<Student>((string)studentJson);
            Assert.IsNotNull(student);
            Assert.AreEqual(_students.First().id, student.id);
        }

        [When(@"The client puts a request for a student deletion")]
        public void WhenTheClientPutsARequestForAStudentDeletion()
        {
            var uri = "http://localhost:8080/students/" + _studId;
            var result = _client.DeleteAsync(uri).Result;
            var statusCode = result.StatusCode;
            ScenarioContext.Current["statusCode"] = statusCode;
        }
 


        [After]
        public void Cleanup()
        {
            GivenIAmAnAuthorizedUser();
            if (_students != null)
            {
                foreach(Student s in _students)
                {
                    var deleteStudent = "http://localhost:8080/students/" + s.id;
                    _client.DeleteAsync(deleteStudent);
                }
            }

            _student = null;
            _studId = 0;
            _students = null;
        }
    }
}
