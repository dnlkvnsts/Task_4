using FluentValidation;
using Task_4.DTOs;

namespace Task_4.Validators
{

    public class AuthorValidator : AbstractValidator<AuthorCreateDTO>
    {
        public AuthorValidator()
        {
            RuleFor(a => a.Name)
                .NotEmpty()
                .WithMessage("Name is required")

                .Matches(@"^[a-zA-Zа-яА-Я\s'-]+$")
                .WithMessage("Name can only contain letters")
                .Length(1, 20)
                .WithMessage("Name must be have 1-20 symbols");

            RuleFor(a => a.DateOfBirth)
                .NotEmpty()
                .WithMessage("Date of birth is required")
               .LessThanOrEqualTo(DateTime.Now)
               .WithMessage("Date of birth can not be in the future")
               .When(x => x.DateOfBirth.HasValue);
        }
    }
}
