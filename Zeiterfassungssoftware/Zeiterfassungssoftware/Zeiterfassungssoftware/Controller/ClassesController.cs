using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Times;
using ActivityDescription = Zeiterfassungssoftware.SharedData.Activities.ActivityDescription;
using ActivityTitle = Zeiterfassungssoftware.SharedData.Activities.ActivityTitle;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllClasses()
        {
            var Classes = await _context.Classes
                 .Include(e => e.ShouldTimes)
                 .Select(e => ClassMapper.ToDTO(e))
                 .ToListAsync();

            return Ok(Classes);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetClassById(Guid Id)
        {
            var Class = await _context.Classes
                .Include(e => e.ShouldTimes)
                .FirstOrDefaultAsync(e => e.Id == Id);

            if (Class is null)
                return NotFound();

            return Ok(ClassMapper.ToDTO(Class));
        }

        //[HttpGet("own")]
        //public async Task<IActionResult> GetOwnClass()
        //{
        //    var UserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //    if (string.IsNullOrEmpty(UserId))
        //        return Unauthorized();

        //    var DbUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == UserId);
        //    if (DbUser == null)
        //        return Unauthorized();

        //    var Class = await _context.Classes
        //        //.Include(e => e.ShouldTimes)
        //        .FirstOrDefaultAsync(e => e.Id == DbUser.ClassId);

        //    if (Class is null)
        //        return NotFound();

        //    return Ok(ClassMapper.ToDTO(Class));
        //}

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteClassById(Guid Id)
        {
            if (Id == Guid.Empty)
                return BadRequest();

            var Class = await _context.Classes
                .Include(e => e.ShouldTimes)
                .FirstOrDefaultAsync(e => e.Id == Id);

            if (Class is null)
                return NotFound();


            var ShouldTime = _context.ShouldTimes
                .Where(e => e.ClassId == Id && e.ValidUntil > DateTime.Now);

            if (ShouldTime.Any())
                return Conflict("There are still ShouldTimes connected to this Class");


            foreach(var User in _context.Users.Where(e => e.ClassId == Class.Id))
            {
                User.ClassId = Guid.Empty;
            }

            _context.Classes.Remove(Class);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddClass([FromBody] SharedData.Classes.Class Class)
        {
            if (!ClassMapper.ValidateDTO(Class))
                return BadRequest("Invalid data");

            var DbClass = ClassMapper.FromDTO(Class);
            DbClass.Id = Guid.NewGuid();

            _context.Classes.Add(DbClass);
            await _context.SaveChangesAsync();

            return Ok(ClassMapper.ToDTO(DbClass));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPatch("{Id}")]
        public async Task<IActionResult> UpdateClass(Guid Id, [FromBody] SharedData.Classes.Class ClassDto)
        {
            if (Id == Guid.Empty)
                return BadRequest();

            if (!ClassMapper.ValidateDTO(ClassDto))
                return BadRequest("Invalid data");

            var DbClass = await _context.Classes
                .Include(c => c.ShouldTimes)
                .FirstOrDefaultAsync(c => c.Id == Id);

            if (DbClass is null)
                return NotFound();

            
            var Class = ClassMapper.FromDTO(ClassDto);
            DbClass.Name = Class.Name;

            await _context.SaveChangesAsync();

            return Ok(ClassMapper.ToDTO(DbClass));
        }

    }
}
