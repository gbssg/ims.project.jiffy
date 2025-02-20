using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;
using Zeiterfassungssoftware.Data.Jiffy;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Activities;
using ActivityDescription = Zeiterfassungssoftware.SharedData.Activities.ActivityDescription;
using ActivityTitle = Zeiterfassungssoftware.SharedData.Activities.ActivityTitle;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ActivitiesController : ControllerBase
    {

        [HttpGet("descriptions/all")]
        public IActionResult GetAllDescriptions()
        {
            using (var Context = new JiffyContext())
            {
                AspNetUser? AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));
                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                var Descriptions = Context.ActivityDescriptions.Where(e => (e.UserId == AspNetUser.Id));



                return Ok(Descriptions.ToList());
            }
        }

        
        [HttpPost("descriptions/new")]
        public async Task<IActionResult> AddDescription([FromBody] ActivityDescription Description)
        {
            if (string.IsNullOrWhiteSpace(Description.Value))
                return BadRequest();

            if (!User.Identity.IsAuthenticated)
                return Unauthorized();
            
            using (var Context = new JiffyContext())
            {
                var AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));

                var Temp = new Data.Jiffy.Models.ActivityDescription()
                {
                    Id = Guid.NewGuid(),
                    Value = Description.Value,
                    UserId = AspNetUser.Id,
                };

                Context.ActivityDescriptions.Add(Temp);
                Context.SaveChanges();

                return Ok();
            }
        }

        [HttpGet("titles/all")]
        public IActionResult GetAllTitles()
        {
            using (var Context = new JiffyContext())
            {
                AspNetUser? AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));
                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                var Titles = Context.ActivityTitles.Where(e => (e.UserId == AspNetUser.Id))
                                                   .Select(t => new ActivityTitle(t.Value));


                return Ok(Titles.ToList());
            }
        }

        //id
        [HttpPost("titles/new")]
		public async Task<IActionResult> AddTitle([FromBody] ActivityTitle Title)
        {
            if (string.IsNullOrWhiteSpace(Title.Value))
                return BadRequest();

            using (var Context = new JiffyContext())
            {
                var AspNetUser = Context.AspNetUsers.FirstOrDefault(e => string.Equals(e.Email, User.Identity.Name));
                if (AspNetUser is null || !User.Identity.IsAuthenticated)
                    return Unauthorized();

                var Temp = new Data.Jiffy.Models.ActivityTitle()
                {
                    Id = Guid.NewGuid(),
                    Value = Title.Value,
                    UserId = AspNetUser.Id,
                };

                Context.ActivityTitles.Add(Temp);
                Context.SaveChanges();

                return Ok();
            }
        }


    }
}
