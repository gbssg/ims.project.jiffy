using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Zeiterfassungssoftware.Data;
using Zeiterfassungssoftware.Mapper;
using Zeiterfassungssoftware.SharedData.ShouldTimes;
using Zeiterfassungssoftware.SharedData.Users;

namespace Zeiterfassungssoftware.Controller
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UsersController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var Users = await _context.Users.Select(e => UserMapper.ToDTO(e)).ToListAsync();
            return Ok(Users);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserById(string Id)
        {
            var User = await _userManager.FindByIdAsync(Id);

            if (User is null)
                return NotFound();

            return Ok(UserMapper.ToDTO(User));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> AddUsers([FromBody, Required] UserDto user)
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

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> UpdateUser(string id, [FromBody, Required] UserDto user)
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
