using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Classes;
using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.Mapper.Tests
{
    [TestClass()]
    public class ClassMapperTests
    {
        #region FromDTO Tests

        [TestMethod()]
        public void FromDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ClassMapper.FromDTO(null));
        }

        [TestMethod()]
        public void FromDTOValid()
        {
            var shouldTimeDto = new ShouldTimeDto()
            {
                Id = Guid.NewGuid(),
                ClassId = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Monday,
                Should = TimeSpan.FromHours(8)
            };

            var classDto = new ClassDto()
            {
                Id = Guid.NewGuid(),
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTimeDto> { shouldTimeDto }
            };

            var shouldTime = new ShouldTime()
            {
                Id = shouldTimeDto.Id,
                ClassId = shouldTimeDto.ClassId,
                DayOfWeek = shouldTimeDto.DayOfWeek,
                Should = shouldTimeDto.Should
            };

            var classEntity = new Class()
            {
                Id = classDto.Id,
                Name = classDto.Name,
                ShouldTimes = new List<ShouldTime> { shouldTime }
            };

            Assert.AreEqual(classEntity, ClassMapper.FromDTO(classDto));
        }

        [TestMethod()]
        public void FromDTOValidWithEmptyCollection()
        {
            var classDto = new ClassDto()
            {
                Id = Guid.NewGuid(),
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTimeDto>()
            };

            var classEntity = new Class()
            {
                Id = classDto.Id,
                Name = classDto.Name,
                ShouldTimes = new List<ShouldTime>()
            };

            var result = ClassMapper.FromDTO(classDto);
            Assert.AreEqual(classEntity.Id, result.Id);
            Assert.AreEqual(classEntity.Name, result.Name);
            Assert.AreEqual(0, result.ShouldTimes.Count);
        }

        [TestMethod()]
        public void FromDTOValidWithMultipleShouldTimes()
        {
            var classId = Guid.NewGuid();
            var shouldTimeDto1 = new ShouldTimeDto()
            {
                Id = Guid.NewGuid(),
                ClassId = classId,
                DayOfWeek = DayOfWeek.Monday,
                Should = TimeSpan.FromHours(8)
            };

            var shouldTimeDto2 = new ShouldTimeDto()
            {
                Id = Guid.NewGuid(),
                ClassId = classId,
                DayOfWeek = DayOfWeek.Tuesday,
                Should = TimeSpan.FromHours(6)
            };

            var classDto = new ClassDto()
            {
                Id = classId,
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTimeDto> { shouldTimeDto1, shouldTimeDto2 }
            };

            var result = ClassMapper.FromDTO(classDto);
            Assert.AreEqual(classDto.Id, result.Id);
            Assert.AreEqual(classDto.Name, result.Name);
            Assert.AreEqual(2, result.ShouldTimes.Count);
        }

        [TestMethod()]
        public void FromDTONotEqual()
        {
            var classDto = new ClassDto()
            {
                Id = Guid.NewGuid(),
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTimeDto>()
            };

            var classEntity = new Class()
            {
                Id = Guid.NewGuid(),
                Name = "Mathematics 201",
                ShouldTimes = new List<ShouldTime>()
            };

            Assert.AreNotEqual(classEntity, ClassMapper.FromDTO(classDto));
        }

        #endregion

        #region ToDTO Tests

        [TestMethod()]
        public void ToDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ClassMapper.ToDTO(null));
        }

        [TestMethod()]
        public void ToDTOValid()
        {
            var classId = Guid.NewGuid();
            var shouldTime = new ShouldTime()
            {
                Id = Guid.NewGuid(),
                ClassId = classId,
                DayOfWeek = DayOfWeek.Monday,
                Should = TimeSpan.FromHours(8),
                ValidUntil = DateTime.Today.AddYears(1)
            };

            var classEntity = new Class()
            {
                Id = classId,
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTime> { shouldTime }
            };

            var shouldTimeDto = new ShouldTimeDto()
            {
                Id = shouldTime.Id,
                ClassId = shouldTime.ClassId,
                DayOfWeek = shouldTime.DayOfWeek,
                Should = shouldTime.Should
            };

            var classDto = new ClassDto()
            {
                Id = classEntity.Id,
                Name = classEntity.Name,
                ShouldTimes = new List<ShouldTimeDto> { shouldTimeDto }
            };

            Assert.AreEqual(classDto, ClassMapper.ToDTO(classEntity));
        }

        [TestMethod()]
        public void ToDTOValidWithEmptyCollection()
        {
            var classEntity = new Class()
            {
                Id = Guid.NewGuid(),
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTime>()
            };

            var classDto = new ClassDto()
            {
                Id = classEntity.Id,
                Name = classEntity.Name,
                ShouldTimes = new List<ShouldTimeDto>()
            };

            var result = ClassMapper.ToDTO(classEntity);
            Assert.AreEqual(classDto.Id, result.Id);
            Assert.AreEqual(classDto.Name, result.Name);
            Assert.AreEqual(0, result.ShouldTimes.Count);
        }

        [TestMethod()]
        public void ToDTOValidWithMultipleShouldTimes()
        {
            var classId = Guid.NewGuid();
            var shouldTime1 = new ShouldTime()
            {
                Id = Guid.NewGuid(),
                ClassId = classId,
                DayOfWeek = DayOfWeek.Monday,
                Should = TimeSpan.FromHours(8),
                ValidUntil = DateTime.Today.AddYears(1)
            };

            var shouldTime2 = new ShouldTime()
            {
                Id = Guid.NewGuid(),
                ClassId = classId,
                DayOfWeek = DayOfWeek.Tuesday,
                Should = TimeSpan.FromHours(6),
                ValidUntil = DateTime.Today.AddYears(1)
            };

            var classEntity = new Class()
            {
                Id = classId,
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTime> { shouldTime1, shouldTime2 }
            };

            var result = ClassMapper.ToDTO(classEntity);
            Assert.AreEqual(classEntity.Id, result.Id);
            Assert.AreEqual(classEntity.Name, result.Name);
            Assert.AreEqual(2, result.ShouldTimes.Count);
        }

        [TestMethod()]
        public void ToDTONotEqual()
        {
            var classEntity = new Class()
            {
                Id = Guid.NewGuid(),
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTime>()
            };

            var classDto = new ClassDto()
            {
                Id = Guid.NewGuid(),
                Name = "Mathematics 201",
                ShouldTimes = new List<ShouldTimeDto>()
            };

            Assert.AreNotEqual(classDto, ClassMapper.ToDTO(classEntity));
        }

        #endregion

        #region ValidateDTO Tests

        [TestMethod()]
        public void ValidateDTONullTest()
        {
            Assert.IsFalse(ClassMapper.ValidateDTO(null));
        }

        [TestMethod()]
        public void ValidateDTOValidTest()
        {
            var classDto = new ClassDto()
            {
                Id = Guid.NewGuid(),
                Name = "Computer Science 101",
                ShouldTimes = new List<ShouldTimeDto>()
            };

            Assert.IsTrue(ClassMapper.ValidateDTO(classDto));
        }

        [TestMethod()]
        public void ValidateDTOEmptyNameTest()
        {
            var classDto = new ClassDto()
            {
                Id = Guid.NewGuid(),
                Name = string.Empty,
                ShouldTimes = new List<ShouldTimeDto>()
            };

            Assert.IsFalse(ClassMapper.ValidateDTO(classDto));
        }

        [TestMethod()]
        public void ValidateDTONullNameTest()
        {
            var classDto = new ClassDto()
            {
                Id = Guid.NewGuid(),
                Name = null,
                ShouldTimes = new List<ShouldTimeDto>()
            };

            Assert.IsFalse(ClassMapper.ValidateDTO(classDto));
        }

        [TestMethod()]
        public void ValidateDTOWhitespaceNameTest()
        {
            var classDto = new ClassDto()
            {
                Id = Guid.NewGuid(),
                Name = "   ",
                ShouldTimes = new List<ShouldTimeDto>()
            };

            Assert.IsFalse(ClassMapper.ValidateDTO(classDto));
        }

        #endregion
    }
}