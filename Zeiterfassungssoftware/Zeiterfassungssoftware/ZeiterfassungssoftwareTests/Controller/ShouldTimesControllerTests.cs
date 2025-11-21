using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zeiterfassungssoftware.Controller;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.Controller.Tests
{
    [TestClass]
    public class ShouldTimesControllerTests
    {
        private ApplicationDbContext _context;
        private ShouldTimesController _controller;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new ShouldTimesController(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetShouldTimes_ReturnsEmptyList_WhenNoValidShouldTimesExist()
        {
            var result = await _controller.GetShouldTimes();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var shouldTimes = okResult.Value as List<ShouldTimeDto>;
            Assert.IsNotNull(shouldTimes);
            Assert.AreEqual(0, shouldTimes.Count);
        }

        [TestMethod]
        public async Task GetShouldTimes_ReturnsOnlyValidShouldTimes()
        {
            var validShouldTime = new ShouldTime
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Monday,
                ValidUntil = DateTime.Now.AddDays(30)
            };
            var expiredShouldTime = new ShouldTime
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Tuesday,
                ValidUntil = DateTime.Now.AddDays(-30)
            };
            _context.ShouldTimes.AddRange(validShouldTime, expiredShouldTime);
            await _context.SaveChangesAsync();

            var result = await _controller.GetShouldTimes();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var shouldTimes = okResult.Value as List<ShouldTimeDto>;
            Assert.IsNotNull(shouldTimes);
            Assert.AreEqual(1, shouldTimes.Count);
        }

        [TestMethod]
        public async Task GetShouldTimes_ReturnsAllValidShouldTimes()
        {
            var shouldTime1 = new ShouldTime
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Monday,
                ValidUntil = DateTime.Now.AddDays(30)
            };
            var shouldTime2 = new ShouldTime
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Tuesday,
                ValidUntil = DateTime.Now.AddDays(30)
            };
            _context.ShouldTimes.AddRange(shouldTime1, shouldTime2);
            await _context.SaveChangesAsync();

            var result = await _controller.GetShouldTimes();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var shouldTimes = okResult.Value as List<ShouldTimeDto>;
            Assert.IsNotNull(shouldTimes);
            Assert.AreEqual(2, shouldTimes.Count);
        }

        [TestMethod]
        public async Task GetShouldTimeById_ReturnsShouldTime_WhenValidShouldTimeExists()
        {
            var id = Guid.NewGuid();
            var shouldTime = new ShouldTime
            {
                Id = id,
                DayOfWeek = DayOfWeek.Monday,
                ValidUntil = DateTime.Now.AddDays(30)
            };
            _context.ShouldTimes.Add(shouldTime);
            await _context.SaveChangesAsync();

            var result = await _controller.GetShouldTimeById(id);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var shouldTimeDto = okResult.Value as ShouldTimeDto;
            Assert.IsNotNull(shouldTimeDto);
        }

        [TestMethod]
        public async Task GetShouldTimeById_ReturnsNotFound_WhenShouldTimeDoesNotExist()
        {
            var result = await _controller.GetShouldTimeById(Guid.NewGuid());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetShouldTimeById_ReturnsNotFound_WhenShouldTimeIsExpired()
        {
            var id = Guid.NewGuid();
            var shouldTime = new ShouldTime
            {
                Id = id,
                DayOfWeek = DayOfWeek.Monday,
                ValidUntil = DateTime.Now.AddDays(-30)
            };
            _context.ShouldTimes.Add(shouldTime);
            await _context.SaveChangesAsync();

            var result = await _controller.GetShouldTimeById(id);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        


        [TestMethod]
        public async Task DeleteShouldTime_ReturnsNotFound_WhenShouldTimeDoesNotExist()
        {
            var result = await _controller.DeleteShouldTime(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteShouldTime_InvalidatesShouldTime_WhenShouldTimeExists()
        {
            var id = Guid.NewGuid();
            var shouldTime = new ShouldTime
            {
                Id = id,
                DayOfWeek = DayOfWeek.Monday,
                ValidUntil = DateTime.Now.AddDays(30)
            };
            _context.ShouldTimes.Add(shouldTime);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteShouldTime(id);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));

            var deletedShouldTime = await _context.ShouldTimes.FindAsync(id);
            Assert.IsNotNull(deletedShouldTime);
            Assert.IsTrue(deletedShouldTime.ValidUntil <= DateTime.Now);
        }

        [TestMethod]
        public async Task DeleteShouldTime_DoesNotPhysicallyDeleteRecord()
        {
            var id = Guid.NewGuid();
            var shouldTime = new ShouldTime
            {
                Id = id,
                DayOfWeek = DayOfWeek.Monday,
                ValidUntil = DateTime.Now.AddDays(30)
            };
            _context.ShouldTimes.Add(shouldTime);
            await _context.SaveChangesAsync();

            await _controller.DeleteShouldTime(id);

            var shouldTimeStillExists = await _context.ShouldTimes.FindAsync(id);
            Assert.IsNotNull(shouldTimeStillExists);
        }
    }
}