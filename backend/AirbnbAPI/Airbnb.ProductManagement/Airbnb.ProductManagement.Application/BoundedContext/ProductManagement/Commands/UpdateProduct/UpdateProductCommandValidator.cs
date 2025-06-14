using FluentValidation;

namespace Airbnb.ProductManagement.Application.BoundedContext.Commands;

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("ID продукта должен быть положительным числом.");

        RuleFor(c => c.ProductTitle)
            .NotEmpty().WithMessage("Название продукта не может быть пустым.")
            .MaximumLength(100).WithMessage("Название продукта не может превышать 100 символов.");

        RuleFor(c => c.ProductDescription)
            .MaximumLength(500).WithMessage("Описание продукта не может превышать 500 символов.");

        RuleFor(c => c.ProductPrice)
            .GreaterThan(0).WithMessage("Цена продукта должна быть положительным числом.");

        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть положительным числом.");

        RuleFor(c => c.ApartmentTypeId)
            .GreaterThan(0).WithMessage("ID типа апартаментов должен быть положительным числом.");

        RuleFor(c => c.AddressLegalId)
            .GreaterThan(0).WithMessage("ID юридического адреса должен быть положительным числом.");
    }
}