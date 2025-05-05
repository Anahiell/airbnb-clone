using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Airbnb.PictureManagement.Application.BoundedContext.Commands;

public class UploadProductImageCommandValidator : AbstractValidator<UploadProductPictureCommand>
{
    public UploadProductImageCommandValidator()
    {
        RuleFor(c => c.Files)
            .NotEmpty().WithMessage("Необходимо загрузить хотя бы один файл.");

        RuleForEach(c => c.Files)
            .Must(BeAnImage).WithMessage("Файл должен быть изображением (jpeg/png).");

        RuleFor(c => c.ProductId)
            .GreaterThan(0).WithMessage("Должен быть указан валидный ProductId.");
    }

    private bool BeAnImage(IFormFile file)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
        return allowedTypes.Contains(file.ContentType);
    }
}