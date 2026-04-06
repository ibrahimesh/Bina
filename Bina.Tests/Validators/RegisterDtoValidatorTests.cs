using System.Linq;
using Bina.BLL.DTOs.User;
using Bina.BLL.Validators;
using FluentAssertions;
using Xunit;

namespace Bina.Tests.Validators
{
    public class RegisterDtoValidatorTests
    {
        private readonly RegisterDtoValidator _validator;

        public RegisterDtoValidatorTests()
        {
            _validator = new RegisterDtoValidator();
        }

        [Fact]
        public void Validate_ValidUser_ShouldNotHaveErrors()
        {
            var user = new RegisterDto
            {
                FullName = "R??ad Nuri",
                Email = "reshad@gmail.com",
                PhoneNumber = "+994501234567",
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            var result = _validator.Validate(user);

            // Log detailed failures into assertion logic
            var errors = string.Join(", ", result.Errors.Select(x => x.ErrorMessage));
            result.IsValid.Should().BeTrue(because: "Gözl?nilm?y?n x?ta tap?ld?: " + errors);
            result.Errors.Should().BeEmpty();
        }

        [Fact]
        public void Validate_InvalidPasswordMismatch_ShouldHaveError()
        {
            var user = new RegisterDto
            {
                FullName = "R??ad Nuri",
                Email = "reshad@gmail.com",
                PhoneNumber = "+994501234567",
                Password = "Password123!",
                ConfirmPassword = "WrongPassword!"
            };

            var result = _validator.Validate(user);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == "ConfirmPassword" && e.ErrorMessage.Contains("?ifr?l?r uy?un g?lmir"));
        }

        [Fact]
        public void Validate_InvalidAzerbaijaniPhoneFormat_ShouldHaveError()
        {
            var user = new RegisterDto
            {
                FullName = "R??ad Nuri",
                Email = "reshad@gmail.com",
                PhoneNumber = "0501234567", // S?hv format (+994 yoxdur)
                Password = "Password123!",
                ConfirmPassword = "Password123!"
            };

            var result = _validator.Validate(user);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle(e => e.PropertyName == "PhoneNumber");
        }
    }
}