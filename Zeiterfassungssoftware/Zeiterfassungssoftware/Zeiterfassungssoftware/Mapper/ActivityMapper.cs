using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Mapper
{
    public class ActivityMapper
    {
        public static ActivityTitle FromTitleDTO(ActivityTitleDto activityTitleDto)
        {
            if (activityTitleDto is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = activityTitleDto.Id,
                Favorite = activityTitleDto.Favorite,
                Title = activityTitleDto.Value,
            };
        }

        public static ActivityDescription FromDescriptionDTO(ActivityDescriptionDto activityDescriptionDto)
        {
            if (activityDescriptionDto is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = activityDescriptionDto.Id,
                Favorite = activityDescriptionDto.Favorite,
                Value = activityDescriptionDto.Value,
            };
        }

        public static ActivityTitleDto ToTitleDTO(ActivityTitle activityTitle)
        {
            if (activityTitle is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = activityTitle.Id,
                Favorite = activityTitle.Favorite,
                Value = activityTitle.Title,
            };
        }

        public static ActivityDescriptionDto ToDescriptionDTO(ActivityDescription activityDescription)
        {
            if (activityDescription is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = activityDescription.Id,
                Favorite = activityDescription.Favorite,
                Value = activityDescription.Value,
            };
        }

        public static bool ValidateTitleDTO(ActivityTitleDto activityTitleDto)
        {
            if (activityTitleDto is null)
                return false;

            return !string.IsNullOrWhiteSpace(activityTitleDto.Value);
        }

        public static bool ValidateDescriptionDTO(ActivityDescription activityDescriptionDto)
        {
            if (activityDescriptionDto is null)
                return false;

            return !string.IsNullOrWhiteSpace(activityDescriptionDto.Value);
        }
    }
}
