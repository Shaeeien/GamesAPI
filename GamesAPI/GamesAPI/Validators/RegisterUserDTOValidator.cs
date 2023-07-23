using FluentValidation;
using FluentValidation.Results;
using GamesAPI.DTOs;

namespace GamesAPI.Validators
{
    public class RegisterUserDTOValidator : AbstractValidator<RegisterUserDTO>
    {
        public RegisterUserDTOValidator() 
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("E-mail cannot be empty!");
            RuleFor(u => u.UserName).NotEmpty().WithMessage("Username cannot be empty!");
            RuleFor(u => u.Password).NotEmpty().WithMessage("Password cannot be empty!");
            RuleFor(u => u.Password).MinimumLength(8).WithMessage("Password must containt at least 8 characters!")
                .Matches("[A-Z]+").WithMessage("Password must contain at least 1 capital letter!")
                .Matches("[1-9]+").WithMessage("Password must contain at least 1 digit!")
                .Matches("[!@#%^&*()_+\\-=[\\]{}|;':\",./<>?~]+").WithMessage("Password must contain at least 1 special character!");
            RuleFor(u => u.RepeatPassword).NotEmpty().WithMessage("RepeatPassword cannot be empty!");
            RuleFor(u => u.RepeatPassword).Equal(u => u.Password).WithMessage("Password and RepeatPassword must be equal!");
        }
    }
}
