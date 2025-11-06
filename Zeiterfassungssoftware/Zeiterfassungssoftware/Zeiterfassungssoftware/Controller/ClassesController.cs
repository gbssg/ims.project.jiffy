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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClassById(Guid id)
        {
            var Class = await _context.Classes
                .Include(e => e.ShouldTimes)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (Class is null)
                return NotFound();

            return Ok(ClassMapper.ToDTO(Class));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var Class = await _context.Classes
                .Include(e => e.ShouldTimes)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (Class is null)
                return NotFound();


            var ShouldTimes = _context.ShouldTimes
                .Where(e => e.ClassId == id && e.ValidUntil > DateTime.Now);

            foreach (var ShouldTime in ShouldTimes)
            {
                ShouldTime.ValidUntil = DateTime.Now;
            }

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
        public async Task<IActionResult> AddClass([FromBody] SharedData.Classes.ClassDto classDto)
        {
            if (!ClassMapper.ValidateDTO(classDto))
                return BadRequest("Invalid data");

            var Class = ClassMapper.FromDTO(classDto);
            Class.Id = Guid.NewGuid();

            _context.Classes.Add(Class);
            await _context.SaveChangesAsync();

            return Ok(ClassMapper.ToDTO(Class));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClass(Guid id, [FromBody] SharedData.Classes.ClassDto classDto)
        {
            if (id == Guid.Empty)
                return BadRequest();

            if (!ClassMapper.ValidateDTO(classDto))
                return BadRequest("Invalid data");

            var DbClass = await _context.Classes
                .Include(c => c.ShouldTimes)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (DbClass is null)
                return NotFound();

            
            var Class = ClassMapper.FromDTO(classDto);
            DbClass.Name = Class.Name;

            await _context.SaveChangesAsync();

            return Ok(ClassMapper.ToDTO(DbClass));
        }

    }
}
