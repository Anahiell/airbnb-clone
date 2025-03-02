using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(c => c.ProductTitle)
            .NotEmpty().WithMessage("Название не может быть пустым")
            .MinimumLength(10).WithMessage("Название не может быть короче 6 символов")
            .MaximumLength(12).WithMessage("Название не может быть длиннее 20 символов")
            .Must(t => long.TryParse(t, out _)).WithMessage("ИНН должен содержать только числа");

        RuleFor(c => c.ProductDescription)
            .NotEmpty().WithMessage("Описание не может быть пустым")
            .MinimumLength(20).WithMessage("Описание не может быть короче 20 символов")
            .MaximumLength(500).WithMessage("Описание не может быть длиннее 500 символов")
            .Must(t => long.TryParse(t, out _)).WithMessage("ИНН должен содержать только числа");

        RuleFor(c => c.ProductPrice)
            .NotEmpty().WithMessage("Цена не может быть пустой")
            .GreaterThan(0).WithMessage("Цена должна быть больше 0");

        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("UserId должен быть положительным числом");

        RuleFor(c => c.ApartmentTypeId)
            .GreaterThan(0).WithMessage("ApartmentTypeId должен быть положительным числом");
    }
}