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


        /// <summary>
        /// Gets a list of all users (for administrators only).
        /// </summary>
        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserDto>))]
        public async Task<ActionResult<List<UserDto>>> GetUsers()
        {
            var Users = await _context.Users.Select(e => UserMapper.ToDTO(e, new())).ToListAsync();

            foreach(UserDto user in Users)
            {
                var UserRoles = await _context.UserRoles.Where(e => e.UserId == user.Id).Select(e => e.RoleId).ToListAsync();
                var Roles = await _context.Roles.Where(e => UserRoles.Contains(e.Id)).Select(e => RoleMapper.ToDto(e)).ToListAsync();
                user.Roles = Roles;
            }

            return Ok(Users);
        }

        /// <summary>
        /// It gets a user's information (for administrators only).
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = "Administrator")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetUserById(string Id)
        {
            var User = await _userManager.FindByIdAsync(Id);

            if (User is null)
                return NotFound();

            var UserRoles = await _context.UserRoles.Where(e => e.UserId == Id).Select(e => e.RoleId).ToListAsync();
            var Roles = await _context.Roles.Where(e => UserRoles.Contains(e.Id)).ToListAsync();

            return Ok(UserMapper.ToDTO(User, Roles));
        }

        /// <summary>
        /// Creates a new user (for administrators only).
        /// </summary>
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
            return Ok(UserMapper.ToDTO(applicationUser, new()));
        }

        /// <summary>
        /// Deletes a user by ID (for administrators only).
        /// </summary>
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

        /// <summary>
        /// Updates a specific user's information. If the current user is an administrator, they can edit every field of every user. If the user is not an administrator, however, they can only edit their own class ID.
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> UpdateUser(string id, [FromBody, Required] UserDto user)
        {
            var applicationUser = await _userManager.FindByIdAsync(id);
            
            if (applicationUser == null)
                return NotFound();

            var UserRoles = await _context.UserRoles.Where(e => e.UserId == id).Select(e => e.RoleId).ToListAsync();
            var Roles = await _context.Roles.Where(e => UserRoles.Contains(e.Id)).ToListAsync();

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

                var newRoles = user.Roles
                    .Where(r => !string.IsNullOrWhiteSpace(r.Name))
                    .Select(r => r.Name!.Trim())
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var currentRoles = Roles
                    .Select(r => r.Name)
                    .Where(n => !string.IsNullOrWhiteSpace(n))
                    .Distinct(StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var existingRoleNames = await _context.Roles
                    .Select(r => r.Name)
                    .Where(n => n != null)
                    .ToListAsync();

                var missingRoles = newRoles
                    .Where(rn => !existingRoleNames.Contains(rn, StringComparer.OrdinalIgnoreCase))
                    .ToList();

                if (missingRoles.Count > 0)
                    return BadRequest(new { message = "One or more roles do not exist.", missingRoles });

                var toAdd = newRoles
                    .Except(currentRoles, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                var toRemove = currentRoles
                    .Except(newRoles, StringComparer.OrdinalIgnoreCase)
                    .ToList();

                if (toRemove.Count > 0)
                {
                    var removeResult = await _userManager.RemoveFromRolesAsync(applicationUser, toRemove);
                    if (!removeResult.Succeeded)
                        return BadRequest(removeResult.Errors);
                }

                if (toAdd.Count > 0)
                {
                    var addResult = await _userManager.AddToRolesAsync(applicationUser, toAdd);
                    if (!addResult.Succeeded)
                        return BadRequest(addResult.Errors);
                }

                UserRoles = await _context.UserRoles
                    .Where(e => e.UserId == id)
                    .Select(e => e.RoleId)
                    .ToListAsync();

                Roles = await _context.Roles
                    .Where(e => UserRoles.Contains(e.Id))
                    .ToListAsync();

                return Ok(UserMapper.ToDTO(applicationUser, Roles));
            }

            if(id == User.Claims.FirstOrDefault().Value)
            {
                applicationUser.ClassId = user.ClassId;

                var result = await _userManager.UpdateAsync(applicationUser);
                if (!result.Succeeded)
                    return BadRequest(result.Errors);

                return Ok(UserMapper.ToDTO(applicationUser, new()));
            }

            return BadRequest();
        }

    }

}
