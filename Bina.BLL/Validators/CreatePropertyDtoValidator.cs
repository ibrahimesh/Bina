using FluentValidation;
using Bina.BLL.DTOs.Property;
using Bina.DAL.Enums;
using System;

namespace Bina.BLL.Validators
{
    public class CreatePropertyDtoValidator : AbstractValidator<CreatePropertyDto>
    {
        public CreatePropertyDtoValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Ba?l?q mütl?qdir.")
                .Length(10, 200).WithMessage("Ba?l?q 10 il? 200 simvol aral???nda olmal?d?r.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("T?svir mütl?qdir.")
                .Length(20, 5000).WithMessage("T?svir 20 il? 5000 simvol aral???nda olmal?d?r.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Qiym?t 0-dan böyük olmal?d?r.")
                .LessThan(100_000_000).WithMessage("Qiym?t 100 milyondan kiçik olmal?d?r.");

            RuleFor(x => x.Area)
                .GreaterThan(0).WithMessage("Sah? 0-dan böyük olmal?d?r.")
                .LessThan(50_000).WithMessage("Sah? 50,000-d?n kiçik olmal?d?r.");

            RuleFor(x => x.Floor)
                .GreaterThan(0).When(x => x.Floor > 0).WithMessage("M?rt?b? müsb?t ?d?d olmal?d?r.")
                .Must((dto, floor) => floor <= dto.TotalFloors).When(x => x.Floor > 0 && x.TotalFloors > 0)
                .WithMessage("M?rt?b? ümumi m?rt?b? say?ndan böyük ola bilm?z.");

            RuleFor(x => x.TotalFloors)
                .GreaterThan(0).When(x => x.TotalFloors > 0).WithMessage("Ümumi m?rt?b? müsb?t ?d?d olmal?d?r.")
                .LessThan(200).WithMessage("Ümumi m?rt?b? 200-d?n kiçik olmal?d?r.");

            // Cross-field conditions
            RuleFor(x => x.TotalFloors)
                .NotEmpty().When(x => x.Floor > 0).WithMessage("M?rt?b? qeyd olunduqda ümumi m?rt?b? d? qeyd olunmal?d?r.");

            RuleFor(x => x.RoomCount)
                .InclusiveBetween(1, 50).WithMessage("Otaq say? 1 il? 50 aral???nda olmal?d?r.");

            RuleFor(x => x.ListingType)
                .Must(param => Enum.IsDefined(typeof(ListingType), param))
                .WithMessage("Elan?n tipi düzgün deyil.");

            RuleFor(x => x.CategoryId)
                .GreaterThan(0).WithMessage("Kateqoriya mütl?qdir.");

            RuleFor(x => x.CityId)
                .GreaterThan(0).WithMessage("??h?r mütl?qdir.");

            RuleFor(x => x.DistrictId)
                .GreaterThan(0).When(x => x.DistrictId.HasValue).WithMessage("S?hv rayon format?.");

            RuleFor(x => x.MetroId)
                .GreaterThan(0).When(x => x.MetroId.HasValue).WithMessage("S?hv metro format?.");
        }
    }
}