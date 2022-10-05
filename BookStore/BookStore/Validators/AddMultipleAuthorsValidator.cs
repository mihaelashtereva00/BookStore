using BookStore.Models.Requests;
using FluentValidation;

namespace BookStore.Validators
{
    public class AddMultipleAuthorsValidator : AbstractValidator<AddMultipleAuthorsRequest>
    {
        public AddMultipleAuthorsValidator()
        {
            RuleFor(x => x.Reason)
                .NotNull()
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50);

            RuleFor(x => x.AuthorRequests)
                .Must(a => a.Count() >= 1)
                .WithMessage("count is less than 1");
        }
    }
}
