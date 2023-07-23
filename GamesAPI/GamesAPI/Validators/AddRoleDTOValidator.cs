using FluentValidation;
using GamesAPI.DTOs;

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
