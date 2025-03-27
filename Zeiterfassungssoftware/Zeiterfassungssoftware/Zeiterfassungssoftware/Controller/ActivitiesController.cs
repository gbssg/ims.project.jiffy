using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Activities;
using ActivityDescription = Zeiterfassungssoftware.SharedData.Activities.ActivityDescription;
using ActivityTitle = Zeiterfassungssoftware.SharedData.Activities.ActivityTitle;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {
        private ApplicationDbContext _context;

        public ActivitiesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("descriptions")]
        public IActionResult GetAllDescriptions()
        {
            string UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Descriptions = _context.ActivityDescriptions.Where(e => (e.UserId == UserId))
                                                            .Select(e => e.ToActivityDescription());

            return Ok(Descriptions.ToList());            
        }

        
        [HttpPost("descriptions")]
        public async Task<IActionResult> AddDescription([FromBody] ActivityDescription Description)
        {
            if (!IsValid(Description))
                return BadRequest("Invalid data");

            string UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbDescription = new Data.Jiffy.Models.ActivityDescription()
            {
                Id = Guid.NewGuid(),
                Value = Description.Value,
                UserId = UserId,
            };

            _context.ActivityDescriptions.Add(DbDescription);
            _context.SaveChanges();

            return Ok(DbDescription.ToActivityDescription());
        }

        [HttpGet("titles")]
        public IActionResult GetAllTitles()
        {
            string UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Titles = _context.Activitys.Where(e => (e.UserId == UserId))
                                           .Select(e => e.ToActivityTitle());

            return Ok(Titles.ToList());
        }

        //id
        [HttpPost("titles")]
		public async Task<IActionResult> AddTitle([FromBody] ActivityTitle Title)
        {
            if (!IsValid(Title))
                return BadRequest("Invalid data");

            string UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbTitle = new Data.Jiffy.Models.Activity()
            {
                Id = Guid.NewGuid(),
                Title = Title.Value,
                UserId = UserId,
            };

            _context.Activitys.Add(DbTitle);
            _context.SaveChanges();

            return Ok(DbTitle.ToActivityTitle());
        }

        [HttpDelete("titles/{Id}")]
        public async Task<IActionResult> DeleteTitle(Guid Id)
        {
            string UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Activity? Activity = _context.Activitys.FirstOrDefault(e => (e.UserId == UserId) && Id.Equals(e.Id));
            
            if(Activity is null)
                return NotFound();

            
            _context.Activitys.Remove(Activity);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("titles")]
        public async Task<IActionResult> UpdateTitle([FromBody] ActivityTitle Title)
        {
            if (!IsValid(Title))
                return BadRequest("Invalid data");

            string UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Activity? DbTitle = _context.Activitys.FirstOrDefault(e => (e.Id == Title.Id) && (e.UserId == UserId));

            if (DbTitle is null) 
                return NotFound();

            DbTitle.Favorite = Title.Favorite;
            DbTitle.Title = Title.Value;

            _context.SaveChanges();

            return Ok(DbTitle.ToActivityTitle());
        }


        [HttpDelete("descriptions/{Id}")]
        public async Task<IActionResult> DeleteDescription(Guid Id)
        {
            string UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Data.Jiffy.Models.ActivityDescription? Activity = _context.ActivityDescriptions.FirstOrDefault(e => (e.UserId == UserId) && Id.Equals(e.Id));

            if (Activity is null)
                return NotFound();


            _context.Remove(Activity);
            _context.SaveChanges();

            return Ok();
        }

        [HttpPatch("descriptions")]
        public async Task<IActionResult> UpdateDescription([FromBody] ActivityTitle Title)
        {
            if (!IsValid(Title))
                return BadRequest("Invalid data");

            string UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Data.Jiffy.Models.ActivityDescription? DbDescription = _context.ActivityDescriptions.FirstOrDefault(e => (e.Id == Title.Id) && (e.UserId == UserId));

            if (DbDescription is null)
                return NotFound();

            DbDescription.Favorite = Title.Favorite;
            DbDescription.Value = Title.Value;

            _context.SaveChanges();

            return Ok(DbDescription.ToActivityDescription());
        }

        public bool IsValid(object Obj)
        {
            if (Obj is ActivityDescription Description)
                return !string.IsNullOrWhiteSpace(Description.Value);
            
            if (Obj is ActivityTitle Title)
                return !string.IsNullOrWhiteSpace(Title.Value);
            
            return false;
        }
    }
}
