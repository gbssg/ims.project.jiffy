using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeiterfassungssoftware.Client.Pages;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.ShouldTimes;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Mapper.Tests
{
    [TestClass()]
    public class ShouldTimeMapperTests
    {
        [TestMethod()]
        public void ToDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ShouldTimeMapper.ToDTO(null));
        }

        [TestMethod()]
        public void FromDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ShouldTimeMapper.FromDTO(null));
        }

        [TestMethod()]
        public void ToDTOValid()
        {
            var Id = Guid.NewGuid();
            var ClassId = Guid.NewGuid();

            var ShouldTime = new ShouldTime()
            {
                Id = Id,
                ClassId = ClassId,
                DayOfWeek = DayOfWeek.Wednesday,
                Should = new TimeSpan(2, 0, 0),
                ValidUntil = DateTime.Now
            };

            var ShouldTimeDto = new ShouldTimeDto()
            {
                Id = Id,
                ClassId = ClassId,
                DayOfWeek = DayOfWeek.Wednesday,
                Should = new TimeSpan(2, 0, 0)
            };

            Assert.AreEqual(ShouldTimeDto, ShouldTimeMapper.ToDTO(ShouldTime));
        }

        [TestMethod()]
        public void FromDTOValid()
        {
            var Id = Guid.NewGuid();
            var ClassId = Guid.NewGuid();

            var ShouldTime = new ShouldTime()
            {
                Id = Id,
                ClassId = ClassId,
                DayOfWeek = DayOfWeek.Wednesday,
                Should = new TimeSpan(2, 0, 0),
                ValidUntil = DateTime.Now
            };

            var ShouldTimeDto = new ShouldTimeDto()
            {
                Id = Id,
                ClassId = ClassId,
                DayOfWeek = DayOfWeek.Wednesday,
                Should = new TimeSpan(2, 0, 0)
            };

            Assert.AreEqual(ShouldTime, ShouldTimeMapper.FromDTO(ShouldTimeDto));
        }

        [TestMethod()]
        public void FromDTONotEqual()
        {
            var ShouldTime = new ShouldTime()
            {
                Id = Guid.NewGuid(),
                ClassId = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Wednesday,
                Should = new TimeSpan(2, 0, 0),
                ValidUntil = DateTime.Now
            };

            var ShouldTimeDto = new ShouldTimeDto()
            {
                Id = Guid.NewGuid(),
                ClassId = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Wednesday,
                Should = new TimeSpan(2, 0, 0)
            };

            Assert.AreNotEqual(ShouldTime, ShouldTimeMapper.FromDTO(ShouldTimeDto));
        }

        [TestMethod()]
        public void ToDTONotEqual()
        {
            var ShouldTime = new ShouldTime()
            {
                Id = Guid.NewGuid(),
                ClassId = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Wednesday,
                Should = new TimeSpan(2, 0, 0),
                ValidUntil = DateTime.Now
            };

            var ShouldTimeDto = new ShouldTimeDto()
            {
                Id = Guid.NewGuid(),
                ClassId = Guid.NewGuid(),
                DayOfWeek = DayOfWeek.Wednesday,
                Should = new TimeSpan(2, 0, 0)
            };

            Assert.AreNotEqual(ShouldTimeDto, ShouldTimeMapper.ToDTO(ShouldTime));
        }
    }
}