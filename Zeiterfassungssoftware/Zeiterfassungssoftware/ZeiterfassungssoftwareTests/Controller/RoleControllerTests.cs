using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zeiterfassungssoftware.Controller;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.SharedData.Roles;

namespace Zeiterfassungssoftware.Controller.Tests
{
    [TestClass]
    public class RoleControllerTests
    {
        private ApplicationDbContext _context;
        private RolesController _controller;

        [TestInitialize]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            _context = new ApplicationDbContext(options);
            _controller = new RolesController(_context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [TestMethod]
        public async Task GetRoles_ReturnsEmptyList_WhenNoRolesExist()
        {
            var result = await _controller.GetRoles();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var roles = okResult.Value as List<RoleDto>;
            Assert.IsNotNull(roles);
            Assert.AreEqual(0, roles.Count);
        }

        [TestMethod]
        public async Task GetRoles_ReturnsAllRoles_WhenRolesExist()
        {
            var role1 = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };
            var role2 = new IdentityRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            _context.Roles.AddRange(role1, role2);
            await _context.SaveChangesAsync();

            var result = await _controller.GetRoles();

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var roles = okResult.Value as List<RoleDto>;
            Assert.IsNotNull(roles);
            Assert.AreEqual(2, roles.Count);
        }

        [TestMethod]
        public async Task GetRoleById_ReturnsRole_WhenRoleExists()
        {
            var roleId = Guid.NewGuid().ToString();
            var role = new IdentityRole
            {
                Id = roleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            var result = await _controller.GetRoleById(roleId);

            var okResult = result.Result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var roleDto = okResult.Value as RoleDto;
            Assert.IsNotNull(roleDto);
            Assert.AreEqual(roleId, roleDto.Id);
            Assert.AreEqual("Admin", roleDto.Name);
        }

        [TestMethod]
        public async Task GetRoleById_ReturnsNotFound_WhenRoleDoesNotExist()
        {
            var result = await _controller.GetRoleById("nonexistent-id");

            Assert.IsInstanceOfType(result.Result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task CreateRole_CreatesNewRole_WithValidData()
        {
            var roleDto = new RoleDto
            {
                Name = "Manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var result = await _controller.CreateRole(roleDto);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var createdRole = okResult.Value as RoleDto;
            Assert.IsNotNull(createdRole);
            Assert.IsFalse(string.IsNullOrEmpty(createdRole.Id));
            Assert.AreEqual("Manager", createdRole.Name);

            var dbRole = await _context.Roles.FirstOrDefaultAsync(r => r.Name == "Manager");
            Assert.IsNotNull(dbRole);
        }

        [TestMethod]
        public async Task CreateRole_ReturnsBadRequest_WithInvalidData()
        {
            var roleDto = new RoleDto
            {
                Name = "",
                NormalizedName = "",
                ConcurrencyStamp = ""
            };

            var result = await _controller.CreateRole(roleDto);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Invalid data", badRequestResult.Value);
        }

        [TestMethod]
        public async Task UpdateRole_UpdatesExistingRole_WithValidData()
        {
            var roleId = Guid.NewGuid().ToString();
            var existingRole = new IdentityRole
            {
                Id = roleId,
                Name = "OldName",
                NormalizedName = "OLDNAME",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            _context.Roles.Add(existingRole);
            await _context.SaveChangesAsync();

            var updateDto = new RoleDto
            {
                Id = roleId,
                Name = "NewName",
                NormalizedName = "NEWNAME",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var result = await _controller.UpdateRole(roleId, updateDto);

            var okResult = result as OkObjectResult;
            Assert.IsNotNull(okResult);
            var updatedRole = okResult.Value as RoleDto;
            Assert.IsNotNull(updatedRole);
            Assert.AreEqual("NEWNAME", updatedRole.Name);
            Assert.AreEqual("NEWNAME", updatedRole.NormalizedName);

            var dbRole = await _context.Roles.FindAsync(roleId);
            Assert.AreEqual("NEWNAME", dbRole.Name);
        }

        [TestMethod]
        public async Task UpdateRole_ReturnsNotFound_WhenRoleDoesNotExist()
        {
            var roleDto = new RoleDto
            {
                Name = "UpdatedName",
                NormalizedName = "UPDATEDNAME",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var result = await _controller.UpdateRole("nonexistent-id", roleDto);

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task UpdateRole_ReturnsBadRequest_WithInvalidData()
        {
            var roleId = Guid.NewGuid().ToString();
            var existingRole = new IdentityRole
            {
                Id = roleId,
                Name = "ExistingRole",
                NormalizedName = "EXISTINGROLE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            _context.Roles.Add(existingRole);
            await _context.SaveChangesAsync();

            var invalidDto = new RoleDto
            {
                Name = "",
                NormalizedName = "",
                ConcurrencyStamp = ""
            };

            var result = await _controller.UpdateRole(roleId, invalidDto);

            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual("Invalid data", badRequestResult.Value);
        }

        [TestMethod]
        public async Task DeleteRole_DeletesRole_WhenRoleExists()
        {
            var roleId = Guid.NewGuid().ToString();
            var role = new IdentityRole
            {
                Id = roleId,
                Name = "ToDelete",
                NormalizedName = "TODELETE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();

            var result = await _controller.DeleteRole(roleId);

            Assert.IsInstanceOfType(result, typeof(NoContentResult));

            var deletedRole = await _context.Roles.FindAsync(roleId);
            Assert.IsNull(deletedRole);
        }

        [TestMethod]
        public async Task DeleteRole_ReturnsNotFound_WhenRoleDoesNotExist()
        {
            var result = await _controller.DeleteRole("nonexistent-id");

            Assert.IsInstanceOfType(result, typeof(NotFoundResult));
        }

        [TestMethod]
        public async Task DeleteRole_DoesNotAffectOtherRoles()
        {
            var roleId1 = Guid.NewGuid().ToString();
            var roleId2 = Guid.NewGuid().ToString();

            var role1 = new IdentityRole
            {
                Id = roleId1,
                Name = "ToDelete",
                NormalizedName = "TODELETE",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            var role2 = new IdentityRole
            {
                Id = roleId2,
                Name = "ToKeep",
                NormalizedName = "TOKEEP",
                ConcurrencyStamp = Guid.NewGuid().ToString()
            };

            _context.Roles.AddRange(role1, role2);
            await _context.SaveChangesAsync();

            await _controller.DeleteRole(roleId1);

            var remainingRole = await _context.Roles.FindAsync(roleId2);
            Assert.IsNotNull(remainingRole);
            Assert.AreEqual("ToKeep", remainingRole.Name);
        }
    }
}