using FluentValidation;

namespace Application.Features.Books.Commands.Create;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(c => c.Title).NotEmpty();
        RuleFor(c => c.PublishedDate).NotEmpty();
    }
}