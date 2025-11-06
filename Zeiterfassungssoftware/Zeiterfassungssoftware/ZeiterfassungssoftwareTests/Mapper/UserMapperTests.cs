using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Mapper.Tests
{
    [TestClass()]
    public class UserMapperTests
    {
        [TestMethod()]
        public void ToDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => UserMapper.ToDTO(null));
        }

        [TestMethod()]
        public void FromDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => UserMapper.FromDTO(null));
        }

        [TestMethod()]
        public void ToDTOValid()
        {
            var applicationUser = new ApplicationUser()
            {
                Id = "sdfsadfasd",
                ClassId = Guid.Empty,
                UserName = "hallo@gmail.com",
                NormalizedUserName = "hallo@gmail.com",
                Email = "hallo@gmail.com",
                NormalizedEmail = "hallo@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.Today,
                LockoutEnabled = true,
                AccessFailedCount = 1
            };

            var userDto = new UserDto()
            {
                Id = "sdfsadfasd",
                ClassId = Guid.Empty,
                UserName = "hallo@gmail.com",
                NormalizedUserName = "hallo@gmail.com",
                Email = "hallo@gmail.com",
                NormalizedEmail = "hallo@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.Today,
                LockoutEnabled = true,
                AccessFailedCount = 1
            };

            Assert.AreEqual(userDto, UserMapper.ToDTO(applicationUser));
        }

        [TestMethod()]
        public void FromDTOValid()
        {
            var applicationUser = new ApplicationUser()
            {
                Id = "sdfsadfasd",
                ClassId = Guid.Empty,
                UserName = "hallo@gmail.com",
                NormalizedUserName = "hallo@gmail.com",
                Email = "hallo@gmail.com",
                NormalizedEmail = "hallo@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.Today,
                LockoutEnabled = true,
                AccessFailedCount = 1
            };

            var userDto = new UserDto()
            {
                Id = "sdfsadfasd",
                ClassId = Guid.Empty,
                UserName = "hallo@gmail.com",
                NormalizedUserName = "hallo@gmail.com",
                Email = "hallo@gmail.com",
                NormalizedEmail = "hallo@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.Today,
                LockoutEnabled = true,
                AccessFailedCount = 1
            };

            Assert.AreEqual(applicationUser, UserMapper.FromDTO(userDto));
        }

        [TestMethod()]
        public void FromDTONotEqual()
        {
            var applicationUser = new ApplicationUser()
            {
                Id = "asdfasdf",
                ClassId = Guid.Empty,
                UserName = "asdf@gmail.com",
                NormalizedUserName = "adf@gmail.com",
                Email = "asdf@gmail.com",
                NormalizedEmail = "asdf@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.Today,
                LockoutEnabled = true,
                AccessFailedCount = 1
            };

            var userDto = new UserDto()
            {
                Id = "sdfsadfasd",
                ClassId = Guid.Empty,
                UserName = "hallo@gmail.com",
                NormalizedUserName = "hallo@gmail.com",
                Email = "hallo@gmail.com",
                NormalizedEmail = "hallo@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.Today,
                LockoutEnabled = true,
                AccessFailedCount = 1
            };

            Assert.AreNotEqual(applicationUser, UserMapper.FromDTO(userDto));
        }

        [TestMethod()]
        public void ToDTONotEqual()
        {
            var applicationUser = new ApplicationUser()
            {
                Id = Guid.NewGuid().ToString(),
                ClassId = Guid.Empty,
                UserName = "asdf@gmail.com",
                NormalizedUserName = "asdf@gmail.com",
                Email = "asdf@gmail.com",
                NormalizedEmail = "asdf@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.Today,
                LockoutEnabled = true,
                AccessFailedCount = 1
            };

            var userDto = new UserDto()
            {
                Id = Guid.NewGuid().ToString(),
                ClassId = Guid.Empty,
                UserName = "hallo@gmail.com",
                NormalizedUserName = "hallo@gmail.com",
                Email = "hallo@gmail.com",
                NormalizedEmail = "hallo@gmail.com",
                EmailConfirmed = true,
                PhoneNumber = string.Empty,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = true,
                LockoutEnd = DateTime.Today,
                LockoutEnabled = true,
                AccessFailedCount = 1
            };

            Assert.AreNotEqual(userDto, UserMapper.ToDTO(applicationUser));
        }
    }
}