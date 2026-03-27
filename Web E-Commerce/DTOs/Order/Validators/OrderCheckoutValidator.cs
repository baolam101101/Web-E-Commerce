using FluentValidation;
using Web_E_Commerce.DTOs.Order.Requests;
using Web_E_Commerce.DTOs.Shared.Constants;

namespace Web_E_Commerce.DTOs.Order.Validators
{
    public class OrderCheckoutValidator : AbstractValidator<OrderCheckoutRequest>
    {
        public OrderCheckoutValidator()
        {
            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .Matches(ValidationPatterns.PhoneNumber)
                .WithMessage("Phone number must be 10 digits and start with 0");

            RuleFor(x => x.ShippingAddress)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}