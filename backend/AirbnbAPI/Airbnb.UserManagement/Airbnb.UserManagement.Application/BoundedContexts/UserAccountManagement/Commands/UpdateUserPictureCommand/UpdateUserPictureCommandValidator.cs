using FluentValidation;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserPictureCommand;


public class UpdateUserPictureCommandValidator : AbstractValidator<UpdateUserPictureCommand>
{
    public UpdateUserPictureCommandValidator()
    {
        RuleFor(c => c.UserId)
            .GreaterThan(0).WithMessage("ID пользователя должен быть положительным");

        RuleFor(c => c.Picture)
            .NotNull().WithMessage("Фотография обязательна")
            .Must(f => f.Length > 0).WithMessage("Файл фотографии не может быть пустым");
    }
}