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
using Zeiterfassungssoftware.Controller;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Controller.Tests
{
    [TestClass()]
    public class UsersControllerTests
    {
        private UsersController _usersController;
        private ApplicationDbContext _context;
        private Mock<UserManager<ApplicationUser>> _userManagerMock;
        private List<ApplicationUser> _users;

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

            _users = new List<ApplicationUser>
            {
                new ApplicationUser
                {
                    Id = "user-1",
                    NormalizedUserName = "MANAGER@EMA.IL",
                    UserName = "manager@ema.il",
                    Email = "manager@ema.il",
                    NormalizedEmail = "MANAGER@EMA.IL",
                    ClassId = Guid.NewGuid(),
                    EmailConfirmed = true
                },
                new ApplicationUser
                {
                    Id = "user-2",
                    NormalizedUserName = "USER@EMA.IL",
                    UserName = "user@ema.il",
                    Email = "user@ema.il",
                    NormalizedEmail = "USER@EMA.IL",
                    ClassId = Guid.NewGuid(),
                    EmailConfirmed = false
                }
            };

            _userManagerMock.Setup(e => e.FindByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((string id) => _context.Users.FirstOrDefault(e => e.Id == id));

            _userManagerMock.Setup(e => e.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<ApplicationUser, string>((user, pass) =>
                {
                    user.Id = Guid.NewGuid().ToString();
                    _context.Users.Add(user);
                    _context.SaveChanges();
                });

            _userManagerMock.Setup(e => e.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(e => e.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Success)
                .Callback<ApplicationUser>(u => {
                    _context.Users.Remove(u);
                    _context.SaveChanges();
                });

            _userManagerMock.Setup(e => e.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync("EPIC_TOKEN");

            _userManagerMock.Setup(e => e.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _context.Users.AddRange(_users);
            _context.SaveChanges();

            _usersController = new UsersController(_userManagerMock.Object, _context);
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
                new Claim(ClaimTypes.Role, "Administrator")
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _usersController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }

        private void AsUser(string userId)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            };
            var identity = new ClaimsIdentity(claims, "TestAuthType");
            var claimsPrincipal = new ClaimsPrincipal(identity);

            _usersController.ControllerContext = new ControllerContext
            {
                HttpContext = new DefaultHttpContext { User = claimsPrincipal }
            };
        }


        [TestMethod]
        public async Task GetUsers_ReturnsAllUsers()
        {
            
            AsAdmin();
            
            var result = await _usersController.GetUsers();

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var users = okResult.Value as List<UserDto>;
            Assert.IsNotNull(users);
            Assert.AreEqual(2, users.Count);
        }


        [TestMethod]
        public async Task GetUserById_ExistingUser_ReturnsUser()
        {
            AsAdmin();

            var result = await _usersController.GetUserById("user-1");

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var user = okResult.Value as UserDto;
            Assert.IsNotNull(user);
            Assert.AreEqual("user-1", user.Id);
        }

        [TestMethod]
        public async Task GetUserById_NonExistingUser_ReturnsNotFound()
        {
            AsAdmin();

            var result = await _usersController.GetUserById("non-existing-id");

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task AddUsers_ValidUser_ReturnsCreatedUser()
        {
            AsAdmin();

            var newUser = new UserDto
            {
                UserName = "newuser@ema.il",
                Email = "newuser@ema.il",
                NormalizedEmail = "NEWUSER@EMA.IL",
                Password = "Password123!",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.AddUsers(newUser);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var user = okResult.Value as UserDto;
            Assert.IsNotNull(user);
            Assert.AreEqual(newUser.Email, user.Email);
        }

        [TestMethod]
        public async Task AddUsers_ShortPassword_ReturnsBadRequest()
        {
            AsAdmin();

            var newUser = new UserDto
            {
                UserName = "newuser@ema.il",
                Email = "newuser@ema.il",
                NormalizedEmail = "NEWUSER@EMA.IL",
                Password = "123",
                ClassId = Guid.NewGuid()
            };
            
            var result = await _usersController.AddUsers(newUser);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task AddUsers_EmptyPassword_ReturnsBadRequest()
        {
            AsAdmin();

            var newUser = new UserDto
            {
                UserName = "newuser@ema.il",
                Email = "newuser@ema.il",
                NormalizedEmail = "NEWUSER@EMA.IL",
                Password = "",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.AddUsers(newUser);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task AddUsers_DuplicateEmail_ReturnsConflict()
        {
            
            AsAdmin();
            var newUser = new UserDto
            {
                UserName = "manager@ema.il",
                Email = "manager@ema.il",
                NormalizedEmail = "MANAGER@EMA.IL",
                Password = "Password123!",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.AddUsers(newUser);

            Assert.IsInstanceOfType(result.Result, typeof(ConflictResult));
        }

        [TestMethod]
        public async Task AddUsers_CreateFails_ReturnsBadRequest()
        {
            AsAdmin();

            _userManagerMock.Setup(e => e.CreateAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Creation failed" }));

            var newUser = new UserDto
            {
                UserName = "newuser@ema.il",
                Email = "newuser@ema.il",
                NormalizedEmail = "NEWUSER@EMA.IL",
                Password = "Password123!",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.AddUsers(newUser);
            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }


        [TestMethod]
        public async Task DeleteUser_ExistingUser_ReturnsNoContent()
        {
            AsAdmin();

            var result = await _usersController.DeleteUser("user-2");
            Assert.IsInstanceOfType(result, typeof(NoContentResult));
        }

        [TestMethod]
        public async Task DeleteUser_NonExistingUser_ReturnsNotFound()
        {
            
            AsAdmin();

            var result = await _usersController.DeleteUser("non-existing-id");
            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteUser_DeleteFails_ReturnsBadRequest()
        {
            
            AsAdmin();

            _userManagerMock.Setup(e => e.DeleteAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Deletion failed" }));

            
            var result = await _usersController.DeleteUser("user-1");

            Assert.IsInstanceOfType(result, typeof(BadRequestObjectResult));
        }


        [TestMethod]
        public async Task UpdateUser_AsAdmin_UpdatesAllFields()
        {
            AsAdmin();

            var updatedUser = new UserDto
            {
                Id = "user-2",
                UserName = "updated@ema.il",
                Email = "updated@ema.il",
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnabled = true,
                AccessFailedCount = 5,
                EmailConfirmed = true,
                ClassId = Guid.NewGuid(),
                LockoutEnd = DateTime.UtcNow.AddDays(1),
                Password = "NewPassword123!"
            };

            var result = await _usersController.UpdateUser("user-2", updatedUser);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var okResult = result.Result as OkObjectResult;
            var user = okResult.Value as UserDto;
            Assert.IsNotNull(user);
            Assert.AreEqual(updatedUser.UserName, user.UserName);
        }

        [TestMethod]
        public async Task UpdateUser_AsRegularUser_UpdatesOnlyClassId()
        {
            AsUser("user-2");

            var newClassId = Guid.NewGuid();
            var updatedUser = new UserDto
            {
                Id = "user-2",
                UserName = "should-not-update@ema.il",
                Email = "should-not-update@ema.il",
                ClassId = newClassId
            };

            var result = await _usersController.UpdateUser("user-2", updatedUser);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            _userManagerMock.Verify(e => e.UpdateAsync(It.Is<ApplicationUser>(e => e.ClassId == newClassId)), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUser_AsRegularUserUpdatingOtherUser_ReturnsBadRequest()
        {
            AsUser("user-1");

            var updatedUser = new UserDto
            {
                Id = "user-2",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.UpdateUser("user-2", updatedUser);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestResult));
        }

        [TestMethod]
        public async Task UpdateUser_NonExistingUser_ReturnsNotFound()
        {
            AsAdmin();

            var updatedUser = new UserDto
            {
                Id = "non-existing-id",
                UserName = "test@ema.il",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.UpdateUser("non-existing-id", updatedUser);

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateUser_UpdateFails_ReturnsBadRequest()
        {
            AsAdmin();

            _userManagerMock.Setup(e => e.UpdateAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Update failed" }));

            var updatedUser = new UserDto
            {
                Id = "user-1",
                UserName = "updated@ema.il",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.UpdateUser("user-1", updatedUser);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateUser_AsAdmin_WithPassword_ResetsPassword()
        {
            AsAdmin();

            var updatedUser = new UserDto
            {
                Id = "user-1",
                UserName = "manager@ema.il",
                Password = "NewPassword123!",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.UpdateUser("user-1", updatedUser);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            _userManagerMock.Verify(e => e.GeneratePasswordResetTokenAsync(It.IsAny<ApplicationUser>()), Times.Once);
            _userManagerMock.Verify(e => e.ResetPasswordAsync(It.IsAny<ApplicationUser>(), "EPIC_TOKEN", "NewPassword123!"), Times.Once);
        }

        [TestMethod]
        public async Task UpdateUser_AsAdmin_PasswordResetFails_ReturnsBadRequest()
        {
            AsAdmin();

            _userManagerMock.Setup(e => e.ResetPasswordAsync(It.IsAny<ApplicationUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Password reset failed" }));

            var updatedUser = new UserDto
            {
                Id = "user-1",
                UserName = "manager@ema.il",
                Password = "NewPassword123!",
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.UpdateUser("user-1", updatedUser);

            Assert.IsInstanceOfType(result.Result, typeof(BadRequestObjectResult));
        }

        [TestMethod]
        public async Task UpdateUser_AsAdmin_WithLockoutEndMinValue_ClearsLockout()
        {
            AsAdmin();
            var updatedUser = new UserDto
            {
                Id = "user-1",
                UserName = "manager@ema.il",
                LockoutEnd = DateTime.MinValue,
                ClassId = Guid.NewGuid()
            };

            var result = await _usersController.UpdateUser("user-1", updatedUser);

            Assert.IsInstanceOfType(result.Result, typeof(OkObjectResult));
            var user = _users.First(e => e.Id == "user-1");
            Assert.IsNull(user.LockoutEnd);
        }
    }
}