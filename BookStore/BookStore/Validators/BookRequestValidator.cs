using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validators
{
    public class BookRequestValidator : AbstractValidator<BookRequest>
    {
        public BookRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotNull()
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(x => x.AuthorId)
                .NotNull()
                .GreaterThan(0);

            RuleFor(x => x.Id)
                .NotNull()
                .GreaterThan(0);
        }
    }
}
