using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Activities;
using ActivityDescription = Zeiterfassungssoftware.SharedData.Activities.ActivityDescription;

namespace Zeiterfassungssoftware.Mapper
{
    public class ActivityMapper
    {
        public static Activity FromTitleDTO(ActivityTitle ActivityTitle)
        {
            if (ActivityTitle is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = ActivityTitle.Id,
                Favorite = ActivityTitle.Favorite,
                Title = ActivityTitle.Value,
            };
        }

        public static Data.Jiffy.Models.ActivityDescription FromDescriptionDTO(ActivityDescription ActivityDescription)
        {
            if (ActivityDescription is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = ActivityDescription.Id,
                Favorite = ActivityDescription.Favorite,
                Value = ActivityDescription.Value,
            };
        }

        public static ActivityTitle ToTitleDTO(Activity ActivityTitle)
        {
            if (ActivityTitle is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = ActivityTitle.Id,
                Favorite = ActivityTitle.Favorite,
                Value = ActivityTitle.Title,
            };
        }

        public static ActivityDescription ToDescriptionDTO(Data.Jiffy.Models.ActivityDescription ActivityDescription)
        {
            if (ActivityDescription is null)
                throw new ArgumentNullException();

            return new()
            {
                Id = ActivityDescription.Id,
                Favorite = ActivityDescription.Favorite,
                Value = ActivityDescription.Value,
            };
        }

        public static bool ValidateTitleDTO(ActivityTitle ActivityTitle)
        {
            if (ActivityTitle is null)
                return false;

            return !string.IsNullOrWhiteSpace(ActivityTitle.Value);
        }

        public static bool ValidateDescriptionDTO(ActivityDescription ActivityDescription)
        {
            if (ActivityDescription is null)
                return false;

            return !string.IsNullOrWhiteSpace(ActivityDescription.Value);
        }
    }
}
