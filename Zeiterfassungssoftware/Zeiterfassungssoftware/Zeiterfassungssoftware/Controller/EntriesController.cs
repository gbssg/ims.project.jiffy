using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
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
			return Ok(TimeEntrySource.GetEntries());
		}

		[HttpGet("id/{Id}")]
		public IActionResult GetEntryById(Guid Id)
		{
			TimeEntry? Result = TimeEntrySource.GetEntries().Where(e => e.Id == Id).FirstOrDefault();

			if(Result is null)
			{
				return NotFound();
			}

			return Ok(Result);
		}

		[HttpPost("new")]
		public IActionResult Add([FromBody] TimeEntry Entry)
		{
			TimeEntrySource.Add(Entry);
			return Ok();
		}

        [HttpDelete("delete/{id}")]
        public IActionResult AddDescription(Guid Id)
        {
            TimeEntry? Entry = TimeEntrySource.GetEntries().Where(e => e.Id == Id).FirstOrDefault();

            if (Entry is null)
            {
                return NotFound();
            }

            TimeEntrySource.Remove(Entry);
            return Ok();
        }

		[HttpPatch("update")]
        public IActionResult PatchEntry([FromBody] TimeEntry Entry)
        {
            if(Entry is null)
				return BadRequest();

			try
			{
				TimeEntrySource.Update(Entry);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
			
            return Ok();
        }
    }
}
