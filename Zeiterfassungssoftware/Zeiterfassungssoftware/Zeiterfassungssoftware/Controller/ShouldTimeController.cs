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

        [HttpPatch("/{Id}")]
        public async Task<IActionResult> UpdateShouldTime([FromBody] ShouldTime ShouldTime)
        {
            return Ok();
        }
    }
}
