using FluentValidation;
using Bina.BLL.DTOs.Property;

namespace Bina.BLL.Validators
{
    public class PropertyFilterDtoValidator : AbstractValidator<PropertyFilterDto>
    {
        public PropertyFilterDtoValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("S?hif? nömr?si 0-dan böyük olmal?d?r.");

            RuleFor(x => x.PageSize)
                .InclusiveBetween(1, 50).WithMessage("S?hif? say? 1 il? 50 aras?nda olmal?d?r.");

            RuleFor(x => x.MinPrice)
                .GreaterThanOrEqualTo(0).When(x => x.MinPrice.HasValue).WithMessage("Minimum qiym?t m?nfi ola bilm?z.");

            RuleFor(x => x.MaxPrice)
                .GreaterThan(x => x.MinPrice).When(x => x.MaxPrice.HasValue && x.MinPrice.HasValue)
                .WithMessage("Maksimum qiym?t minimum qiym?td?n böyük olmal?d?r.");

            RuleFor(x => x.MinArea)
                .GreaterThan(0).When(x => x.MinArea.HasValue).WithMessage("Minimum sah? 0-dan böyük olmal?d?r.");

            RuleFor(x => x.MaxArea)
                .GreaterThan(x => x.MinArea).When(x => x.MaxArea.HasValue && x.MinArea.HasValue)
                .WithMessage("Maksimum sah? minimum sah?d?n böyük olmal?d?r.");

            RuleFor(x => x.SortBy)
                .Must(value => 
                    value == "price_asc" || value == "price_desc" || 
                    value == "date_asc" || value == "date_desc" || 
                    value == "area_asc" || value == "area_desc")
                .When(x => !string.IsNullOrEmpty(x.SortBy))
                .WithMessage("Yanl?? s?ralama format? ('price_asc', 'price_desc', 'date_asc', 'date_desc', 'area_asc', 'area_desc' olmal?d?r).");
        }
    }
}