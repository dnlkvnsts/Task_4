using FluentValidation;
using Task_4.DTOs;

namespace Task_4.Validators
{
    public class BookValidator : AbstractValidator<BookCreateDTO>
    {
        public BookValidator()
        {
            RuleFor(b => b.Title)
               .NotEmpty()
               .WithMessage("Title is required")
               .Length(1, 20)
               .WithMessage("Title must be 1-20 characters")
               .Matches(@"^[a-zA-Zа-яА-Я0-9\s':,-]+$")
               .WithMessage("Title can contain only letters");

            RuleFor(b => b.PublishedYear)
                .NotEmpty()
                .WithMessage("Published year is required")
                .InclusiveBetween(1000, DateTime.Now.Year)
                .WithMessage($"Published year must be from 1000 to {DateTime.Now.Year}");

            RuleFor(b => b.AuthorId)
                .NotEmpty()
                .WithMessage("Author ID is required")
                .GreaterThan(0)
                .WithMessage("Author ID must be positive");
        }
    }
}
