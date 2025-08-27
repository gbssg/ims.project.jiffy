using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
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
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPatch("set-class/{Id}")]
        public async Task<IActionResult> SetClass(Guid Id)
        {
            var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();

            var Class = await _context.Classes.FirstOrDefaultAsync(e => e.Id == Id);
            if (Class is null)
                return NotFound("Class not found");

            var DbUser = await _context.Users.FirstOrDefaultAsync(e => e.Id == UserId);
            if (DbUser is null)
                return Unauthorized();
            
            DbUser.ClassId = Id;

            await _context.SaveChangesAsync();
            return Ok();
        }
    }
    
}
