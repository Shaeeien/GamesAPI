using GamesAPI.DTOs.UserRoles;
using GamesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [ApiController]
    [EnableCors("ReactApp")]
    [Route("/api/userroles")]
    public class UserRolesController : ControllerBase
    {
        private readonly IUserRolesService _userRolesService;
        public UserRolesController(IUserRolesService userRolesService)
        {
            _userRolesService = userRolesService;
        }

        [HttpPost("add"), Authorize(Roles = "Admin")]
        public ActionResult Add([FromBody] AddUserRoleDTO dto)
        {
            if(dto == null) 
            {
                return ValidationProblem();
            }

            try
            {
                IdentityUserRole<int> roleToAdd = new IdentityUserRole<int>()
                {
                    UserId = dto.UserId,
                    RoleId = dto.RoleId
                };
                _userRolesService.Add(roleToAdd);
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPut("update"), Authorize(Roles = "Admin")]
        public ActionResult Edit() 
        {
            return Ok();
        }

        [HttpDelete, Authorize(Roles = "Admin")]
        public ActionResult Delete() 
        {
            return Ok();
        }
    }
}
