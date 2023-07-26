using FluentValidation;
using GamesAPI.DTOs.User;

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
