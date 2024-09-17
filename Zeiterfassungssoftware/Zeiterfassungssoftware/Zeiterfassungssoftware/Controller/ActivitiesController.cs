using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ActivitiesController : ControllerBase
    {
        private IActivityProvider ActivitySource;

        public ActivitiesController(IActivityProvider ActivitySource)
        {
            this.ActivitySource = ActivitySource;
        }

        [HttpGet("titles/all")]
        public IActionResult GetAllTitles()
        {
            return Ok(ActivitySource.GetActivityTitles());
        }

        [HttpGet("descriptions/all")]
        public IActionResult GetAllDescriptions()
        {
            return Ok(ActivitySource.GetActivityDescriptions());
        }

        //[HttpGet("descriptions/id/{Id}")]
        //public IActionResult GetEntryById(int Id)
        //{
        //    TimeEntry? Result = ActivitySource.GetActivityDescriptions().Where(e => e.Id == Id).FirstOrDefault();

        //    if (Result is null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(Result);
        //}

        [HttpPost("descriptions/new")]
		public async Task<IActionResult> AddDescription([FromBody] ActivityDescription Description)
        {
            ActivitySource.Add(Description);
            return Ok();
        }

    }
}
