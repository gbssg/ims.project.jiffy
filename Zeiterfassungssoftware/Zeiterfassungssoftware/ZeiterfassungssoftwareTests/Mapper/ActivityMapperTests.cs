using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Mapper.Tests
{
[TestClass()]
public class ActivityMapperTests
{
        #region FromTitleDTO Tests

        [TestMethod()]
        public void FromTitleDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ActivityMapper.FromTitleDTO(null));
        }

        [TestMethod()]
        public void FromTitleDTOValid()
        {
            var activityTitleDto = new ActivityTitleDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "Development"
            };

            var activityTitle = new ActivityTitle()
            {
                Id = activityTitleDto.Id,
                Favorite = activityTitleDto.Favorite,
                Title = activityTitleDto.Value
            };

            Assert.AreEqual(activityTitle, ActivityMapper.FromTitleDTO(activityTitleDto));
        }

        [TestMethod()]
        public void FromTitleDTONotEqual()
        {
            var activityTitleDto = new ActivityTitleDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "Development"
            };

            var activityTitle = new ActivityTitle()
            {
                Id = Guid.NewGuid(),
                Favorite = false,
                Title = "Testing"
            };

            Assert.AreNotEqual(activityTitle, ActivityMapper.FromTitleDTO(activityTitleDto));
        }

        #endregion

        #region FromDescriptionDTO Tests

        [TestMethod()]
        public void FromDescriptionDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ActivityMapper.FromDescriptionDTO(null));
        }

        [TestMethod()]
        public void FromDescriptionDTOValid()
        {
            var activityDescriptionDto = new ActivityDescriptionDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "Working on feature implementation"
            };

            var activityDescription = new ActivityDescription()
            {
                Id = activityDescriptionDto.Id,
                Favorite = activityDescriptionDto.Favorite,
                Value = activityDescriptionDto.Value
            };

            Assert.AreEqual(activityDescription, ActivityMapper.FromDescriptionDTO(activityDescriptionDto));
        }

        [TestMethod()]
        public void FromDescriptionDTONotEqual()
        {
            var activityDescriptionDto = new ActivityDescriptionDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "Working on feature implementation"
            };

            var activityDescription = new ActivityDescription()
            {
                Id = Guid.NewGuid(),
                Favorite = false,
                Value = "Different description"
            };

            Assert.AreNotEqual(activityDescription, ActivityMapper.FromDescriptionDTO(activityDescriptionDto));
        }

        #endregion

        #region ToTitleDTO Tests

        [TestMethod()]
        public void ToTitleDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ActivityMapper.ToTitleDTO(null));
        }

        [TestMethod()]
        public void ToTitleDTOValid()
        {
            var activityTitle = new ActivityTitle()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Title = "Development"
            };

            var activityTitleDto = new ActivityTitleDto()
            {
                Id = activityTitle.Id,
                Favorite = activityTitle.Favorite,
                Value = activityTitle.Title
            };

            Assert.AreEqual(activityTitleDto, ActivityMapper.ToTitleDTO(activityTitle));
        }

        [TestMethod()]
        public void ToTitleDTONotEqual()
        {
            var activityTitle = new ActivityTitle()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Title = "Development"
            };

            var activityTitleDto = new ActivityTitleDto()
            {
                Id = Guid.NewGuid(),
                Favorite = false,
                Value = "Testing"
            };

            Assert.AreNotEqual(activityTitleDto, ActivityMapper.ToTitleDTO(activityTitle));
        }

        #endregion

        #region ToDescriptionDTO Tests

        [TestMethod()]
        public void ToDescriptionDTONullArgumentTest()
        {
            Assert.ThrowsException<ArgumentNullException>(() => ActivityMapper.ToDescriptionDTO(null));
        }

        [TestMethod()]
        public void ToDescriptionDTOValid()
        {
            var activityDescription = new ActivityDescription()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "Working on feature implementation"
            };

            var activityDescriptionDto = new ActivityDescriptionDto()
            {
                Id = activityDescription.Id,
                Favorite = activityDescription.Favorite,
                Value = activityDescription.Value
            };

            Assert.AreEqual(activityDescriptionDto, ActivityMapper.ToDescriptionDTO(activityDescription));
        }

        [TestMethod()]
        public void ToDescriptionDTONotEqual()
        {
            var activityDescription = new ActivityDescription()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "Working on feature implementation"
            };

            var activityDescriptionDto = new ActivityDescriptionDto()
            {
                Id = Guid.NewGuid(),
                Favorite = false,
                Value = "Different description"
            };

            Assert.AreNotEqual(activityDescriptionDto, ActivityMapper.ToDescriptionDTO(activityDescription));
        }

        #endregion

        #region ValidateTitleDTO Tests

        [TestMethod()]
        public void ValidateTitleDTONullTest()
        {
            Assert.IsFalse(ActivityMapper.ValidateTitleDTO(null));
        }

        [TestMethod()]
        public void ValidateTitleDTOValidTest()
        {
            var activityTitleDto = new ActivityTitleDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "Development"
            };

            Assert.IsTrue(ActivityMapper.ValidateTitleDTO(activityTitleDto));
        }

        [TestMethod()]
        public void ValidateTitleDTOEmptyValueTest()
        {
            var activityTitleDto = new ActivityTitleDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = string.Empty
            };

            Assert.IsFalse(ActivityMapper.ValidateTitleDTO(activityTitleDto));
        }

        [TestMethod()]
        public void ValidateTitleDTOWhitespaceValueTest()
        {
            var activityTitleDto = new ActivityTitleDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "   "
            };

            Assert.IsFalse(ActivityMapper.ValidateTitleDTO(activityTitleDto));
        }

        #endregion

        #region ValidateDescriptionDTO Tests

        [TestMethod()]
        public void ValidateDescriptionDTONullTest()
        {
            Assert.IsFalse(ActivityMapper.ValidateDescriptionDTO(null));
        }

        [TestMethod()]
        public void ValidateDescriptionDTOValidTest()
        {
            var activityDescriptionDto = new ActivityDescriptionDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "Working on feature implementation"
            };

            Assert.IsTrue(ActivityMapper.ValidateDescriptionDTO(activityDescriptionDto));
        }

        [TestMethod()]
        public void ValidateDescriptionDTOEmptyValueTest()
        {
            var activityDescriptionDto = new ActivityDescriptionDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = string.Empty
            };

            Assert.IsFalse(ActivityMapper.ValidateDescriptionDTO(activityDescriptionDto));
        }

        [TestMethod()]
        public void ValidateDescriptionDTOWhitespaceValueTest()
        {
            var activityDescriptionDto = new ActivityDescriptionDto()
            {
                Id = Guid.NewGuid(),
                Favorite = true,
                Value = "   "
            };

            Assert.IsFalse(ActivityMapper.ValidateDescriptionDTO(activityDescriptionDto));
        }

        #endregion
    }
}
