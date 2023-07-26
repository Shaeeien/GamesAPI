using GamesAPI.DTOs.Roles;
using GamesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace GamesAPI.Controllers
{
    [ApiController]
    [Route("/api/role")]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _roleService;
        public RolesController(IRoleService roleService) 
        {
            _roleService = roleService;
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("add")]
        public async Task<ActionResult> Add(AddRoleDTO dto)
        {
            IdentityRole<int> role = new IdentityRole<int>()
            {
                Name = dto.Name
            };
            if (_roleService.Exists(role))
                return Conflict("Role already exists");
            if(await _roleService.Add(role))
                return Ok();
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("update/{id}")]
        public async Task<ActionResult> Update(int id, [FromBody]AddRoleDTO dto)
        {
            IdentityRole<int>? roleToEdit = await _roleService.FindById(id);
            if(roleToEdit != null)
            {
                IdentityRole<int> updatedRole = new IdentityRole<int>
                {
                    Name = dto.Name
                };
                if (await _roleService.Update(roleToEdit, updatedRole))
                {
                    return Ok();
                }
            }            
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Remove(int id) 
        {
            var roleToRemove = await _roleService.FindById(id);
            if(roleToRemove != null)
            {
                if (await _roleService.Remove(roleToRemove))
                    return Ok();
            }
            else
            {
                return NotFound();
            }
            return BadRequest();
        }
    }
}
