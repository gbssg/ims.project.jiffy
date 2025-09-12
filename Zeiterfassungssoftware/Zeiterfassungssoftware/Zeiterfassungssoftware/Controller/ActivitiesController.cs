using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Mapper;
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

        #region Descriptions

        [HttpGet("descriptions")]
        public async Task<IActionResult> GetAllDescriptions()
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Descriptions = await _context.ActivityDescriptions
                .Where(e => e.UserId == UserId)
                .Select(e => ActivityMapper.ToDescriptionDTO(e))
                .ToListAsync();

            return Ok(Descriptions);            
        }

        
        [HttpPost("descriptions")]
        public async Task<IActionResult> AddDescription([FromBody] ActivityDescription Description)
        {
            if (!ActivityMapper.ValidateDescriptionDTO(Description))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbDescription = new Data.Jiffy.Models.ActivityDescription()
            {
                Id = Guid.NewGuid(),
                Value = Description.Value,
                UserId = UserId,
                Favorite = Description.Favorite
            };

            _context.ActivityDescriptions.Add(DbDescription);
            await _context.SaveChangesAsync();

            return Ok(ActivityMapper.ToDescriptionDTO(DbDescription));
        }

        [HttpDelete("descriptions/{Id}")]
        public async Task<IActionResult> DeleteDescription(Guid Id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbDescription = await _context.ActivityDescriptions
                .FirstOrDefaultAsync(e => e.UserId == UserId && e.Id == Id);

            if (DbDescription is null)
                return NotFound();

            _context.ActivityDescriptions.Remove(DbDescription);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("descriptions/{Id}")]
        public async Task<IActionResult> UpdateDescription(Guid Id, [FromBody] ActivityDescription Description)
        {
            if (!ActivityMapper.ValidateDescriptionDTO(Description))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbDescription = await _context.ActivityDescriptions
                .FirstOrDefaultAsync(e => e.Id == Id && e.UserId == UserId);

            if (DbDescription is null)
                return NotFound();

            DbDescription.Favorite = Description.Favorite;
            DbDescription.Value = Description.Value;

            await _context.SaveChangesAsync();

            return Ok(ActivityMapper.ToDescriptionDTO(DbDescription));
        }

        #endregion

        #region Titles

        [HttpGet("titles")]
        public async Task<IActionResult> GetAllTitles()
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Titles = await _context.Activitys
                .Where(e => e.UserId == UserId)
                .Select(e => ActivityMapper.ToTitleDTO(e))
                .ToListAsync();

            return Ok(Titles);
        }

        [HttpPost("titles")]
		public async Task<IActionResult> AddTitle([FromBody] ActivityTitle Title)
        {
            if (!ActivityMapper.ValidateTitleDTO(Title))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbTitle = new Data.Jiffy.Models.ActivityTitle()
            {
                Id = Guid.NewGuid(),
                Title = Title.Value,
                UserId = UserId,
                Favorite = Title.Favorite
            };

            _context.Activitys.Add(DbTitle);
            await _context.SaveChangesAsync();

            return Ok(ActivityMapper.ToTitleDTO(DbTitle));
        }

        [HttpDelete("titles/{Id}")]
        public async Task<IActionResult> DeleteTitle(Guid Id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbTitle = await _context.Activitys
                .FirstOrDefaultAsync(e => e.UserId == UserId && e.Id == Id);

            if (DbTitle is null)
                return NotFound();

            _context.Activitys.Remove(DbTitle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("titles/{Id}")]
        public async Task<IActionResult> UpdateTitle(Guid Id, [FromBody] ActivityTitle Title)
        {
            if (!ActivityMapper.ValidateTitleDTO(Title))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbTitle = await _context.Activitys
                .FirstOrDefaultAsync(e => e.Id == Id && e.UserId == UserId);

            if (DbTitle == null)
                return NotFound();

            DbTitle.Favorite = Title.Favorite;
            DbTitle.Title = Title.Value;

            await _context.SaveChangesAsync();

            return Ok(ActivityMapper.ToTitleDTO(DbTitle));
        }

        #endregion
    }
}
