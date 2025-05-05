using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Airbnb.PictureManagement.Application.BoundedContext.UserPictureManagement.Commands.UpdateUserPictureCommand;

public class UpdateUserPictureCommandValidator : AbstractValidator<UpdateUserPictureCommand>
{
    public UpdateUserPictureCommandValidator()
    {
        RuleFor(c => c.Id)
            .GreaterThan(0).WithMessage("Id должен быть положительным числом");

        RuleFor(c => c.File)
            .NotNull().WithMessage("Файл не может быть пустым")
            .Must(BeAnImage).WithMessage("Файл должен быть изображением (jpeg/png).");
    }

    private bool BeAnImage(IFormFile file)
    {
        var allowedTypes = new[] { "image/jpeg", "image/png", "image/jpg" };
        return allowedTypes.Contains(file.ContentType);
    }
}