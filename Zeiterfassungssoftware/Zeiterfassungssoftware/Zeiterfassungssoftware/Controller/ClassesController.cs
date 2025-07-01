using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Activities;
using Zeiterfassungssoftware.SharedData.Time;
using ActivityDescription = Zeiterfassungssoftware.SharedData.Activities.ActivityDescription;
using ActivityTitle = Zeiterfassungssoftware.SharedData.Activities.ActivityTitle;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ClassesController : ControllerBase
    {
        private ApplicationDbContext _context;

        public ClassesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAllClasses()
        {
            var Classes = _context.Classes;
            foreach(var Class in Classes)
            {
                Class.ShouldTimes = _context.ShouldTimes.Where(e => e.ClassId == Class.Id).ToList();
            }
            return Ok(Classes.Select(e => ClassMapper.ToDTO(e)).ToList());
        }

        [HttpGet("{Id}")]
        public IActionResult GetClassById(Guid Id)
        {
            var Class = _context.Classes.FirstOrDefault(e => e.Id.Equals(Id));
            if (Class is null)
                return NotFound();

            Class.ShouldTimes = _context.ShouldTimes.Where(e => e.ClassId == Class.Id).ToList();

            return Ok(ClassMapper.ToDTO(Class));
        }

        [HttpGet("own")]
        public IActionResult GetOwnClass()
        {
            var UserId = User.Claims.FirstOrDefault()?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(UserId))
                return Unauthorized();
            
            var DbUser = _context.Users.FirstOrDefault(e => string.Equals(e.Id, UserId));
            if (DbUser is null)
                return Unauthorized();


            var Class = _context.Classes.FirstOrDefault(e => e.Id.Equals(DbUser.ClassId));
            if (Class is null)
                return NotFound();

            Class.ShouldTimes = _context.ShouldTimes.Where(e => e.ClassId == Class.Id).ToList();
            
            return Ok(ClassMapper.ToDTO(Class));
        }
        [Authorize(Roles = "Administrator")]
        [HttpDelete("{Id}")]
        public IActionResult DeleteClassById(Guid Id)
        {
            if (Id.Equals(Guid.Empty))
                return Forbid();

            var Class = _context.Classes.FirstOrDefault(e => e.Id.Equals(Id));
            if (Class is null)
                return NotFound();

            _context.Classes.Remove(Class);
            _context.SaveChanges();

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

            foreach(var ShouldTime in DbClass.ShouldTimes)
            {
                ShouldTime.Id = Guid.NewGuid();
                ShouldTime.ClassId = DbClass.Id;
                _context.ShouldTimes.Add(ShouldTime);
            }

            _context.Classes.Add(DbClass);
            _context.SaveChanges();

            return Ok(ClassMapper.ToDTO(DbClass));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPatch]
        public IActionResult UpdateClass([FromBody] SharedData.Classes.Class Class)
        {
            if (Class.Id.Equals(Guid.Empty))
                return Forbid();

            if (!ClassMapper.ValidateDTO(Class))
                return BadRequest("Invalid data");

            Class? DbClass = _context.Classes.FirstOrDefault(e => e.Id.Equals(Class.Id));
            if (DbClass is null)
                return NotFound();

            DbClass = ClassMapper.FromDTO(Class);
            _context.SaveChanges();

            return Ok(ClassMapper.ToDTO(DbClass));
        }

    }
}
