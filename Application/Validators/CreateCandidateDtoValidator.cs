using Application.DTOs;
using FluentValidation;

namespace Application.Validators;

public sealed class CreateCandidateDtoValidator : AbstractValidator<CreateCandidateDto>
{
    public CreateCandidateDtoValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("First name is required.")
            .MaximumLength(100);

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100);

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email format is invalid.")
            .MaximumLength(255);

        RuleFor(x => x.PhoneNumber)
            .Must(phone => string.IsNullOrWhiteSpace(phone) || PhoneHelper.IsValid(phone, "RS"))
            .WithMessage("Phone number is invalid. Use a valid Serbian number (e.g. +3816xxxxxx).");

        
    }


}

