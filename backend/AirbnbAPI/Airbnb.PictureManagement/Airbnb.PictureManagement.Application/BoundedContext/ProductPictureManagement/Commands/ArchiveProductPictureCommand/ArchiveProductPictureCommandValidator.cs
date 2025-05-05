using FluentValidation;

namespace Airbnb.PictureManagement.Application.BoundedContext.ProductPictureManagement.Commands.ArchiveProductPictureCommand;

public class ArchiveProductPictureCommandValidator : AbstractValidator<ArchiveProductPictureCommand>
{
    public ArchiveProductPictureCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id картинки продукта должен быть положительным числом");
    }
}