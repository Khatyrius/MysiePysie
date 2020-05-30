using Castle.Core.Internal;
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
    [Scope(Feature = "ClassesControl")]
    public class ClassesControlSteps : BaseSteps
    {
        StudentDTO _student;
        int _studId;
        List<Student> _students = new List<Student>();
        ClassDTO _class;
        int _classId;
        List<Class> _classes = new List<Class>();


        [Given(@"I create a new class")]
        public void GivenICreateANewClass(Table table)
        {
            _class = table.CreateInstance<ClassDTO>();
        }
        
        [Given(@"There's an existing class")]
        public void GivenThereSAnExistingClass(Table table)
        {
            var @class = table.CreateInstance<ClassDTO>();
            var uri = "http://localhost:8080/classes";
            var classJson = JsonConvert.SerializeObject(@class);
            var result = _client.PostAsync(uri, new StringContent(classJson, Encoding.UTF8, "application/json")).Result;

            Class @classToList = JsonConvert.DeserializeObject<Class>(result.Content.ReadAsStringAsync().Result);
            _classes.Add(@classToList);
        }
        
        [Given(@"I have the class id")]
        public void GivenIHaveTheClassId()
        {
            if (_classes.Any())
                _classId = _classes.First().id;
            else
                _classId = 0;
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
        
        [Given(@"I have the students id")]
        public void GivenIHaveTheStudentsId()
        {
            if (_students.Any())
                _studId = _students.First().id;
            else
                _studId = 0;
        }

        [When(@"the student is in the class")]
        public void WhenTheStudentIsInTheClass()
        {
            var uri = "http://localhost:8080/classes/add/" + _classId + "/" + _studId;
            var result = _client.GetAsync(uri).Result;
        }


        [When(@"The client puts a request for a class creation")]
        public void WhenTheClientPutsARequestForAClassCreation()
        {
            var uri = "http://localhost:8080/classes";
            var @class = JsonConvert.SerializeObject(_class);
            var result = _client.PostAsync(uri, new StringContent(@class, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["classObject"] = result.Content.ReadAsStringAsync().Result;
        }
        
        [When(@"The client puts a request for class with given id")]
        public void WhenTheClientPutsARequestForClassWithGivenId()
        {
            var uri = "http://localhost:8080/classes/" + _classId;
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            var classObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["classObject"] = result.Content.ReadAsStringAsync().Result;
        }
        
        [When(@"The client puts a request for classes list")]
        public void WhenTheClientPutsARequestForClassesList()
        {
            var uri = "http://localhost:8080/classes";
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            var classObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["classObject"] = result.Content.ReadAsStringAsync().Result;
        }
        
        [When(@"The client puts a request for class update")]
        public void WhenTheClientPutsARequestForClassUpdate(Table table)
        {
            var @class = table.CreateInstance<ClassDTO>();
            @class.id = _classId;

            var uri = "http://localhost:8080/classes";
            var classJson = JsonConvert.SerializeObject(@class);
            var result = _client.PutAsync(uri, new StringContent(classJson, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["classObject"] = result.Content.ReadAsStringAsync().Result;
        }
        
        [When(@"The client puts a request for class deletion")]
        public void WhenTheClientPutsARequestForClassDeletion()
        {
            var uri = "http://localhost:8080/classes/" + _classId;
            var result = _client.DeleteAsync(uri).Result;
            var statusCode = result.StatusCode;
            ScenarioContext.Current["statusCode"] = statusCode;
        }
        
        [When(@"The client puts a request to add a student to class")]
        public void WhenTheClientPutsARequestToAddAStudentToClass()
        {
            var uri = "http://localhost:8080/classes/add/" + _classId +"/"+ _studId;
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            ScenarioContext.Current["statusCode"] = statusCode;
        }

        [Then(@"the created class is returned")]
        public void ThenTheCreatedClassIsReturned()
        {
            var classJson = ScenarioContext.Current["classObject"];
            Class @class = JsonConvert.DeserializeObject<Class>((string)classJson);
            Assert.IsNotNull(@class);
            Assert.AreEqual(_class.name, @class.name);
            Assert.AreEqual(_class.students, @class.students);

            _classes.Add(@class);
        }


        [When(@"The client puts a request to remove student from class")]
        public void WhenTheClientPutsARequestToRemoveStudentFromClass()
        {
            var uri = "http://localhost:8080/classes/remove/" + _classId + "/" + _studId;
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            ScenarioContext.Current["statusCode"] = statusCode;
        }
                
        [Then(@"The exisitng class is returned")]
        public void ThenTheExisitngClassIsReturned()
        {
            var classJson = ScenarioContext.Current["classObject"];
            Class @class = JsonConvert.DeserializeObject<Class>((string)classJson);
            Assert.IsNotNull(@class);
            Assert.AreEqual(_classes.First().name, @class.name);
            Assert.IsTrue(@class.students.IsNullOrEmpty());
        }
        
        [Then(@"a list of classes is returned")]
        public void ThenAListOfClassesIsReturned()
        {
            var classJson = ScenarioContext.Current["classObject"];
            var classes = JsonConvert.DeserializeObject<List<Class>>((string)classJson);
            Assert.IsNotNull(classes);

            for (int i = 0; i < classes.Count; i++)
            {
                Assert.AreEqual(classes[i].id, classes[i].id);
                Assert.AreEqual(classes[i].name, classes[i].name);
                Assert.AreEqual(_classes[i].students.IsNullOrEmpty(), classes[i].students.IsNullOrEmpty());
            }
        }
        
        [Then(@"the updated class id matches the existing class id")]
        public void ThenTheUpdatedClassIdMatchesTheExistingClassId()
        {
            var classJson = ScenarioContext.Current["classObject"];
            Class @class = JsonConvert.DeserializeObject<Class>((string)classJson);
            Assert.IsNotNull(@class);
            Assert.AreEqual(_classes.First().id, @class.id);
        }
        
        [Then(@"the student should be in the classes student list")]
        public void ThenTheStudentShouldBeInTheClassesStudentList()
        {
            var classJson = _client.GetAsync("http://localhost:8080/classes/" + _classId).Result.Content.ReadAsStringAsync().Result;
            Class @class = JsonConvert.DeserializeObject<Class>((string)classJson);
            var studentJson = _client.GetAsync("http://localhost:8080/students/" + _studId).Result.Content.ReadAsStringAsync().Result;
            Student student = JsonConvert.DeserializeObject<Student>((string)studentJson);

            Assert.IsTrue(!@class.students.IsNullOrEmpty());
            Assert.AreEqual(student.@class.id, @class.id);
            Assert.AreEqual(student.@class.name, @class.name);
            Assert.AreEqual(@class.students.First().id, student.id);
            Assert.AreEqual(@class.students.First().forename, student.forename);
            Assert.AreEqual(@class.students.First().surname, student.surname);
            Assert.AreEqual(@class.students.First().status, student.status);
        }
        
        [Then(@"student is no longer in class")]
        public void ThenStudentIsNoLongerInClass()
        {
            var classJson = _client.GetAsync("http://localhost:8080/classes/" + _classId).Result.Content.ReadAsStringAsync().Result;
            Class @class = JsonConvert.DeserializeObject<Class>((string)classJson);

            Assert.IsTrue(@class.students.IsNullOrEmpty());
        }

        [After]
        public void Cleanup()
        {
            GivenIAmAnAuthorizedUser();
            if (_students != null)
            {
                foreach (Student s in _students)
                {
                    var deleteStudent = "http://localhost:8080/students/" + s.id;
                    var response = _client.DeleteAsync(deleteStudent).Result;
                }
            }

            if (_classes != null)
            {
                foreach (Class c in _classes)
                {
                    var deleteClass = "http://localhost:8080/classes/" + c.id;
                    var response = _client.DeleteAsync(deleteClass).Result;
                }
            }

            _student = null;
            _studId = 0;
            _students = null;
            _class = null;
            _classId = 0;
            _classes = null;
        }
    }
}
