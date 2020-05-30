using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Moq;
using MysiePysieService.Controllers;
using MysiePysieService.Data;
using MysiePysieService.DTO;
using MysiePysieService.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MysiePysieService.Test.UnitTests.ControllersTests
{
    class ClassesControllerTest
    {
        [Test]
        public async Task Get_All_Classes()
        {
            var studentsOfClass1 = new List<Student>(){
                new Student()
                {
                    id = 1,
                    forename = "test",
                    surname = "test",
                    age = 1,
                    status = "test",
                    @class = new Class()
                    {
                        id = 1,
                        name = "Klasa1"
                    }
                }
            };


            //Given
            var classes = new List<Class>()
            {
                new Class()
                {
                    id = 1,
                    name = "Klasa1",
                    students = studentsOfClass1
                },
                new Class()
                {
                    id = 2,
                    name = "Klasa2"
                }
            };

            var classesRepositoryMock = new Mock<IClassRepository>();
            classesRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(classes);

            //When
            ClassesController controller = new ClassesController(classesRepositoryMock.Object);
            var result = await controller.GetClasses() as ObjectResult;
            var actualResult = (List<Class>)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(classes[0].id, actualResult[0].id);
            Assert.AreEqual(classes[0].name, actualResult[0].name);
            Assert.AreEqual(classes[0].students.First().id, actualResult[0].students.First().id);
            Assert.AreEqual(classes[0].students.First().forename, actualResult[0].students.First().forename);
            Assert.AreEqual(classes[0].students.First().surname, actualResult[0].students.First().surname);
            Assert.AreEqual(classes[0].students.First().@class.id, actualResult[0].id);
            Assert.AreEqual(classes[1].id, actualResult[1].id);
            Assert.AreEqual(classes[1].name, actualResult[1].name);
            Assert.IsTrue(classes[1].students == null);
        }

        [Test]
        public async Task Get_Class_By_Id()
        {
            //Given
            int classId = 1;

            var @class = new Class()
            {
                id = classId,
                name = "Klasa Testowa"
            };

            var classesRepositoryMock = new Mock<IClassRepository>();
            classesRepositoryMock.Setup(x => x.GetById(classId)).ReturnsAsync(@class);

            //When
            var controller = new ClassesController(classesRepositoryMock.Object);
            var result = await controller.GetClass(classId) as ObjectResult;
            var actualResult = (Class)result.Value;

            //Then
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(@class.id, actualResult.id);
            Assert.AreEqual(@class.name, actualResult.name);
        }
    }
}
