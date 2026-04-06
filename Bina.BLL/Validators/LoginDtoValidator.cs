using FluentValidation;
using Bina.BLL.DTOs.User;

namespace Bina.BLL.Validators
{
    public class LoginDtoValidator : AbstractValidator<LoginDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-poçt mütl?qdir.")
                .EmailAddress().WithMessage("Düzgün e-poçt format? deyil.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("?ifr? daxil edin.");
        }
    }
}