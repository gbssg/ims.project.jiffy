using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.Users;

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


        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var Users = await _context.Users.Select(e => UserMapper.ToDTO(e)).ToListAsync();
            return Ok(Users);
        }

        [Authorize(Roles = "Administrator")]
        [HttpGet]
        public async Task<IActionResult> GetUserById(string Id)
        {
            var User = await _userManager.FindByIdAsync(Id);

            if (User is null)
                return NotFound();

            return Ok(UserMapper.ToDTO(User));
        }

        [Authorize(Roles = "Administrator")]
        [HttpPost]
        public async Task<IActionResult> AddUsers([FromBody] UserDto user)
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

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] Zeiterfassungssoftware.SharedData.Users.UserDto user)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            if (applicationUser == null)
                return NotFound();

            if(User.IsInRole("Administrator"))
            {
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

                applicationUser.ClassId = user.ClassId;

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

            if(id == User.Claims.FirstOrDefault().Value)
            {
                applicationUser.ClassId = user.ClassId;

                var result = await _userManager.UpdateAsync(applicationUser);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok(UserMapper.ToDTO(applicationUser));
            }

            return BadRequest();
        }

    }

}
