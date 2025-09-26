using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net.WebSockets;
using System.Security.Claims;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Data.Jiffy.Models;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Time;
using Zeiterfassungssoftware.SharedData.User;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
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

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _context.Users.Select(e => UserMapper.ToDTO(e)).ToListAsync();
            return Ok(Users);
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddUsers([FromBody] User user)
        {
            if(string.IsNullOrWhiteSpace(user.Password) || user.Password.Length < 6)
                return BadRequest();

            if (await _context.Users.AnyAsync(e => e.NormalizedEmail == user.NormalizedEmail))
                return Conflict();

            var applicationUser = UserMapper.FromDTO(user);

            var result = await _userManager.CreateAsync(applicationUser, user.Password);

            if (!result.Succeeded)
                return BadRequest(result.Errors);
;
            return Ok(UserMapper.ToDTO(applicationUser));
        }

        [Authorize(Roles = "Administrator")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser == null)
                return NotFound();

            var result = await _userManager.DeleteAsync(applicationUser);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return NoContent();
        }

        [Authorize(Roles = "Administrator")]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] Zeiterfassungssoftware.SharedData.User.User user)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser == null)
                return NotFound();

            if (!string.IsNullOrWhiteSpace(user.UserName))
                applicationUser.UserName = user.UserName;

            if (!string.IsNullOrWhiteSpace(user.Email))
                applicationUser.Email = user.Email;

            applicationUser.PhoneNumber = user.PhoneNumber ?? applicationUser.PhoneNumber;
            applicationUser.PhoneNumberConfirmed = user.PhoneNumberConfirmed;
            applicationUser.TwoFactorEnabled = user.TwoFactorEnabled;

            applicationUser.LockoutEnabled = user.LockoutEnabled;
            applicationUser.AccessFailedCount = user.AccessFailedCount;
            applicationUser.EmailConfirmed = user.EmailConfirmed;

            if (user.LockoutEnd > DateTime.MinValue)
                applicationUser.LockoutEnd = user.LockoutEnd;
            else
                applicationUser.LockoutEnd = null;

            var result = await _userManager.UpdateAsync(applicationUser);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            if (!string.IsNullOrWhiteSpace(user.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(applicationUser);
                var passResult = await _userManager.ResetPasswordAsync(applicationUser, token, user.Password);
                if (!passResult.Succeeded)
                    return BadRequest(passResult.Errors);
            }

            return Ok(UserMapper.ToDTO(applicationUser));
        }

    }

}
