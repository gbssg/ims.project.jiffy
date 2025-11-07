using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Activities;

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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActivityDescriptionDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ActivityDescriptionDto>>> GetAllDescriptions()
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActivityDescriptionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ActivityDescriptionDto>> AddDescription([FromBody, Required] ActivityDescriptionDto descriptionDto)
        {
            if (!ActivityMapper.ValidateDescriptionDTO(descriptionDto))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Description = ActivityMapper.FromDescriptionDTO(descriptionDto);
            Description.Id = Guid.NewGuid();
            Description.UserId = UserId;

            _context.ActivityDescriptions.Add(Description);
            await _context.SaveChangesAsync();

            return Ok(ActivityMapper.ToDescriptionDTO(Description));
        }

        [HttpDelete("descriptions/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteDescription(Guid id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Description = await _context.ActivityDescriptions
                .FirstOrDefaultAsync(e => e.UserId == UserId && e.Id == id);

            if (Description is null)
                return NotFound();

            _context.ActivityDescriptions.Remove(Description);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("descriptions/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActivityDescriptionDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActivityDescriptionDto>> UpdateDescription(Guid id, [FromBody, Required] ActivityDescriptionDto descriptionDto)
        {
            if (!ActivityMapper.ValidateDescriptionDTO(descriptionDto))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Description = await _context.ActivityDescriptions
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == UserId);

            if (Description is null)
                return NotFound();

            Description.Favorite = descriptionDto.Favorite;
            Description.Value = descriptionDto.Value;

            await _context.SaveChangesAsync();

            return Ok(ActivityMapper.ToDescriptionDTO(Description));
        }

        #endregion

        #region Titles

        [HttpGet("titles")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ActivityTitleDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<ActivityTitleDto>>> GetAllTitles()
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActivityTitleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<ActivityTitleDto>> AddTitle([FromBody, Required] ActivityTitleDto titleDto)
        {
            if (!ActivityMapper.ValidateTitleDTO(titleDto))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Title = ActivityMapper.FromTitleDTO(titleDto);
            Title.Id = Guid.NewGuid();
            Title.UserId = UserId;

            _context.Activitys.Add(Title);
            await _context.SaveChangesAsync();

            return Ok(ActivityMapper.ToTitleDTO(Title));
        }

        [HttpDelete("titles/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteTitle(Guid id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Title = await _context.Activitys
                .FirstOrDefaultAsync(e => e.UserId == UserId && e.Id == id);

            if (Title is null)
                return NotFound();

            _context.Activitys.Remove(Title);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("titles/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ActivityTitleDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ActivityTitleDto>> UpdateTitle(Guid id, [FromBody, Required] ActivityTitleDto titleDto)
        {
            if (!ActivityMapper.ValidateTitleDTO(titleDto))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Title = await _context.Activitys
                .FirstOrDefaultAsync(e => e.Id == id && e.UserId == UserId);

            if (Title == null)
                return NotFound();

            Title.Favorite = titleDto.Favorite;
            Title.Title = titleDto.Value;

            await _context.SaveChangesAsync();

            return Ok(ActivityMapper.ToTitleDTO(Title));
        }

        #endregion
    }
}
