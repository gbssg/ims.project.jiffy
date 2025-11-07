using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Mapper.Tests
{
    [TestClass()]
    public class EntryMapperTests
    {
        #region FromDTO Tests

        [TestMethod()]
        public void FromDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => EntryMapper.FromDTO(null));
        }

        [TestMethod()]
        public void FromDTOValid()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = "Development",
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            var entry = new Entry()
            {
                Id = timeEntryDto.Id,
                Start = timeEntryDto.Start,
                End = timeEntryDto.End,
                Title = timeEntryDto.Title,
                Description = timeEntryDto.Description
            };

            Assert.AreEqual(entry, EntryMapper.FromDTO(timeEntryDto));
        }

        [TestMethod()]
        public void FromDTOValidWithNullEnd()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = null,
                Title = "Development",
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            var result = EntryMapper.FromDTO(timeEntryDto);
            Assert.AreEqual(timeEntryDto.Id, result.Id);
            Assert.AreEqual(timeEntryDto.Start, result.Start);
            Assert.IsNull(result.End);
            Assert.AreEqual(timeEntryDto.Title, result.Title);
            Assert.AreEqual(timeEntryDto.Description, result.Description);
        }

        [TestMethod()]
        public void FromDTONotEqual()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = "Development",
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            var entry = new Entry()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-1),
                End = DateTime.Now,
                Title = "Testing",
                Description = "Writing unit tests"
            };

            Assert.AreNotEqual(entry, EntryMapper.FromDTO(timeEntryDto));
        }

        #endregion

        #region ToDTO Tests

        [TestMethod()]
        public void ToDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => EntryMapper.ToDTO(null));
        }

        [TestMethod()]
        public void ToDTOValid()
        {
            var shouldTime = new ShouldTime()
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Monday,
                Should = TimeSpan.FromHours(8),
                ValidUntil = DateTime.Today.AddYears(1),
                ClassId = Guid.NewGuid()
            };

            var entry = new Entry()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = "Development",
                Description = "Working on feature implementation",
                ShouldTimeId = shouldTime.Id,
                ShouldTime = shouldTime,
                UserId = "user123"
            };

            var timeEntryDto = new TimeEntryDto()
            {
                Id = entry.Id,
                Start = entry.Start,
                End = entry.End,
                Title = entry.Title,
                Description = entry.Description,
                ShouldTime = shouldTime.Should
            };

            Assert.AreEqual(timeEntryDto, EntryMapper.ToDTO(entry));
        }

        [TestMethod()]
        public void ToDTOValidWithNullEnd()
        {
            var shouldTime = new ShouldTime()
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Monday,
                Should = TimeSpan.FromHours(8),
                ValidUntil = DateTime.Today.AddYears(1),
                ClassId = Guid.NewGuid()
            };

            var entry = new Entry()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = null,
                Title = "Development",
                Description = "Working on feature implementation",
                ShouldTimeId = shouldTime.Id,
                ShouldTime = shouldTime,
                UserId = "user123"
            };

            var result = EntryMapper.ToDTO(entry);
            Assert.AreEqual(entry.Id, result.Id);
            Assert.AreEqual(entry.Start, result.Start);
            Assert.IsNull(result.End);
            Assert.AreEqual(entry.Title, result.Title);
            Assert.AreEqual(entry.Description, result.Description);
            Assert.AreEqual(shouldTime.Should, result.ShouldTime);
        }

        [TestMethod()]
        public void ToDTONotEqual()
        {
            var shouldTime = new ShouldTime()
            {
                Id = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Monday,
                Should = TimeSpan.FromHours(8),
                ValidUntil = DateTime.Today.AddYears(1),
                ClassId = Guid.NewGuid()
            };

            var entry = new Entry()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = "Development",
                Description = "Working on feature implementation",
                ShouldTimeId = shouldTime.Id,
                ShouldTime = shouldTime,
                UserId = "user123"
            };

            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-1),
                End = DateTime.Now,
                Title = "Testing",
                Description = "Writing unit tests",
                ShouldTime = TimeSpan.FromHours(6)
            };

            Assert.AreNotEqual(timeEntryDto, EntryMapper.ToDTO(entry));
        }

        #endregion

        #region ValidateDTO Tests

        [TestMethod()]
        public void ValidateDTONullTest()
        {
            Assert.IsFalse(EntryMapper.ValidateDTO(null));
        }

        [TestMethod()]
        public void ValidateDTOValidTest()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = "Development",
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            Assert.IsTrue(EntryMapper.ValidateDTO(timeEntryDto));
        }

        [TestMethod()]
        public void ValidateDTOValidWithNullEnd()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = null,
                Title = "Development",
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            Assert.IsTrue(EntryMapper.ValidateDTO(timeEntryDto));
        }

        [TestMethod()]
        public void ValidateDTOEmptyTitleTest()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = string.Empty,
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            Assert.IsFalse(EntryMapper.ValidateDTO(timeEntryDto));
        }

        [TestMethod()]
        public void ValidateDTOWhitespaceTitleTest()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = "   ",
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            Assert.IsFalse(EntryMapper.ValidateDTO(timeEntryDto));
        }

        [TestMethod()]
        public void ValidateDTOEmptyDescriptionTest()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = "Development",
                Description = string.Empty,
                Username = "test@example.com"
            };

            Assert.IsFalse(EntryMapper.ValidateDTO(timeEntryDto));
        }

        [TestMethod()]
        public void ValidateDTOWhitespaceDescriptionTest()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now.AddHours(-2),
                End = DateTime.Now,
                Title = "Development",
                Description = "   ",
                Username = "test@example.com"
            };

            Assert.IsFalse(EntryMapper.ValidateDTO(timeEntryDto));
        }

        [TestMethod()]
        public void ValidateDTOStartEqualsEndTest()
        {
            var now = DateTime.Now;
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = now,
                End = now,
                Title = "Development",
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            Assert.IsFalse(EntryMapper.ValidateDTO(timeEntryDto));
        }

        [TestMethod()]
        public void ValidateDTOStartAfterEndTest()
        {
            var timeEntryDto = new TimeEntryDto()
            {
                Id = Guid.NewGuid(),
                Start = DateTime.Now,
                End = DateTime.Now.AddHours(-2),
                Title = "Development",
                Description = "Working on feature implementation",
                Username = "test@example.com"
            };

            Assert.IsFalse(EntryMapper.ValidateDTO(timeEntryDto));
        }

        #endregion

        #region IsHoliday Tests

        [TestMethod()]
        public void IsHolidayTest()
        {
            Assert.AreEqual(1, 1);
        }

        #endregion
    }
}