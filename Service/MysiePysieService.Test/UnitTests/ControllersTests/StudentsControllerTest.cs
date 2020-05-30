using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using MysiePysieService.Controllers;
using MysiePysieService.Data;
using MysiePysieService.Database;
using MysiePysieService.DTO;
using MysiePysieService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TechTalk.SpecFlow.Assist;

namespace MysiePysieService.Test.UnitTests.ControllersTests
{
    class StudentsControllerTest
    {
        [TestCase(1, "Test", "Test", 1,"Test")]
        [TestCase(2, "Test", "Test", 2, "Test")]
        [TestCase(3, "", "Test", 1, "")]
        [Test]
        public async Task Get_Student_By_Id(int id, string forename, string surname, int age, string status)
        {
            //Given
            Student student = new Student()
            {
                id = id,
                forename = forename,
                surname = surname,
                age = age,
                status = status
            };

            var mockStudentReposiotory = new Mock<IStudentRepository>();
            mockStudentReposiotory.Setup(c => c.GetById(id)).ReturnsAsync(student);

            //When
            StudentsController studnetController = new StudentsController(mockStudentReposiotory.Object);
            var result = await studnetController.GetStudent(id) as OkObjectResult;
            var objectResult = (Student)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(id, objectResult.id);
            Assert.AreEqual(forename, objectResult.forename);
            Assert.AreEqual(surname, objectResult.surname);
            Assert.AreEqual(age, objectResult.age);
            Assert.AreEqual(status, objectResult.status);
        }

        [Test]
        public async Task Get_Student_List()
        {
            //Given
            Student student = new Student()
            {
                id = 1,
                forename = "test",
                surname = "test",
                age = 1,
                status = "test"
            };

            Student student2 = new Student()
            {
                id = 2,
                forename = "test2",
                surname = "test2",
                age = 2,
                status = "test2"
            };

            List<Student> students = new List<Student>();
            students.Add(student);
            students.Add(student2);
           
            var mockStudentReposiotory = new Mock<IStudentRepository>();
            mockStudentReposiotory.Setup(c => c.GetAll()).ReturnsAsync(students);

            //When
            StudentsController studnetController = new StudentsController(mockStudentReposiotory.Object);
            var result = await studnetController.GetStudents() as OkObjectResult;
            var objectResult = (List<Student>)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(student.id, objectResult[0].id);
            Assert.AreEqual(student.forename, objectResult[0].forename);
            Assert.AreEqual(student.surname, objectResult[0].surname);
            Assert.AreEqual(student.age, objectResult[0].age);
            Assert.AreEqual(student.status, objectResult[0].status);

            Assert.AreEqual(student2.id, objectResult[1].id);
            Assert.AreEqual(student2.forename, objectResult[1].forename);
            Assert.AreEqual(student2.surname, objectResult[1].surname);
            Assert.AreEqual(student2.age, objectResult[1].age);
            Assert.AreEqual(student2.status, objectResult[1].status);
        }

        [Test]
        public async Task Add_New_Student()
        {
            //Given
            StudentDTO student = new StudentDTO()
            {
                forename = "test",
                surname = "test",
                status = "test",
                age = 1
            };

            var mockStudentReposiotory = new Mock<IStudentRepository>();
            mockStudentReposiotory.Setup(x => x.Add(It.IsAny<Student>())).ReturnsAsync(true);

            //When
            StudentsController studentsController = new StudentsController(mockStudentReposiotory.Object);
            var result = await studentsController.AddStudent(student) as ObjectResult;
            var objectResult = (Student)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual(student.forename, objectResult.forename);
            Assert.AreEqual(student.surname, objectResult.surname);
            Assert.AreEqual(student.age, objectResult.age);
            Assert.AreEqual(student.status, objectResult.status);
        }

        [Test]
        public async Task Update_Student()
        {
            //Given
            var mockStudentReposiotory = new Mock<IStudentRepository>();
            mockStudentReposiotory.Setup(x => x.Update(It.IsAny<Student>())).ReturnsAsync(true);

            //When
            StudentDTO updatedStudent = new StudentDTO()
            {
                id = 1,
                forename = "updatedTest",
                surname = "updatedTest",
                status = "updatedTest",
                age = 1
            };

            StudentsController studentsController = new StudentsController(mockStudentReposiotory.Object);
            var result = await studentsController.UpdateStudent(updatedStudent) as ObjectResult;
            var objectResult = (Student)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(updatedStudent.id, objectResult.id);
            Assert.AreEqual(updatedStudent.forename, objectResult.forename);
            Assert.AreEqual(updatedStudent.surname, objectResult.surname);
            Assert.AreEqual(updatedStudent.age, objectResult.age);
            Assert.AreEqual(updatedStudent.status, objectResult.status);
        }

        [Test]
        public async Task Delete_Student()
        {
            //given 
            int studentId = 1;
            var mockStudentReposiotory = new Mock<IStudentRepository>();
            mockStudentReposiotory.Setup(x => x.Delete(studentId)).ReturnsAsync(true);

            //When
            StudentsController studentsController = new StudentsController(mockStudentReposiotory.Object);
            var result = await studentsController.DeleteStudent(studentId) as ObjectResult;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [Test]
        public async Task Get_Last_Id()
        {
            //given 
            int studentId = 1;
            var mockStudentReposiotory = new Mock<IStudentRepository>();
            mockStudentReposiotory.Setup(x => x.GetLast()).Returns(studentId);

            //When
            StudentsController studentsController = new StudentsController(mockStudentReposiotory.Object);
            var result = await studentsController.GetLastId() as ObjectResult;
            var actualResult = result.Value;
            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(studentId, actualResult);
        }
    }
}
