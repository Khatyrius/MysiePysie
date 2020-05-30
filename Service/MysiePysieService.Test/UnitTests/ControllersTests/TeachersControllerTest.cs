using Microsoft.AspNetCore.Mvc;
using Moq;
using MysiePysieService.Controllers;
using MysiePysieService.Data;
using MysiePysieService.DTO;
using MysiePysieService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MysiePysieService.Test.UnitTests.ControllersTests
{
    class TeachersControllerTest
    {
        [TestCase("TeacherTest","TeacherTest",1)]
        [TestCase("Pan", "Bohater", 2)]
        [Test]
        public async Task Add_Teacher_Test(string forename, string surname, int age)
        {
            //Given
            TeacherDTO teacher = new TeacherDTO()
            {
                forename = forename,
                surname = surname,
                age = age
            };

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(x => x.Add(It.IsAny<Teacher>())).ReturnsAsync(true);

            //When
            TeacherController teacherController = new TeacherController(teacherRepositoryMock.Object);
            var result = await teacherController.AddTeacher(teacher) as ObjectResult;
            var teacherResult = (Teacher)result.Value;


            //Then
            Assert.AreEqual((int)HttpStatusCode.Created, result.StatusCode);
            Assert.AreEqual(teacher.forename, teacherResult.forename);
            Assert.AreEqual(teacher.surname, teacherResult.surname);
            Assert.AreEqual(teacher.age, teacherResult.age);
        }

        [Test]
        public async Task Get_Teacher_By_Id()
        {
            Teacher teacher = new Teacher()
            {
                id = 1,
                forename = "test",
                surname = "test",
                age = 1
            };

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(x => x.GetById(teacher.id)).ReturnsAsync(teacher);

            //When
            TeacherController teacherController = new TeacherController(teacherRepositoryMock.Object);
            var result = await teacherController.GetTeacher(teacher.id) as ObjectResult;
            var teacherResult = (Teacher)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(teacher.id, teacherResult.id);
            Assert.AreEqual(teacher.forename, teacherResult.forename);
            Assert.AreEqual(teacher.surname, teacherResult.surname);
            Assert.AreEqual(teacher.age, teacherResult.age);
        }

        [Test]
        public async Task Get_Teachers()
        {
            //Given
            var teachers = new List<Teacher>() {
                new Teacher()
                {
                    forename = "test",
                    surname = "test",
                    age = 1
                },
                new Teacher()
                {
                    forename = "test2",
                    surname = "test2",
                    age = 2
                }
            };
            
            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(teachers);

            //When
            TeacherController controller = new TeacherController(teacherRepositoryMock.Object);
            var result = await controller.GetTeachers() as ObjectResult;
            var resultList = (List<Teacher>) result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);

            Assert.AreEqual("test", resultList[0].forename);
            Assert.AreEqual("test", resultList[0].surname);
            Assert.AreEqual(1, resultList[0].age);

            Assert.AreEqual("test2", resultList[1].forename);
            Assert.AreEqual("test2", resultList[1].surname);
            Assert.AreEqual(2, resultList[1].age);
        }

        [Test]
        public async Task Update_Teacher()
        {
            //Given
            TeacherDTO updatedTeacher = new TeacherDTO()
            {
                id = 1,
                forename = "updatedTest",
                surname = "updatedTest",
                age = 1
            };

            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(x => x.Update(It.IsAny<Teacher>())).ReturnsAsync(true);

            //When
            TeacherController controller = new TeacherController(teacherRepositoryMock.Object);
            var result = await controller.UpdateTeacher(updatedTeacher) as ObjectResult;
            var actualResult = (Teacher)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);

            Assert.AreEqual(updatedTeacher.id, actualResult.id);
            Assert.AreEqual(updatedTeacher.forename, actualResult.forename);
            Assert.AreEqual(updatedTeacher.surname, actualResult.surname);
            Assert.AreEqual(updatedTeacher.age, actualResult.age);
        }

        public async Task Delete_Teacher()
        {
            //Given
            int teacherId = 1;
            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(x => x.Delete(teacherId)).ReturnsAsync(true);

            //When
            TeacherController controller = new TeacherController(teacherRepositoryMock.Object);
            var result = await controller.DeleteTeacher(teacherId) as ObjectResult;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        public async Task Get_Last_Teacher_Id()
        {
            //Given
            int teacherId = 5;
            var teacherRepositoryMock = new Mock<ITeacherRepository>();
            teacherRepositoryMock.Setup(x => x.GetLast()).Returns(teacherId);

            //When
            TeacherController controller = new TeacherController(teacherRepositoryMock.Object);
            var result = await controller.GetLastId() as ObjectResult;
            var actualResult = result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(teacherId, actualResult);
        }
    }
}
