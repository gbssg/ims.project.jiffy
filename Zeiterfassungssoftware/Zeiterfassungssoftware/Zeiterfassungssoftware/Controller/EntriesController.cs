using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Diagnostics;
using System.Security.Claims;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Services
{

	[Route("api/v1/[controller]")]
	[ApiController]
    [Authorize]
    public class EntriesController : ControllerBase
	{
        private ITimeEntryProvider TimeEntrySource;

        public EntriesController(ITimeEntryProvider TimeEntrySource)
        {
            this.TimeEntrySource = TimeEntrySource;
        }

        [HttpGet("all")]
		public IActionResult GetAllEntries()
		{
            using (var Context = new JiffyContext())
            {
                AspNetUser? AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated )
                    return Unauthorized();

                var Entries = Context.Entries.Where(e => e.UserId == AspNetUser.Id || User.IsInRole("Administrator"))
                    .OrderByDescending(e => e.Start)
                    .Select(e => e.ToTimeEntry())
                    .ToList();

                return Ok(Entries);
            }
		}

		[HttpGet("id/{Id}")]
		public IActionResult GetEntryById(Guid Id)
		{

            using (var Context = new JiffyContext())
            {
                AspNetUser? AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                Entry? Result = Context.Entries.FirstOrDefault(e => e.Id == Id && ((e.UserId == AspNetUser.Id) || (User.IsInRole("Administrator"))));
                
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
            using (var Context = new JiffyContext())
            {
                AspNetUser? AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

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
                Context.Entries.Add(NewEntry);
                Context.SaveChanges();

                return Ok(NewEntry.Id);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult AddDescription(Guid Id)
        {
            using (var Context = new JiffyContext())
            {
                AspNetUser? AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                Entry? Entry = Context.Entries.FirstOrDefault(e => (e.Id == Id) && ((e.UserId == AspNetUser.Id) || (User.IsInRole("Administrator"))));

                if (Entry is null)
                    return NotFound();

                Context.Entries.Remove(Entry);
                Context.SaveChanges();

                return Ok();
            }
        }

		[HttpPatch("update")]
        public IActionResult PatchEntry([FromBody] TimeEntry Entry)
        {
            using (var Context = new JiffyContext())
            {
                AspNetUser? AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                Entry? DbEntry = Context.Entries.FirstOrDefault(e => (e.Id == Entry.Id) && ((e.UserId == AspNetUser.Id) || (User.IsInRole("Administrator"))));

                if (Entry is null)
                    return NotFound();

                DbEntry.Start = Entry.Start;
                DbEntry.End = Entry.End;
                DbEntry.Title = Entry.Title;
                DbEntry.Description = Entry.Description;


                Context.SaveChanges();

                return Ok();
            }
        }
    }
}
