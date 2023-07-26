using FluentValidation;
using GamesAPI.DTOs.Roles;

namespace GamesAPI.Validators
{
    public class AddRoleDTOValidator : AbstractValidator<AddRoleDTO>
    {
        public AddRoleDTOValidator()
        {
            RuleFor(r => r.Name).NotEmpty();
        }
    }
}
