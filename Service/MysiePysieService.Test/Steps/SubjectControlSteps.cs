using Microsoft.EntityFrameworkCore.Internal;
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
    [Scope(Feature = "SubjectControl")]
    public class SubjectControlSteps : BaseSteps
    {
        TeacherDTO _teacher;
        int _teacherId;
        List<Teacher> _teachers = new List<Teacher>();
        SubjectDTO _subject;
        int _subjectId;
        List<Subject> _subjects = new List<Subject>();

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
        public void GivenIHaveTheTeachersId()
        {
            if (_teachers.Any())
            {
                _teacherId = _teachers.First().id;
            }
            else
                _teacherId = 0;
        }
        
        [Given(@"i create a new subject")]
        public void GivenICreateANewSubject(Table table)
        {
            _subject = table.CreateInstance<SubjectDTO>();
            _subject.teacher = _teachers.First();
        }

        [When(@"The client puts a request for subject creation")]
        public void WhenTheClientPutsARequestForSubjectCreation()
        {
            var uri = "http://localhost:8080/subjects";
            var subject = JsonConvert.SerializeObject(_subject);
            var result = _client.PostAsync(uri, new StringContent(subject, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["subjectObject"] = result.Content.ReadAsStringAsync().Result;
        }


        [Then(@"the created subject is returned")]
        public void ThenTheCreatedSubjectIsReturned()
        {
            var subjectJson = ScenarioContext.Current["subjectObject"];
            Subject subject = JsonConvert.DeserializeObject<Subject>((string)subjectJson);
            Assert.IsNotNull(subject);
            Assert.AreEqual(_subject.name, subject.name);
            Assert.AreEqual(_subject.ECTSpoints, subject.ECTSpoints);
            Assert.AreEqual(_subject.teacher.id, subject.teacher.id);
            Assert.AreEqual(_subject.teacher.forename, subject.teacher.forename);
            Assert.AreEqual(_subject.teacher.surname, subject.teacher.surname);
            Assert.AreEqual(_subject.teacher.age, subject.teacher.age);


            _subjects.Add(subject);
        }

        [Given(@"there's an existing subject")]
        public void GivenThereSAnExistingSubject(Table table)
        {
            var subject = table.CreateInstance<SubjectDTO>();
            subject.teacher = _teachers.First();
            var uri = "http://localhost:8080/subjects";
            var subjectJson = JsonConvert.SerializeObject(subject);
            var result = _client.PostAsync(uri, new StringContent(subjectJson, Encoding.UTF8, "application/json")).Result;

            Subject subjectToList = JsonConvert.DeserializeObject<Subject>(result.Content.ReadAsStringAsync().Result);
            _subjects.Add(subjectToList);
        }

        [Given(@"i have the subject id")]
        public void GivenIHaveTheSubjectId()
        {
            if (_subjects.Any())
            {
                _subjectId = _subjects.First().id;
            }else 
                _subjectId = 0;
        }

        [When(@"The client puts a request for a subject with givenId")]
        public void WhenTheClientPutsARequestForASubjectWithGivenId()
        {
            var uri = "http://localhost:8080/subjects/" + _subjectId;
            var result = _client.GetAsync(uri).Result;
            var statusCode = result.StatusCode;
            var subjectObject = result.Content.ReadAsStringAsync().Result;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["subjectObject"] = subjectObject;
        }

        [Then(@"the returned subject matches the existing one")]
        public void ThenTheReturnedSubjectMatchesTheExistingOne()
        {
            var subjectJson = ScenarioContext.Current["subjectObject"];
            Subject subject = JsonConvert.DeserializeObject<Subject>((string)subjectJson);
            Assert.IsNotNull(subject);
            Assert.AreEqual(_subjects.First().id, subject.id);
            Assert.AreEqual(_subjects.First().name, subject.name);
            Assert.AreEqual(_subjects.First().ECTSpoints, subject.ECTSpoints);
            Assert.AreEqual(_subjects.First().teacher.id, subject.teacher.id);
            Assert.AreEqual(_subjects.First().teacher.forename, subject.teacher.forename);
            Assert.AreEqual(_subjects.First().teacher.surname, subject.teacher.surname);
            Assert.AreEqual(_subjects.First().teacher.age, subject.teacher.age);
        }


        [When(@"i update the subject teacher to the second one")]
        public void WhenIUpdateTheSubjectTeacherToTheSecondOne()
        {
            var teacher = _teachers.Find(t => t.surname.Equals("LepszyMuzyk"));
            var subject = _subjects.First();
            subject.id = _subjectId;
            subject.teacher = teacher;

            var uri = "http://localhost:8080/subjects";
            var subjectJson = JsonConvert.SerializeObject(subject);
            var result = _client.PutAsync(uri, new StringContent(subjectJson, Encoding.UTF8, "application/json")).Result;
            var statusCode = result.StatusCode;

            ScenarioContext.Current["statusCode"] = statusCode;
            ScenarioContext.Current["subjectObject"] = result.Content.ReadAsStringAsync().Result;
        }

        [Then(@"the subject teacher is changed")]
        public void ThenTheSubjectTeacherIsChanged()
        {
            var teacher = _teachers.Find(t => t.surname.Equals("LepszyMuzyk"));
            var subjectJson = ScenarioContext.Current["subjectObject"];
            Subject subject = JsonConvert.DeserializeObject<Subject>((string)subjectJson);

            Assert.IsNotNull(subject);
            Assert.AreEqual(_subjects.First().id, subject.id);
            Assert.AreEqual(_subjects.First().name, subject.name);
            Assert.AreEqual(_subjects.First().ECTSpoints, subject.ECTSpoints);
            Assert.AreEqual(teacher.id, subject.teacher.id);
            Assert.AreEqual(teacher.forename, subject.teacher.forename);
            Assert.AreEqual(teacher.surname, subject.teacher.surname);
            Assert.AreEqual(teacher.age, subject.teacher.age);
        }



        [After]
        public void Cleanup()
        {
            GivenIAmAnAuthorizedUser();
            if(_subjects != null)
            {
                foreach (Subject s in _subjects)
                {
                    var deleteSubject = "http://localhost:8080/subjects/" + s.id;
                    _client.DeleteAsync(deleteSubject);
                }
            }

            if (_teachers != null)
            {
                foreach (Teacher t in _teachers)
                {
                    var deleteTeacher = "http://localhost:8080/teachers/" + t.id;
                    _client.DeleteAsync(deleteTeacher);
                }
            }


            _subject = null;
            _subjectId = 0;
            _subjects = null;
            _teacher = null;
            _teacherId = 0;
            _teachers = null;
        }
    }
}
