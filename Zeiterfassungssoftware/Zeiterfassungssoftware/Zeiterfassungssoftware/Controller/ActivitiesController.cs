using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
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
