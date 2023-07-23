using GamesAPI.DTOs;
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
        public ActionResult Add(AddRoleDTO dto)
        {
            IdentityRole<int> role = new IdentityRole<int>()
            {
                Name = dto.Name
            };
            if (_roleService.Exists(role))
                return Conflict("Role already exists");
            if(_roleService.Add(role))
                return Ok();
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpPatch("update/{id}")]
        public ActionResult Update(int id, [FromBody]AddRoleDTO dto)
        {
            IdentityRole<int>? roleToEdit = _roleService.FindById(id);
            if(roleToEdit != null)
            {
                IdentityRole<int> updatedRole = new IdentityRole<int>
                {
                    Name = dto.Name
                };
                if (_roleService.Update(roleToEdit, updatedRole))
                {
                    return Ok();
                }
            }            
            return BadRequest();
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("delete/{id}")]
        public ActionResult Remove(int id) 
        {
            var roleToRemove = _roleService.FindById(id);
            if(roleToRemove != null)
            {
                if (_roleService.Remove(roleToRemove))
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
