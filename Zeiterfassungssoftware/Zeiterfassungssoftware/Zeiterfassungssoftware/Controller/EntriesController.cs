using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Security.Claims;
using Zeiterfassungssoftware.Client.Data.Filter;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Time;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> GetEntries([FromQuery] int Start, [FromQuery] int Limit)
        {
            if (Start < 0) 
                Start = 0;

            if (Limit <= 0 || Limit > 1000) 
                Limit = 50;

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            if (User.IsInRole("Administrator"))
            {
                var Entries = await _context.Entries
                    .OrderByDescending(e => e.Start)
                    .Skip(Start)
                    .Take(Limit)
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
                    .Skip(Start)
                    .Take(Limit)
                    .Select(e => EntryMapper.ToDTO(e))
                    .ToListAsync();

                return Ok(Entries);
            }
        }

        [HttpGet("{Id}")]
		public IActionResult GetEntryById(Guid Id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? DbEntry = _context.Entries.FirstOrDefault(e => e.Id == Id && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));
                
            if (DbEntry is null)
                return NotFound();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            TimeEntry Entry = EntryMapper.ToDTO(DbEntry);
            Entry.Username = Username;

            return Ok(Entry);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] TimeEntry Entry)
        {
            if (!EntryMapper.ValidateDTO(Entry))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var DbUser = await _userManager.GetUserAsync(User);
            var ShouldTimes = _context.ShouldTimes.Where(e => e.ClassId == DbUser.ClassId);
            var Day = DateTime.Now.DayOfWeek;
            var ShouldTime = ShouldTimes.FirstOrDefault(e => e.DayOfWeek == Day);
            var Time = ShouldTime is null ? TimeSpan.Zero : ShouldTime.Should;


            Entry DbEntry = new()
            {
                Id = Guid.NewGuid(),
                Start = Entry.Start,
                End = Entry.End,
                Title = Entry.Title,
                Description = Entry.Description,
                UserId = UserId,
                ShouldTime = Time,
            };

            _context.Entries.Add(DbEntry);
            _context.SaveChanges();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            TimeEntry EntryDto = EntryMapper.ToDTO(DbEntry);
            Entry.Username = Username;

            return Ok(EntryDto);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEntry(Guid Id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? Entry = _context.Entries.FirstOrDefault(e => (e.Id == Id) && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));

            if (Entry is null)
                return NotFound();

            _context.Entries.Remove(Entry);
            _context.SaveChanges();

            return Ok();
        }

		[HttpPatch]
        public IActionResult PatchEntry([FromBody] TimeEntry Entry)
        {
            if (!EntryMapper.ValidateDTO(Entry))
                return BadRequest("Invalid data");

            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            Entry? DbEntry = _context.Entries.FirstOrDefault(e => (e.Id == Entry.Id) && ((e.UserId == UserId) || (User.IsInRole("Administrator"))));

            if (DbEntry is null)
                return NotFound();

            DbEntry.Start = Entry.Start;
            DbEntry.End = Entry.End;
            DbEntry.Title = Entry.Title;
            DbEntry.Description = Entry.Description;

            _context.SaveChanges();

            string Username = User.IsInRole("Administrator") ? User.Identity.Name.Split("@")[0] : string.Empty;

            return Ok(EntryMapper.ToDTO(DbEntry));
        }

        public bool IsHoliday(DateTime Date)
        {
            return false;
        }

    }
}
