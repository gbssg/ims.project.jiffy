using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Services
{

    [Route("api/v1/[controller]")]
	[ApiController]
    [Authorize]
    public class EntriesController : ControllerBase
	{
        private ApplicationDbContext _context;

        public EntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("all")]
		public IActionResult GetAllEntries()
		{
            {
                var AspNetUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated )
                    return Unauthorized();

                var Entries = _context.Entries.Where(e => e.UserId == AspNetUser.Id || User.IsInRole("Administrator"))
                    .OrderByDescending(e => e.Start)
                    .Select(e => e.ToTimeEntry())
                    .ToList();

                return Ok(Entries);
            }
		}

		[HttpGet("id/{Id}")]
		public IActionResult GetEntryById(Guid Id)
		{
            {
                var AspNetUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                Entry? Result = _context.Entries.FirstOrDefault(e => e.Id == Id && ((e.UserId == AspNetUser.Id) || (User.IsInRole("Administrator"))));
                
                if (Result is null)
                {
                    return NotFound();
                }

                return Ok(Result.ToTimeEntry());
            }
        }

        [HttpPost("new")]
        public async Task<IActionResult> Add([FromBody] TimeEntry Entry)
        {
            if (string.IsNullOrWhiteSpace(Entry.Title) || string.IsNullOrWhiteSpace(Entry.Description))
                return BadRequest();

            {
                var AspNetUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                if (string.IsNullOrWhiteSpace(Entry.Title) || string.IsNullOrWhiteSpace(Entry.Description))
                    return BadRequest("Invalid data");

                Entry NewEntry = new()
                {
                    Id = Guid.NewGuid(),
                    Start = Entry.Start,
                    End = Entry.End,
                    Title = Entry.Title,
                    Description = Entry.Description,
                    UserId = AspNetUser.Id,
                    User = AspNetUser
                };
                _context.Entries.Add(NewEntry);
                _context.SaveChanges();

                return Ok(NewEntry.Id);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult AddDescription(Guid Id)
        {
            {
                var AspNetUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                Entry? Entry = _context.Entries.FirstOrDefault(e => (e.Id == Id) && ((e.UserId == AspNetUser.Id) || (User.IsInRole("Administrator"))));

                if (Entry is null)
                    return NotFound();

                _context.Entries.Remove(Entry);
                _context.SaveChanges();

                return Ok();
            }
        }

		[HttpPatch("update")]
        public IActionResult PatchEntry([FromBody] TimeEntry Entry)
        {
            {
                var AspNetUser = _context.Users.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                Entry? DbEntry = _context.Entries.FirstOrDefault(e => (e.Id == Entry.Id) && ((e.UserId == AspNetUser.Id) || (User.IsInRole("Administrator"))));

                if (DbEntry is null)
                    return NotFound();

                DbEntry.Start = Entry.Start;
                DbEntry.End = Entry.End;
                DbEntry.Title = Entry.Title;
                DbEntry.Description = Entry.Description;


                _context.SaveChanges();

                return Ok();
            }
        }
    }
}
