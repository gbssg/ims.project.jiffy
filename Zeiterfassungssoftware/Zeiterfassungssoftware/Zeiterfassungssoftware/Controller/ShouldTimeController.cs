using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.ShouldTimes;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ShouldTimeController : ControllerBase
    {
        private ApplicationDbContext _context;

        public ShouldTimeController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetShouldTimes()
        {
            var ShouldTimes = await _context.ShouldTimes.Where(e => e.ValidUntil > DateTime.Now)
                                                        .Select(e => ShouldTimeMapper.ToDTO(e))
                                                        .ToListAsync();
            return Ok(ShouldTimes);
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetShouldTimeById(Guid Id)
        {
            var ShouldTime = await _context.ShouldTimes.FirstOrDefaultAsync(e => e.Id == Id && e.ValidUntil > DateTime.Now);

            if(ShouldTime is null)
                return NotFound();

            return Ok(ShouldTimeMapper.ToDTO(ShouldTime));
        }

        [HttpPut("/{Id}")]
        public async Task<IActionResult> UpdateShouldTime(Guid Id, [FromBody] ShouldTime ShouldTime)
        {
            if (!ShouldTimeMapper.ValidateDto(ShouldTime))
                return BadRequest();

            var OldShouldTime = _context.ShouldTimes.FirstOrDefault(e => e.Id == Id);
            if (OldShouldTime is null)
                return NotFound();

            OldShouldTime.ValidUntil = DateTime.Now;

            var NewShouldTime = ShouldTimeMapper.FromDTO(ShouldTime);
            NewShouldTime.Id = Guid.NewGuid();
            _context.ShouldTimes.Add(NewShouldTime);

            await _context.SaveChangesAsync();

            return Ok(ShouldTimeMapper.ToDTO(NewShouldTime));
        }

        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteShouldTime(Guid id)
        {
            var ShouldTime = await _context.ShouldTimes.FirstOrDefaultAsync(e => e.Id == id);
            if (ShouldTime is null)
                return NotFound();

            ShouldTime.ValidUntil = DateTime.Now;
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
