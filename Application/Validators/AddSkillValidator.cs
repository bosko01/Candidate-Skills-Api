using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public sealed class AddSkillsDtoValidator : AbstractValidator<AddSkillsDto>
{
    public AddSkillsDtoValidator()
    {
        RuleFor(x => x.Skills)
            .NotNull().WithMessage("Skills list is required.")
            .Must(list => list.Count > 0).WithMessage("At least one skill is required.");

        RuleForEach(x => x.Skills)
            .NotEmpty().WithMessage("Skill name cannot be empty.")
            .MaximumLength(100);
    }
}
