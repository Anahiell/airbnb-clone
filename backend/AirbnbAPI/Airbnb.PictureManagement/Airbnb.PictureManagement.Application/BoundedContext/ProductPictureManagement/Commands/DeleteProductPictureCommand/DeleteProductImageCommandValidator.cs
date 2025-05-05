using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.DeletePictureCommand;

public class DeleteProductImageCommandValidator : AbstractValidator<DeleteProductImageCommand>
{
    public DeleteProductImageCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}