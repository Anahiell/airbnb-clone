using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands.UpdatePictureCommand;

public class UpdateProductImageCommandValidator : AbstractValidator<UpdateProductImageCommand>
{
    public UpdateProductImageCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");
    }
}