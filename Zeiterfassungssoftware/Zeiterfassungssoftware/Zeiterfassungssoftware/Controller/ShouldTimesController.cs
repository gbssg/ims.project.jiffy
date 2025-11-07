using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.ShouldTimes;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class ShouldTimesController : ControllerBase
    {
        private ApplicationDbContext _context;

        public ShouldTimesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of all the shouldtimes.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<ShouldTimeDto>))]
        public async Task<ActionResult<List<ShouldTimeDto>>> GetShouldTimes()
        {
            var ShouldTimes = await _context.ShouldTimes.Where(e => e.ValidUntil > DateTime.Now)
                                                        .Select(e => ShouldTimeMapper.ToDTO(e))
                                                        .ToListAsync();
            return Ok(ShouldTimes);
        }

        /// <summary>
        /// Gets a specific shouldtime.
        /// </summary>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShouldTimeDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShouldTimeDto>> GetShouldTimeById(Guid id)
        {
            var ShouldTime = await _context.ShouldTimes.FirstOrDefaultAsync(e => e.Id == id && e.ValidUntil > DateTime.Now);

            if(ShouldTime is null)
                return NotFound();

            return Ok(ShouldTimeMapper.ToDTO(ShouldTime));
        }

        /// <summary>
        /// Updates a specific shouldtime.
        /// </summary>
        [HttpPut("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ShouldTimeDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ShouldTimeDto>> UpdateShouldTime(Guid id, [FromBody, Required] ShouldTimeDto shouldTimeDto)
        {
            if (!ShouldTimeMapper.ValidateDto(shouldTimeDto))
                return BadRequest();

            var OldShouldTime = _context.ShouldTimes.FirstOrDefault(e => e.Id == id);
            if (OldShouldTime is null)
                return NotFound();

            OldShouldTime.ValidUntil = DateTime.Now;

            var NewShouldTime = ShouldTimeMapper.FromDTO(shouldTimeDto);
            NewShouldTime.Id = Guid.NewGuid();
            _context.ShouldTimes.Add(NewShouldTime);

            await _context.SaveChangesAsync();

            return Ok(ShouldTimeMapper.ToDTO(NewShouldTime));
        }


        /// <summary>
        /// Deletes a specific shouldtime.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
