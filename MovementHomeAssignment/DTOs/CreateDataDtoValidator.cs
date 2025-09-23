using FluentValidation;
namespace MovementHomeAssignment.DTOs;

public class CreateDataDtoValidator : AbstractValidator<CreateDataDto>
{
    public CreateDataDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content cannot be empty.")
            .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters.");
    }
}

public class UpdateDataDtoValidator : AbstractValidator<UpdateDataDto>
{
    public UpdateDataDtoValidator()
    {
        RuleFor(x => x.Content)
            .NotEmpty().WithMessage("Content cannot be empty.")
            .MaximumLength(1000).WithMessage("Content cannot exceed 1000 characters.");
    }
}
