using FluentValidation;

namespace MultiPayloadHandling.Validators;

public class AppleValidator : AbstractValidator<Apple>
{
    public AppleValidator()
    {
        RuleFor(t => t.AppleName)
            .NotEmpty()
            .MaximumLength(5);
    }
}