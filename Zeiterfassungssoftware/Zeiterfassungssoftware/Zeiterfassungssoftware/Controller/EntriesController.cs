using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<IActionResult> GetEntries([FromQuery] int start, [FromQuery] int limit)
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

        [HttpGet("{id}")]
		public IActionResult GetEntryById(Guid id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? Entry = _context.Entries.Include(e => e.ShouldTime).FirstOrDefault(e => e.Id == id && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));
                
            if (Entry is null)
                return NotFound();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            TimeEntryDto EntryDto = EntryMapper.ToDTO(Entry);
            EntryDto.Username = Username;

            return Ok(EntryDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddEntry([FromBody] TimeEntryDto entryDto)
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

            var ShouldTime = await _context.ShouldTimes.FirstOrDefaultAsync(e => e.DayOfWeek == DateTime.Now.DayOfWeek && e.ValidUntil > DateTime.Now);
            if ((ShouldTime is not null) && !IsHoliday(DateTime.Now))
                Entry.ShouldTimeId = ShouldTime.Id;
            else
                Entry.ShouldTimeId = Guid.Empty;

            _context.Entries.Add(Entry);
            _context.SaveChanges();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            TimeEntryDto EntryDto = EntryMapper.ToDTO(Entry);
            EntryDto.Username = Username;

            return Ok(EntryDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(Guid id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? Entry = _context.Entries.FirstOrDefault(e => (e.Id == id) && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));

            if (Entry is null)
                return NotFound();

            _context.Entries.Remove(Entry);
            _context.SaveChanges();

            return Ok();
        }

		[HttpPut("{id}")]
        public IActionResult PatchEntry(Guid id, [FromBody] TimeEntryDto entryDto)
        {
            if (!EntryMapper.ValidateDTO(entryDto))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? Entry = _context.Entries.FirstOrDefault(e => (e.Id == id) && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));

            if (Entry is null)
                return NotFound();

            Entry.Start = entryDto.Start;
            Entry.End = entryDto.End;
            Entry.Title = entryDto.Title;
            Entry.Description = entryDto.Description;

            _context.SaveChanges();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            return Ok(EntryMapper.ToDTO(Entry));
        }

        public bool IsHoliday(DateTime Date)
        {
            return false;
        }

    }
}
