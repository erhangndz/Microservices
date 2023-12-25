using FluentValidation;
using FreeCourse.Web.Models.Discounts;

namespace FreeCourse.Web.Validators
{
    public class DiscountApplyInputValidator:AbstractValidator<DiscountApplyInput>
    {

        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("İndirim Kodu alanı boş bırakılamaz");
        }
    }
}
