using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.UploadUserPictureCommand;

public class UploadUserPictureCommandValidator : AbstractValidator<UploadUserPictureCommand>
{
    public UploadUserPictureCommandValidator()
    {
        RuleFor(c => c.File)
            .NotNull().WithMessage("Файл изображения обязателен.")
            .Must(BeAnImage).WithMessage("Файл должен быть изображением (jpeg/png).");

        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть положительным числом.");
    }

    private bool BeAnImage(IFormFile file)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
        return allowedTypes.Contains(file.ContentType);
    }
}