

using Zeiterfassungssoftware.SharedData.Activities;

namespace Zeiterfassungssoftware.Client.Pages
{
    public partial class ManageActivities : IDisposable
    {
        
        private string ActivityTitle { get; set; } = string.Empty;
        private string ActivityDescription { get; set; } = string.Empty;
        private ActivityTitle SelectedTitle => ActivitySource.GetActivityTitles().FirstOrDefault(e => string.Equals(e.Id.ToString(), ActivityTitle)) ?? new ActivityTitle();
        private ActivityDescription SelectedDescription => ActivitySource.GetActivityDescriptions().FirstOrDefault(e => string.Equals(e.Id.ToString(), ActivityDescription)) ?? new ActivityDescription();
        private ActivityTitle? EditingTitle { get; set; }
        private ActivityDescription? EditingDescription { get; set; }


        private Timer? Timer { get; set; }


        protected override void OnInitialized()
        {
            Timer = new Timer(UpdateTimer, null, 0, 200);
        }

        public void UpdateTimer(object? state)
        {
            InvokeAsync(StateHasChanged);
        }

        public void Delete(object Obj)
        {
            ActivitySource.Remove(Obj);
        }

        public void Edit(object Obj)
        {

            if ((Obj is ActivityDescription) ? (EditingDescription is null) : (EditingTitle is null))
            {
                StartEditing(Obj);
            }
            else
            {
                StopEditing(Obj);
            }
        }

        public void StartEditing(object Obj)
        {
            if(Obj is ActivityTitle Title)
            {
                EditingTitle = new()
                {
                    Id = Title.Id,
                    Value = Title.Value
                };
            } 
            
            if(Obj is ActivityDescription Description)
            {
                EditingDescription = new()
                {
                    Id = Description.Id,
                    Value = Description.Value
                };
            }
        }

        public void StopEditing(object Obj)
        {
            if (Obj is ActivityTitle Title)
            {
                if(!Title.Id.Equals(EditingTitle?.Id))
                {
                    Title = ActivitySource.GetActivityTitles().FirstOrDefault(e => e.Id.Equals(EditingTitle?.Id));
                    if (Title is null)
                        return;
                }
                Title.Value = EditingTitle?.Value ?? string.Empty;
                EditingTitle = null;
                ActivitySource.Update(Title);
            }

            if (Obj is ActivityDescription Description)
            {
                if (!Description.Id.Equals(EditingDescription?.Id))
                {
                    Description = ActivitySource.GetActivityDescriptions().FirstOrDefault(e => e.Id.Equals(EditingDescription.Id));
                    if (Description is null)
                        return;
                }

                Description.Value = EditingDescription?.Value ?? string.Empty;
                EditingDescription = null;
                ActivitySource.Update(Description);
            }
        }


        public void Favorize(object Obj)
        {
            if(Obj is ActivityDescription Description)
            {
                Description.Favorite = !Description.Favorite;
                ActivitySource.Update(Description);
                return;
            }

            if (Obj is ActivityTitle Title)
            {
                Title.Favorite = !Title.Favorite;
                ActivitySource.Update(Title);
                return;
            }
        }

        void IDisposable.Dispose()
        {
            Timer?.Dispose();
        }
    }
}