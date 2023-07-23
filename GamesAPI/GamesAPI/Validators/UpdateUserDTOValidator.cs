using FluentValidation;
using GamesAPI.DTOs;

namespace GamesAPI.Validators
{
    public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
    {
        public UpdateUserDTOValidator() 
        {
            RuleFor(u => u.Password).Equal(u => u.RepeatPassword).WithMessage("Passwords must be equal");
        }
    }
}
