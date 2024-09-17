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


        [HttpGet("descriptions/all")]
        public IActionResult GetAllDescriptions()
        {
            return Ok(ActivitySource.GetActivityDescriptions());
        }

		//id
		[HttpPost("descriptions/new")]
		public async Task<IActionResult> AddDescription([FromBody] ActivityDescription Description)
		{
			if (string.IsNullOrWhiteSpace(Description.Value))
				return BadRequest();

			ActivitySource.Add(Description);
            return Ok();
        }

        [HttpGet("titles/all")]
        public IActionResult GetAllTitles()
        {
            return Ok(ActivitySource.GetActivityTitles());
        }

        //id
        [HttpPost("titles/new")]
		public async Task<IActionResult> AddTitle([FromBody] ActivityTitle Title)
        {
            if (string.IsNullOrWhiteSpace(Title.Value))
                return BadRequest();

            ActivitySource.Add(Title);
            return Ok();
        }

    }
}
