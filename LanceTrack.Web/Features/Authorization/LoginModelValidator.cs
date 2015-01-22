using FluentValidation;
using Rx = LanceTrack.Web.Features.Shared.ValidationMessages;

namespace LanceTrack.Web.Features.Authorization
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        public LoginModelValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(e => e.Login)
                .NotEmpty().WithLocalizedMessage(() => Rx.Required);

            RuleFor(e => e.Password)
                .NotEmpty().WithLocalizedMessage(() => Rx.Required);
        }
    }
}