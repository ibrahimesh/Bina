using FluentValidation;
using Bina.BLL.DTOs.User;
using System.Text.RegularExpressions;

namespace Bina.BLL.Validators
{
    public class RegisterDtoValidator : AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad mütl?qdir.")
                .Length(2, 100).WithMessage("Ad 2 il? 100 simvol aral???nda olmal?d?r.")
                .Matches(@"^[\p{L}\s]+$").WithMessage("Ad yaln?z h?rfl?r v? bo?luqlardan ibar?t olmal?d?r.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-poçt mütl?qdir.")
                .EmailAddress().WithMessage("Düzgün e-poçt format? daxil edin.")
                .MaximumLength(255).WithMessage("E-poçt çox uzundur.");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon nömr?si mütl?qdir.")
                .Matches(@"^\+994[0-9]{9}$").WithMessage("Telefon nömr?si düzgün formatda deyil (m?s?l?n: +994501234567).");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("?ifr? mütl?qdir.")
                .MinimumLength(8).WithMessage("?ifr? minimum 8 simvol olmal?d?r.")
                .MaximumLength(100).WithMessage("?ifr? maksimum 100 simvol olmal?d?r.")
                .Must(HaveValidPassword).WithMessage("?ifr?d? ?n az?: 1 böyük h?rf, 1 kiçik h?rf, 1 r?q?m v? 1 xüsusi simvol olmal?d?r.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password).WithMessage("?ifr?l?r uy?un g?lmir.");
        }

        private bool HaveValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password)) return false;

            var hasUpper = new Regex(@"[A-Z]");
            var hasLower = new Regex(@"[a-z]");
            var hasDigit = new Regex(@"[0-9]");
            var hasSpecial = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            return hasUpper.IsMatch(password) && hasLower.IsMatch(password) && hasDigit.IsMatch(password) && hasSpecial.IsMatch(password);
        }
    }
}