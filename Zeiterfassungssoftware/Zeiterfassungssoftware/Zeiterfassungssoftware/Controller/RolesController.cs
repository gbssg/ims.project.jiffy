using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Roles;
using Zeiterfassungssoftware.SharedData.ShouldTimes;
using Zeiterfassungssoftware.SharedData.Times;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrator")]
    public class RolesController : ControllerBase
    {
        private ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Gets a list of all the shouldtimes.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<RoleDto>))]
        public async Task<ActionResult<List<RoleDto>>> GetRoles()
        {
            var Roles = await _context.Roles.Select(e => RoleMapper.ToDto(e))
                                            .ToListAsync();

            return Ok(Roles);
        }

        /// <summary>
        /// Gets a specific shouldtime.
        /// </summary>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<RoleDto>> GetRoleById(string id)
        {
            var Role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == id);

            if (Role is null)
                return NotFound();

            return Ok(RoleMapper.ToDto(Role));
        }

        /// <summary>
        /// Creates a new shouldtime
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> CreateRole([FromBody, Required] RoleDto roleDto)
        {
            if (!RoleMapper.ValidateDto(roleDto))
                return BadRequest("Invalid data");

            var Role = RoleMapper.FromDto(roleDto);
            Role.Id = Guid.NewGuid().ToString();
            Role.Name = roleDto.Name;
            Role.NormalizedName = roleDto.NormalizedName;
            Role.ConcurrencyStamp = roleDto.ConcurrencyStamp;

            _context.Roles.Add(Role);
            await _context.SaveChangesAsync();

            return Ok(RoleMapper.ToDto(Role));
        }

        /// <summary>
        /// Creates a new shouldtime
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TimeEntryDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateRole(string id, [FromBody, Required] RoleDto roleDto)
        {
            if (!RoleMapper.ValidateDto(roleDto))
                return BadRequest("Invalid data");

            IdentityRole? Role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == id);

            if (Role is null)
                return NotFound();

            Role.Name = roleDto.NormalizedName;
            Role.NormalizedName = roleDto.NormalizedName;
            Role.ConcurrencyStamp = roleDto.ConcurrencyStamp;

            await _context.SaveChangesAsync();

            return Ok(RoleMapper.ToDto(Role));
        }

        /// <summary>
        /// Deletes a specific shouldtime.
        /// </summary>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteRole(string id)
        {
            var Role = await _context.Roles.FirstOrDefaultAsync(e => e.Id == id);
            if (Role is null)
                return NotFound();

            _context.Roles.Remove(Role);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
