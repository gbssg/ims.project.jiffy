using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeiterfassungssoftware.Controller;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Classes;

namespace Zeiterfassungssoftware.Controller.Tests
{
    [TestClass]
    public class ClassesControllerTests
    {
        private ApplicationDbContext _context;
        private ClassesController _controller;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new ClassesController(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetAllClasses_ReturnsEmptyList_WhenNoClassesExist()
        {
            var result = await _controller.GetAllClasses();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var classes = okResult.Value as List<ClassDto>;
            Assert.IsNotNull(classes);
            Assert.AreEqual(0, classes.Count);
        }

        [TestMethod]
        public async Task GetAllClasses_ReturnsAllClasses()
        {
            var class1 = new Class { Id = Guid.NewGuid(), Name = "Class 1" };
            var class2 = new Class { Id = Guid.NewGuid(), Name = "Class 2" };
            _context.Classes.AddRange(class1, class2);
            await _context.SaveChangesAsync();

            var result = await _controller.GetAllClasses();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var classes = okResult.Value as List<ClassDto>;
            Assert.IsNotNull(classes);
            Assert.AreEqual(2, classes.Count);
        }

        [TestMethod]
        public async Task GetClassById_ReturnsClass_WhenClassExists()
        {
            var classId = Guid.NewGuid();
            var classEntity = new Class { Id = classId, Name = "Test Class" };
            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();

            var result = await _controller.GetClassById(classId);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var classDto = okResult.Value as ClassDto;
            Assert.IsNotNull(classDto);
            Assert.AreEqual("Test Class", classDto.Name);
        }

        [TestMethod]
        public async Task GetClassById_ReturnsNotFound_WhenClassDoesNotExist()
        {
            var result = await _controller.GetClassById(Guid.NewGuid());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteClassById_ReturnsBadRequest_WhenIdIsEmpty()
        {
            var result = await _controller.DeleteClassById(Guid.Empty);

            Assert.IsInstanceOfType(result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task DeleteClassById_ReturnsNotFound_WhenClassDoesNotExist()
        {
            var result = await _controller.DeleteClassById(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteClassById_DeletesClass_WhenClassExists()
        {
            var classId = Guid.NewGuid();
            var classEntity = new Class { Id = classId, Name = "Test Class" };
            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteClassById(classId);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            var deletedClass = await _context.Classes.FindAsync(classId);
            Assert.IsNull(deletedClass);
        }

        [TestMethod]
        public async Task DeleteClassById_RemovesClassIdFromUsers()
        {
            var classId = Guid.NewGuid();
            var classEntity = new Class { Id = classId, Name = "Test Class" };
            var user = new ApplicationUser { Id = "user1", ClassId = classId };
            _context.Classes.Add(classEntity);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            await _controller.DeleteClassById(classId);

            var updatedUser = await _context.Users.FindAsync("user1");
            Assert.AreEqual(Guid.Empty, updatedUser.ClassId);
        }

        [TestMethod]
        public async Task AddClass_ReturnsCreatedClass()
        {
            var classDto = new ClassDto { Name = "New Class" };

            var result = await _controller.AddClass(classDto);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var returnedClass = okResult.Value as ClassDto;
            Assert.IsNotNull(returnedClass);
            Assert.AreEqual("New Class", returnedClass.Name);
            Assert.AreEqual(1, await _context.Classes.CountAsync());
        }

        [TestMethod]
        public async Task UpdateClass_ReturnsBadRequest_WhenIdIsEmpty()
        {
            var classDto = new ClassDto { Name = "Updated Class" };

            var result = await _controller.UpdateClass(Guid.Empty, classDto);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UpdateClass_ReturnsNotFound_WhenClassDoesNotExist()
        {
            var classDto = new ClassDto { Name = "Updated Class" };

            var result = await _controller.UpdateClass(Guid.NewGuid(), classDto);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateClass_UpdatesClass_WhenClassExists()
        {
            var classId = Guid.NewGuid();
            var classEntity = new Class { Id = classId, Name = "Old Name" };
            _context.Classes.Add(classEntity);
            await _context.SaveChangesAsync();

            var classDto = new ClassDto { Name = "New Name" };
            var result = await _controller.UpdateClass(classId, classDto);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var updatedClass = okResult.Value as ClassDto;
            Assert.IsNotNull(updatedClass);
            Assert.AreEqual("New Name", updatedClass.Name);

            var dbClass = await _context.Classes.FindAsync(classId);
            Assert.AreEqual("New Name", dbClass.Name);
        }
    }
}
