using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.ValueObjects;
using Swashbuckle.AspNetCore.Annotations;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserCommand;

[SwaggerSchema("Информация о профиле пользователя")]
public class UserProfileDto
{
    [SwaggerSchema("Школа пользователя")]
    public string? School { get; set; }

    [SwaggerSchema("Город или локация")]
    public string? Location { get; set; }

    [SwaggerSchema("Хобби пользователя")]
    public string? Hobbies { get; set; }

    [SwaggerSchema("Жизненные цели")]
    public string? LifeGoals { get; set; }

    [SwaggerSchema("На что тратит время")]
    public string? TimeSpentOn { get; set; }

    [SwaggerSchema("Профессия")]
    public string? Profession { get; set; }

    [SwaggerSchema("Любимая песня")]
    public string? FavSong { get; set; }

    [SwaggerSchema("Интересный факт")]
    public string? FunFact { get; set; }

    [SwaggerSchema("Заголовок биографии")]
    public string? BioTitle { get; set; }

    [SwaggerSchema("Домашние животные")]
    public string? Pets { get; set; }

    [SwaggerSchema("Описание о себе")]
    public string? About { get; set; }
}
