using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Services
{

    [Route("api/v1/[controller]")]
	[ApiController]
    [Authorize]
    public class EntriesController : ControllerBase
	{
        private ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public EntriesController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// If the current user is an administrator, a list of all entries and their owners will be returned. However, if the user is not an administrator, only a list of their own entries will be returned.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TimeEntryDto>))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<List<TimeEntryDto>>> GetEntries([FromQuery] int start, [FromQuery] int limit)
        {
            if (start < 0) 
                start = 0;

            if (limit <= 0 || limit > 1000) 
                limit = 50;

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            if (User.IsInRole("Administrator"))
            {
                var Entries = await _context.Entries
                    .OrderByDescending(e => e.Start)
                    .Skip(start)
                    .Take(limit)
                    .Include(e => e.ShouldTime)
                    .ToListAsync();

                var UserIds = Entries.Select(e => e.UserId).Distinct().ToList();

                var Users = await _context.Users
                    .Where(e => UserIds.Contains(e.Id))
                    .ToDictionaryAsync(e => e.Id, e => e.Email?.Split('@')[0] ?? "Unknown");

                var EntriesWithUsernames = Entries.Select(e =>
                {
                    var Entry = EntryMapper.ToDTO(e);
                    Entry.Username = Users.TryGetValue(e.UserId, out string? Username) ? Username : "Unknown";
                    return Entry;
                }).ToList();

                return Ok(EntriesWithUsernames);
            }
            else
            {
                var Entries = await _context.Entries
                    .Where(e => e.UserId == UserId)
                    .OrderByDescending(e => e.Start)
                    .Skip(start)
                    .Take(limit)
                    .Select(e => EntryMapper.ToDTO(e))
                    .ToListAsync();

                return Ok(Entries);
            }
        }

        /// <summary>
        /// Gets a specific entry (limited to own entries for non-administrators).
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeEntryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TimeEntryDto>> GetEntryById(Guid id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? Entry = await _context.Entries.Include(e => e.ShouldTime).FirstOrDefaultAsync(e => e.Id == id && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));
                
            if (Entry is null)
                return NotFound();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            TimeEntryDto EntryDto = EntryMapper.ToDTO(Entry);
            EntryDto.Username = Username;

            return Ok(EntryDto);
        }

        /// <summary>
        /// Creates a new entry that is linked to the current user.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeEntryDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TimeEntryDto>> AddEntry([FromBody, Required] TimeEntryDto entryDto)
        {
            if (!EntryMapper.ValidateDTO(entryDto))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbUser = await _userManager.GetUserAsync(User);
            if (DbUser is null)
                return Unauthorized();

            Entry Entry = EntryMapper.FromDTO(entryDto);
            Entry.Id = Guid.NewGuid();
            Entry.UserId = UserId;


            var ShouldTime = await _context.ShouldTimes.FirstOrDefaultAsync(e => e.DayOfWeek == DateTime.Now.DayOfWeek && e.ValidUntil > DateTime.Now && e.ClassId == DbUser.ClassId);
            if ((ShouldTime is not null) && !EntryMapper.IsHoliday(DateTime.Now))
                Entry.ShouldTimeId = ShouldTime.Id;
            else
                Entry.ShouldTimeId = Guid.Empty;

            _context.Entries.Add(Entry);
            await _context.SaveChangesAsync();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            TimeEntryDto EntryDto = EntryMapper.ToDTO(Entry);
            EntryDto.Username = Username;

            return Ok(EntryDto);
        }

        /// <summary>
        /// Deletes an entry (limited to own entries for non-administrators).
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteEntry(Guid id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? Entry = await _context.Entries.FirstOrDefaultAsync(e => (e.Id == id) && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));

            if (Entry is null)
                return NotFound();

            _context.Entries.Remove(Entry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Updates an entry (limited to own entry for non-administrators).
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeEntryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<TimeEntryDto>> PatchEntry(Guid id, [FromBody, Required] TimeEntryDto entryDto)
        {
            if (!EntryMapper.ValidateDTO(entryDto))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? Entry = await _context.Entries.FirstOrDefaultAsync(e => (e.Id == id) && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));

            if (Entry is null)
                return NotFound();

            Entry.Start = entryDto.Start;
            Entry.End = entryDto.End;
            Entry.Title = entryDto.Title;
            Entry.Description = entryDto.Description;

            await _context.SaveChangesAsync();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            return Ok(EntryMapper.ToDTO(Entry));
        }

    }
}
