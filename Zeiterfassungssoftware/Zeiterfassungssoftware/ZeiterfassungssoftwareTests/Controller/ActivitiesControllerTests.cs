using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Zeiterfassungssoftware.Controller;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Controller.Tests
{
    [TestClass]
    public class ActivitiesControllerTests
    {
        private ApplicationDbContext _context;
        private ActivitiesController _controller;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new ActivitiesController(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        private void AsUser(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };

            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }

        [TestMethod]
        public async Task GetAllDescriptions_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var result = await _controller.GetAllDescriptions();

            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task GetAllDescriptions_ReturnsUserDescriptions()
        {
            var userId = "user1";
            AsUser(userId);

            var description1 = new ActivityDescription { Id = Guid.NewGuid(), UserId = userId, Value = "Description 1" };
            var description2 = new ActivityDescription { Id = Guid.NewGuid(), UserId = "otherUser", Value = "Description 2" };
            _context.ActivityDescriptions.AddRange(description1, description2);
            await _context.SaveChangesAsync();

            var result = await _controller.GetAllDescriptions();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var descriptions = okResult.Value as List<ActivityDescriptionDto>;
            Assert.IsNotNull(descriptions);
            Assert.AreEqual(1, descriptions.Count);
        }

        [TestMethod]
        public async Task GetAllDescriptions_ReturnsEmptyList_WhenNoDescriptionsExist()
        {
            var userId = "user1";
            AsUser(userId);

            var result = await _controller.GetAllDescriptions();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var descriptions = okResult.Value as List<ActivityDescriptionDto>;
            Assert.IsNotNull(descriptions);
            Assert.AreEqual(0, descriptions.Count);
        }

        [TestMethod]
        public async Task AddDescription_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var descriptionDto = new ActivityDescriptionDto { Value = "Test Description" };
            var result = await _controller.AddDescription(descriptionDto);

            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task AddDescription_CreatesDescription()
        {
            var userId = "user1";
            AsUser(userId);

            var descriptionDto = new ActivityDescriptionDto { Value = "Test Description", Favorite = true };
            var result = await _controller.AddDescription(descriptionDto);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var createdDescription = okResult.Value as ActivityDescriptionDto;
            Assert.IsNotNull(createdDescription);
            Assert.AreEqual("Test Description", createdDescription.Value);
            Assert.AreEqual(1, await _context.ActivityDescriptions.CountAsync());
        }

        [TestMethod]
        public async Task DeleteDescription_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var result = await _controller.DeleteDescription(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task DeleteDescription_ReturnsNotFound_WhenDescriptionDoesNotExist()
        {
            var userId = "user1";
            AsUser(userId);

            var result = await _controller.DeleteDescription(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteDescription_DeletesDescription_WhenUserOwnsDescription()
        {
            var userId = "user1";
            var descriptionId = Guid.NewGuid();
            AsUser(userId);

            var description = new ActivityDescription { Id = descriptionId, UserId = userId, Value = "Test" };
            _context.ActivityDescriptions.Add(description);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteDescription(descriptionId);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.AreEqual(0, await _context.ActivityDescriptions.CountAsync());
        }

        [TestMethod]
        public async Task DeleteDescription_ReturnsNotFound_WhenUserDoesNotOwnDescription()
        {
            var userId = "user1";
            var descriptionId = Guid.NewGuid();
            AsUser(userId);

            var description = new ActivityDescription { Id = descriptionId, UserId = "otherUser", Value = "Test" };
            _context.ActivityDescriptions.Add(description);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteDescription(descriptionId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(1, await _context.ActivityDescriptions.CountAsync());
        }

        [TestMethod]
        public async Task UpdateDescription_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var descriptionDto = new ActivityDescriptionDto { Value = "Updated" };
            var result = await _controller.UpdateDescription(Guid.NewGuid(), descriptionDto);

            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task UpdateDescription_ReturnsNotFound_WhenDescriptionDoesNotExist()
        {
            var userId = "user1";
            AsUser(userId);

            var descriptionDto = new ActivityDescriptionDto { Value = "Updated" };
            var result = await _controller.UpdateDescription(Guid.NewGuid(), descriptionDto);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateDescription_UpdatesDescription_WhenUserOwnsDescription()
        {
            var userId = "user1";
            var descriptionId = Guid.NewGuid();
            AsUser(userId);

            var description = new ActivityDescription { Id = descriptionId, UserId = userId, Value = "Old Value", Favorite = false };
            _context.ActivityDescriptions.Add(description);
            await _context.SaveChangesAsync();

            var descriptionDto = new ActivityDescriptionDto { Value = "New Value", Favorite = true };
            var result = await _controller.UpdateDescription(descriptionId, descriptionDto);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var updatedDescription = okResult.Value as ActivityDescriptionDto;
            Assert.IsNotNull(updatedDescription);
            Assert.AreEqual("New Value", updatedDescription.Value);
            Assert.AreEqual(true, updatedDescription.Favorite);
        }

        [TestMethod]
        public async Task UpdateDescription_ReturnsNotFound_WhenUserDoesNotOwnDescription()
        {
            var userId = "user1";
            var descriptionId = Guid.NewGuid();
            AsUser(userId);

            var description = new ActivityDescription { Id = descriptionId, UserId = "otherUser", Value = "Old Value" };
            _context.ActivityDescriptions.Add(description);
            await _context.SaveChangesAsync();

            var descriptionDto = new ActivityDescriptionDto { Value = "New Value" };
            var result = await _controller.UpdateDescription(descriptionId, descriptionDto);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task GetAllTitles_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var result = await _controller.GetAllTitles();

            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task GetAllTitles_ReturnsUserTitles()
        {
            var userId = "user1";
            AsUser(userId);

            var title1 = new ActivityTitle { Id = Guid.NewGuid(), UserId = userId, Title = "Title 1" };
            var title2 = new ActivityTitle { Id = Guid.NewGuid(), UserId = "otherUser", Title = "Title 2" };
            _context.Activitys.AddRange(title1, title2);
            await _context.SaveChangesAsync();

            var result = await _controller.GetAllTitles();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var titles = okResult.Value as List<ActivityTitleDto>;
            Assert.IsNotNull(titles);
            Assert.AreEqual(1, titles.Count);
        }

        [TestMethod]
        public async Task GetAllTitles_ReturnsEmptyList_WhenNoTitlesExist()
        {
            var userId = "user1";
            AsUser(userId);

            var result = await _controller.GetAllTitles();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var titles = okResult.Value as List<ActivityTitleDto>;
            Assert.IsNotNull(titles);
            Assert.AreEqual(0, titles.Count);
        }

        [TestMethod]
        public async Task AddTitle_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var titleDto = new ActivityTitleDto { Value = "Test Title" };
            var result = await _controller.AddTitle(titleDto);

            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task AddTitle_CreatesTitle()
        {
            var userId = "user1";
            AsUser(userId);

            var titleDto = new ActivityTitleDto { Value = "Test Title", Favorite = true };
            var result = await _controller.AddTitle(titleDto);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var createdTitle = okResult.Value as ActivityTitleDto;
            Assert.IsNotNull(createdTitle);
            Assert.AreEqual("Test Title", createdTitle.Value);
            Assert.AreEqual(1, await _context.Activitys.CountAsync());
        }

        [TestMethod]
        public async Task DeleteTitle_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var result = await _controller.DeleteTitle(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task DeleteTitle_ReturnsNotFound_WhenTitleDoesNotExist()
        {
            var userId = "user1";
            AsUser(userId);

            var result = await _controller.DeleteTitle(Guid.NewGuid());

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteTitle_DeletesTitle_WhenUserOwnsTitle()
        {
            var userId = "user1";
            var titleId = Guid.NewGuid();
            AsUser(userId);

            var title = new ActivityTitle { Id = titleId, UserId = userId, Title = "Test" };
            _context.Activitys.Add(title);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteTitle(titleId);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));
            Assert.AreEqual(0, await _context.Activitys.CountAsync());
        }

        [TestMethod]
        public async Task DeleteTitle_ReturnsNotFound_WhenUserDoesNotOwnTitle()
        {
            var userId = "user1";
            var titleId = Guid.NewGuid();
            AsUser(userId);

            var title = new ActivityTitle { Id = titleId, UserId = "otherUser", Title = "Test" };
            _context.Activitys.Add(title);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteTitle(titleId);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
            Assert.AreEqual(1, await _context.Activitys.CountAsync());
        }

        [TestMethod]
        public async Task UpdateTitle_ReturnsUnauthorized_WhenUserIdIsNull()
        {
            _controller.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = new ClaimsPrincipal() }
            };

            var titleDto = new ActivityTitleDto { Value = "Updated" };
            var result = await _controller.UpdateTitle(Guid.NewGuid(), titleDto);

            Assert.IsInstanceOfType(result.Result, typeof(UnauthorizedResult));
        }

        [TestMethod]
        public async Task UpdateTitle_ReturnsNotFound_WhenTitleDoesNotExist()
        {
            var userId = "user1";
            AsUser(userId);

            var titleDto = new ActivityTitleDto { Value = "Updated" };
            var result = await _controller.UpdateTitle(Guid.NewGuid(), titleDto);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateTitle_UpdatesTitle_WhenUserOwnsTitle()
        {
            var userId = "user1";
            var titleId = Guid.NewGuid();
            AsUser(userId);

            var title = new ActivityTitle { Id = titleId, UserId = userId, Title = "Old Title", Favorite = false };
            _context.Activitys.Add(title);
            await _context.SaveChangesAsync();

            var titleDto = new ActivityTitleDto { Value = "New Title", Favorite = true };
            var result = await _controller.UpdateTitle(titleId, titleDto);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var updatedTitle = okResult.Value as ActivityTitleDto;
            Assert.IsNotNull(updatedTitle);
            Assert.AreEqual("New Title", updatedTitle.Value);
            Assert.AreEqual(true, updatedTitle.Favorite);
        }

        [TestMethod]
        public async Task UpdateTitle_ReturnsNotFound_WhenUserDoesNotOwnTitle()
        {
            var userId = "user1";
            var titleId = Guid.NewGuid();
            AsUser(userId);

            var title = new ActivityTitle { Id = titleId, UserId = "otherUser", Title = "Old Title" };
            _context.Activitys.Add(title);
            await _context.SaveChangesAsync();

            var titleDto = new ActivityTitleDto { Value = "New Title" };
            var result = await _controller.UpdateTitle(titleId, titleDto);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }
    }
}