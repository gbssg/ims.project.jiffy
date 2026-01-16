using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Roles;

namespace Zeiterfassungssoftware.Mapper.Tests
{
    [TestClass()]
    public class RoleMapperTests
    {
        [TestMethod]
        public void ToDto_ConvertsIdentityRoleToDto_WithAllFields()
        {
            var identityRole = new IdentityRole
            {
                Id = "123",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ToDto(identityRole);

            Assert.IsNotNull(result);
            Assert.AreEqual("123", result.Id);
            Assert.AreEqual("Admin", result.Name);
            Assert.AreEqual("ADMIN", result.NormalizedName);
            Assert.AreEqual("stamp123", result.ConcurrencyStamp);
        }

        [TestMethod]
        public void ToDto_HandlesNullName_ReturnsEmptyString()
        {
            var identityRole = new IdentityRole
            {
                Id = "123",
                Name = null,
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ToDto(identityRole);

            Assert.AreEqual("", result.Name);
        }

        [TestMethod]
        public void ToDto_HandlesNullNormalizedName_ReturnsEmptyString()
        {
            var identityRole = new IdentityRole
            {
                Id = "123",
                Name = "Admin",
                NormalizedName = null,
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ToDto(identityRole);

            Assert.AreEqual("", result.NormalizedName);
        }

        [TestMethod]
        public void ToDto_HandlesNullConcurrencyStamp_ReturnsEmptyString()
        {
            var identityRole = new IdentityRole
            {
                Id = "123",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = null
            };

            var result = RoleMapper.ToDto(identityRole);

            Assert.AreEqual("", result.ConcurrencyStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ToDto_ThrowsArgumentNullException_WhenIdentityRoleIsNull()
        {
            RoleMapper.ToDto(null);
        }

        [TestMethod]
        public void FromDto_ConvertsRoleDtoToIdentityRole_WithAllFields()
        {
            var roleDto = new RoleDto
            {
                Id = "456",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "stamp456"
            };

            var result = RoleMapper.FromDto(roleDto);

            Assert.IsNotNull(result);
            Assert.AreEqual("456", result.Id);
            Assert.AreEqual("User", result.Name);
            Assert.AreEqual("USER", result.NormalizedName);
            Assert.AreEqual("stamp456", result.ConcurrencyStamp);
        }

        [TestMethod]
        public void FromDto_HandlesEmptyStrings_PreservesEmptyValues()
        {
            var roleDto = new RoleDto
            {
                Id = "",
                Name = "",
                NormalizedName = "",
                ConcurrencyStamp = ""
            };

            var result = RoleMapper.FromDto(roleDto);

            Assert.IsNotNull(result);
            Assert.AreEqual("", result.Id);
            Assert.AreEqual("", result.Name);
            Assert.AreEqual("", result.NormalizedName);
            Assert.AreEqual("", result.ConcurrencyStamp);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FromDto_ThrowsArgumentNullException_WhenRoleDtoIsNull()
        {
            RoleMapper.FromDto(null);
        }

        [TestMethod]
        public void ValidateDto_ReturnsTrue_WhenDtoIsValid()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsFalse_WhenDtoIsNull()
        {
            var result = RoleMapper.ValidateDto(null);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsFalse_WhenNameIsNull()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = null,
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsFalse_WhenNameIsEmpty()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = "",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsFalse_WhenNameIsWhitespace()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = "   ",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsFalse_WhenNormalizedNameIsNull()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = "Admin",
                NormalizedName = null,
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsFalse_WhenNormalizedNameIsEmpty()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = "Admin",
                NormalizedName = "",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsFalse_WhenNormalizedNameIsWhitespace()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = "Admin",
                NormalizedName = "   ",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsFalse_WhenBothNameAndNormalizedNameAreInvalid()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = "",
                NormalizedName = "",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsTrue_WhenConcurrencyStampIsEmpty()
        {
            var roleDto = new RoleDto
            {
                Id = "123",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = ""
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateDto_ReturnsTrue_WhenIdIsEmpty()
        {
            var roleDto = new RoleDto
            {
                Id = "",
                Name = "Admin",
                NormalizedName = "ADMIN",
                ConcurrencyStamp = "stamp123"
            };

            var result = RoleMapper.ValidateDto(roleDto);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ToDto_FromDto_RoundTrip_PreservesAllData()
        {
            var originalRole = new IdentityRole
            {
                Id = "789",
                Name = "Manager",
                NormalizedName = "MANAGER",
                ConcurrencyStamp = "stamp789"
            };

            var dto = RoleMapper.ToDto(originalRole);
            var convertedRole = RoleMapper.FromDto(dto);

            Assert.AreEqual(originalRole.Id, convertedRole.Id);
            Assert.AreEqual(originalRole.Name, convertedRole.Name);
            Assert.AreEqual(originalRole.NormalizedName, convertedRole.NormalizedName);
            Assert.AreEqual(originalRole.ConcurrencyStamp, convertedRole.ConcurrencyStamp);
        }

        [TestMethod]
        public void FromDto_ToDto_RoundTrip_PreservesAllData()
        {
            var originalDto = new RoleDto
            {
                Id = "999",
                Name = "Guest",
                NormalizedName = "GUEST",
                ConcurrencyStamp = "stamp999"
            };

            var identityRole = RoleMapper.FromDto(originalDto);
            var convertedDto = RoleMapper.ToDto(identityRole);

            Assert.AreEqual(originalDto.Id, convertedDto.Id);
            Assert.AreEqual(originalDto.Name, convertedDto.Name);
            Assert.AreEqual(originalDto.NormalizedName, convertedDto.NormalizedName);
            Assert.AreEqual(originalDto.ConcurrencyStamp, convertedDto.ConcurrencyStamp);
        }
    }
}