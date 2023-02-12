using FluentValidation;

namespace MultiPayloadHandling.Validators;

public class PearValidator : AbstractValidator<Pear>
{
    public PearValidator()
    {
        RuleFor(t => t.PearName)
            .NotEmpty()
            .MaximumLength(5);
    }
}