using GamesAPI.DTOs.User;
using GamesAPI.Models;
using GamesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BC = BCrypt.Net.BCrypt;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("/api/user")]
    [Authorize(Roles = "Admin")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public ActionResult Register([FromBody] RegisterUserDTO dto)
        {
            if (ModelState.IsValid)
            {
                AppUser userToAdd = new AppUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    PasswordHash = BC.HashPassword(dto.Password),
                };
                if (!_userService.Exists(userToAdd))
                {
                    try                   
                    {
                        _userService.Add(userToAdd);
                        return Created("https://localhost:7054/api/user/register", userToAdd);
                    }
                    catch
                    {
                        return BadRequest("Error when creating a user");
                    }
                }
                else
                {
                    return Conflict("This user already exists(either email or username is taken)");
                }                
            }
            return BadRequest();
        }

        [HttpDelete("remove/{id}")]
        public async Task<ActionResult> RemoveUserById(int id)
        {
            if (ModelState.IsValid)
            {
                var userToRemove = await _userService.FindById(id);
                if (userToRemove != null)
                {
                    if (await _userService.Remove(userToRemove))
                    {
                        return Ok();
                    }
                }
                return NotFound("User does not exist");
            }
            return BadRequest("Cannot delete user");
        }

        [HttpPatch("update/{id}")]
        public async Task<ActionResult> UpdateUser(int id, [FromBody]UpdateUserDTO dto)
        {
            if (ModelState.IsValid)
            {
                AppUser? userToUpdate = await _userService.FindById(id);
                if(userToUpdate != null)
                {
                    if (!_userService.IsUserNameTaken(dto.UserName))
                        userToUpdate.UserName = dto.UserName;
                    else return Conflict("This username is already taken");
                    if(!_userService.IsEmailTaken(dto.Email))
                        userToUpdate.Email = dto.Email;
                    else return Conflict("This email is already taken");
                    if (dto.Password != null)
                    {
                        userToUpdate.PasswordHash = BC.HashPassword(dto.Password);
                    }
                    await _userService.SaveChanges();
                    return Ok();
                }
                return NotFound();
            }
            return BadRequest();
        }
    }
}