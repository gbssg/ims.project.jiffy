using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Services;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Controller.Tests
{
    [TestClass()]
    public class EntriesControllerTests
    {
        private EntriesController _entriesController;
        private ApplicationDbContext _context;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private List<Entry> _entries;
        private List<ApplicationUser> _users;
        private List<ShouldTime> _shouldTimes;

        [TestInitialize]
        public void Initialize()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: $"TestDB_{Guid.NewGuid()}")
                .Options;

            _context = new ApplicationDbContext(options);

            var store = new Mock<IUserStore<ApplicationUser>>();
            _userManagerMock = new Mock<UserManager<ApplicationUser>>(
                store.Object, null, null, null, null, null, null, null, null
            );

            var classId = Guid.NewGuid();

            _users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "user-1",
                    NormalizedUserName = "ADMIN@EMA.IL",
                    UserName = "admin@ema.il",
                    Email = "admin@ema.il",
                    NormalizedEmail = "ADMIN@EMA.IL",
                    ClassId = classId,
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "user-2",
                    NormalizedUserName = "USER@EMA.IL",
                    UserName = "user@ema.il",
                    Email = "user@ema.il",
                    NormalizedEmail = "USER@EMA.IL",
                    ClassId = classId,
                    EmailConfirmed = true
                }
            };

            _shouldTimes = new List<ShouldTime>
            {
                new ShouldTime
                {
                    Id = Guid.Parse("4107b8b9-36d2-4c68-9171-bc6f7cba478d"),
                    DayOfWeek = DayOfWeek.Monday,
                    ClassId = classId,
                    ValidUntil = DateTime.Now.AddYears(1)
                }
            };

            _entries = new List<Entry>
            {
                new Entry
                {
                    Id = Guid.NewGuid(),
                    UserId = "user-1",
                    Start = DateTime.Now.AddHours(-2),
                    End = DateTime.Now.AddHours(-1),
                    Title = "Admin Entry",
                    Description = "Admin work",
                    ShouldTimeId = _shouldTimes[0].Id
                },
                new Entry
                {
                    Id = Guid.NewGuid(),
                    UserId = "user-2",
                    Start = DateTime.Now.AddHours(-3),
                    End = DateTime.Now.AddHours(-2),
                    Title = "User Entry",
                    Description = "User work",
                    ShouldTimeId = _shouldTimes[0].Id
                }
            };

            _userManagerMock.Setup(e => e.GetUserAsync(It.IsAny<ClaimsPrincipal>()))
                .ReturnsAsync((ClaimsPrincipal principal) =>
                {
                    var userId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                    return _context.Users.FirstOrDefault(u => u.Id == userId);
                });

            _context.Users.AddRange(_users);
            _context.ShouldTimes.AddRange(_shouldTimes);
            _context.Entries.AddRange(_entries);
            _context.SaveChanges();

            _entriesController = new EntriesController(_userManagerMock.Object, _context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void AsAdmin()
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, "user-1"),
                new Claim(ClaimTypes.Role, "Administrator"),
                new Claim(ClaimTypes.Name, "admin@ema.il")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _entriesController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }

        private void AsUser(string userId)
        {
            var user = _users.FirstOrDefault(u => u.Id == userId);
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, user?.Email ?? "unknown@ema.il")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _entriesController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }

        [TestMethod]
        public async Task GetEntries_AsAdmin_ReturnsAllEntries()
        {
            AsAdmin();

            var result = await _entriesController.GetEntries(0, 50);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var entries = okResult.Value as List<TimeEntryDto>;
            Assert.IsNotNull(entries);
            Assert.AreEqual(2, entries.Count);
        }

        [TestMethod]
        public async Task GetEntries_AsRegularUser_ReturnsOnlyOwnEntries()
        {
            AsUser("user-2");

            var result = await _entriesController.GetEntries(0, 50);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var entries = okResult.Value as List<TimeEntryDto>;
            Assert.IsNotNull(entries);
            Assert.AreEqual(1, entries.Count);
            Assert.AreEqual("User Entry", entries[0].Title);
        }

        [TestMethod]
        public async Task GetEntries_WithPagination_ReturnsCorrectRange()
        {
            AsAdmin();

            var result = await _entriesController.GetEntries(0, 1);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var entries = okResult.Value as List<TimeEntryDto>;
            Assert.IsNotNull(entries);
            Assert.AreEqual(1, entries.Count);
        }

        [TestMethod]
        public async Task GetEntryById_ExistingEntry_ReturnsEntry()
        {
            AsAdmin();
            var entryId = _entries[0].Id;

            var result = await _entriesController.GetEntryById(entryId);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var entry = okResult.Value as TimeEntryDto;
            Assert.IsNotNull(entry);
            Assert.AreEqual(entryId, entry.Id);
        }

        [TestMethod]
        public async Task GetEntryById_NonExistingEntry_ReturnsNotFound()
        {
            AsAdmin();

            var result = await _entriesController.GetEntryById(Guid.NewGuid());

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetEntryById_AsUser_OtherUsersEntry_ReturnsNotFound()
        {
            AsUser("user-2");
            var entryId = _entries[0].Id;

            var result = await _entriesController.GetEntryById(entryId);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddEntry_ValidEntry_ReturnsCreatedEntry()
        {
            AsUser("user-2");

            var newEntry = new TimeEntryDto
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                Title = "New Entry",
                Description = "New work"
            };

            var result = await _entriesController.AddEntry(newEntry);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var entry = okResult.Value as TimeEntryDto;
            Assert.IsNotNull(entry);
            Assert.AreEqual(newEntry.Title, entry.Title);
        }

        [TestMethod]
        public async Task AddEntry_InvalidEntry_ReturnsBadRequest()
        {
            AsUser("user-2");

            var newEntry = new TimeEntryDto
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(-1),
                Title = "Invalid Entry",
                Description = "Invalid work"
            };

            var result = await _entriesController.AddEntry(newEntry);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task DeleteEntry_ExistingEntry_ReturnsNoContent()
        {
            AsAdmin();
            var entryId = _entries[0].Id;

            var result = await _entriesController.DeleteEntry(entryId);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteEntry_NonExistingEntry_ReturnsNotFound()
        {
            AsAdmin();

            var result = await _entriesController.DeleteEntry(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteEntry_AsUser_OtherUsersEntry_ReturnsNotFound()
        {
            AsUser("user-2");
            var entryId = _entries[0].Id;

            var result = await _entriesController.DeleteEntry(entryId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PatchEntry_ValidUpdate_ReturnsUpdatedEntry()
        {
            AsAdmin();
            var entryId = _entries[0].Id;

            var updatedEntry = new TimeEntryDto
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(2),
                Title = "Updated Title",
                Description = "Updated Description"
            };

            var result = await _entriesController.PatchEntry(entryId, updatedEntry);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var entry = okResult.Value as TimeEntryDto;
            Assert.IsNotNull(entry);
            Assert.AreEqual(updatedEntry.Title, entry.Title);
        }

        [TestMethod]
        public async Task PatchEntry_InvalidUpdate_ReturnsBadRequest()
        {
            AsAdmin();
            var entryId = _entries[0].Id;

            var updatedEntry = new TimeEntryDto
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(-1),
                Title = "Invalid",
                Description = "Invalid"
            };

            var result = await _entriesController.PatchEntry(entryId, updatedEntry);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task PatchEntry_NonExistingEntry_ReturnsNotFound()
        {
            AsAdmin();

            var updatedEntry = new TimeEntryDto
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                Title = "Test",
                Description = "Test"
            };

            var result = await _entriesController.PatchEntry(Guid.NewGuid(), updatedEntry);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task PatchEntry_AsUser_OtherUsersEntry_ReturnsNotFound()
        {
            AsUser("user-2");
            var entryId = _entries[0].Id;

            var updatedEntry = new TimeEntryDto
            {
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(1),
                Title = "Hacked",
                Description = "Hacked"
            };

            var result = await _entriesController.PatchEntry(entryId, updatedEntry);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}