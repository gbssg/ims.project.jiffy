using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.SharedData.Time;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPatch("{Id}")]
        public IActionResult PatchEntry(Guid Id)
        {
            var UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;

            var Class = _context.Classes.FirstOrDefault(e => e.Id == Id);
            if (Class is null)
                return BadRequest();

            var DbUser = _context.Users.FirstOrDefault(e => e.Id == UserId);
                
            DbUser.ClassId = Id;
            _context.SaveChanges();

            return Ok();
        }
    }
    
}
