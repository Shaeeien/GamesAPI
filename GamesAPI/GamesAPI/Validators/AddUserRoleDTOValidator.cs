using FluentValidation;
using GamesAPI.DTOs.UserRoles;

namespace GamesAPI.Validators
{
    public class AddUserRoleDTOValidator : AbstractValidator<AddUserRoleDTO>
    {
        public AddUserRoleDTOValidator() 
        {
            RuleFor(dto => dto.RoleId).GreaterThan(0).WithMessage("Role id should be greater than zero");
            RuleFor(dto => dto.UserId).GreaterThan(0).WithMessage("User id should be greater than zero");
        }
    }
}
