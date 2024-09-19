using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Services
{

	[Route("api/v1/[controller]")]
	[ApiController]
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
		public IActionResult GetEntryById(int Id)
		{
			TimeEntry? Result = TimeEntrySource.GetEntries().Where(e => e.Id == Id).FirstOrDefault();

			if(Result is null)
			{
				return NotFound();
			}

			return Ok(Result);
		}

		[HttpPost("new")]
		public async Task<IActionResult> AddDescription([FromBody] TimeEntry Entry)
		{
			TimeEntrySource.Add(Entry);
			return Ok();
		}
	}
}
