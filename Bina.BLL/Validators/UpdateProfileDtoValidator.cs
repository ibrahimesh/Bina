using FluentValidation;
using Bina.BLL.DTOs.User;
using System;
using System.Text.RegularExpressions;

namespace Bina.BLL.Validators
{
    public class UpdateProfileDtoValidator : AbstractValidator<UpdateProfileDto>
    {
        public UpdateProfileDtoValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty().WithMessage("Ad mütl?qdir.")
                .Length(2, 100).WithMessage("Ad 2 il? 100 simvol aral???nda olmal?d?r.");

            RuleFor(x => x.PhoneNumber)
                .Matches(@"^\+994[0-9]{9}$").When(x => !string.IsNullOrEmpty(x.PhoneNumber))
                .WithMessage("Telefon nömr?si düzgün formatda deyil (m?s?l?n: +994501234567).");

            RuleFor(x => x.AvatarUrl)
                .Must(url => Uri.TryCreate(url, UriKind.Absolute, out _))
                .When(x => !string.IsNullOrEmpty(x.AvatarUrl))
                .WithMessage("URL format? yanl??d?r.");
        }
    }
}