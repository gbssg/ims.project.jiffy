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

        [HttpGet("descriptions/all")]
        public IActionResult GetAllDescriptions()
        {
            var dbUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));
            if (dbUser is null || !User.Identity.IsAuthenticated)
                return Unauthorized();

            var Descriptions = _context.ActivityDescriptions.Where(e => (e.UserId == dbUser.Id));

            return Ok(Descriptions.ToList());            
        }

        
        [HttpPost("descriptions/new")]
        public async Task<IActionResult> AddDescription([FromBody] ActivityDescription Description)
        {
            if (string.IsNullOrWhiteSpace(Description.Value))
                return BadRequest();

            if (!User.Identity.IsAuthenticated)
                return Unauthorized();
            var dbUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

            var Temp = new Data.Jiffy.Models.ActivityDescription()
            {
                Id = Guid.NewGuid(),
                Value = Description.Value,
                UserId = dbUser.Id,
            };

            _context.ActivityDescriptions.Add(Temp);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet("titles/all")]
        public IActionResult GetAllTitles()
        {
            var dbUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));
            if (dbUser is null || !User.Identity.IsAuthenticated)
                return Unauthorized();

            var Titles = _context.ActivityTitles.Where(e => (e.UserId == dbUser.Id))
                                                .Select(t => new ActivityTitle(t.Value));


            return Ok(Titles.ToList());
        }

        //id
        [HttpPost("titles/new")]
		public async Task<IActionResult> AddTitle([FromBody] ActivityTitle Title)
        {
            if (string.IsNullOrWhiteSpace(Title.Value))
                return BadRequest();

            var AspNetUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));
            if (AspNetUser is null || !User.Identity.IsAuthenticated)
                return Unauthorized();

            var Temp = new Data.Jiffy.Models.ActivityTitle()
            {
                Id = Guid.NewGuid(),
                Value = Title.Value,
                UserId = AspNetUser.Id,
            };

            _context.ActivityTitles.Add(Temp);
            _context.SaveChanges();

            return Ok();
        }
    }
}
