using Airbnb.Application.Messaging;
using Airbnb.Application.Results;
using Airbnb.SharedKernel.Repositories;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Interfaces;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.ValueObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Aggregates;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserPermission;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Events.UserRole;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using Airbnb.UserManagement.Infrastructure.Repositories;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserCommand;

public class UpdateUserCommandHandler : ICommandHandler<UpdateUserCommand, Result<string>>
{
    private readonly IRepository<DomainUser> _userRepository;
    private readonly ILanguageRepository _languageRepository;
    private readonly IUserLanguageRepository _userLanguageRepository;
    
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRoleRepository _userRoleRepository;
    
    private readonly IPermissionRepository _permissionRepository;
    private readonly IUserPermissionRepository _userPermissionRepository;
    
    private readonly IMediator _mediator;

    public UpdateUserCommandHandler(IRepository<DomainUser> userRepository, IMediator mediator, ILanguageRepository languageRepository, IUserLanguageRepository userLanguageRepository, IRoleRepository roleRepository, IUserRoleRepository userRoleRepository, IPermissionRepository permissionRepository, IUserPermissionRepository userPermissionRepository)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _languageRepository = languageRepository;
        _userLanguageRepository = userLanguageRepository;
        _roleRepository = roleRepository;
        _userRoleRepository = userRoleRepository;
        _permissionRepository = permissionRepository;
        _userPermissionRepository = userPermissionRepository;
    }

    public async Task<Result<string>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
        if (user == null)
        {
            return Result<string>.Failure("Пользователь не найден");
        }
        
        var profile = request.Profile is not null
            ? new UserProfile(
                request.Profile.School,
                request.Profile.Location,
                request.Profile.Hobbies,
                request.Profile.LifeGoals,
                request.Profile.TimeSpentOn,
                request.Profile.Profession,
                request.Profile.FavSong,
                request.Profile.FunFact,
                request.Profile.BioTitle,
                request.Profile.Pets,
                request.Profile?.About)
            : null;

        user.UpdateProfile(profile);
        
        var languageNames = request.Languages ?? Enumerable.Empty<string?>();
        var existingLanguages = new List<DomainLanguage>();

        foreach (var name in languageNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var language = await _languageRepository.GetByNameAsync(name, cancellationToken);
            if (language is not null)
            {
                existingLanguages.Add(language);
            }
        }

        var missing = languageNames.Except(existingLanguages.Select(l => l.Name)).ToList();
        if (missing.Any())
        {
            return Result<string>.Failure($"Следующие языки не найдены в системе: {string.Join(", ", missing)}");
        }
        
        user.UpdateLanguages(existingLanguages.Select(l => l.Id));
        
        await _userLanguageRepository.DeleteAllByUserIdAsync(user.Id, cancellationToken);

        foreach (var language in existingLanguages)
        {
            var userLanguage = new DomainUserLanguage(user.Id, language.Id);
            await _userLanguageRepository.AddAsync(userLanguage, cancellationToken);
        }
        
        // Роли 
        
        var roleNames = request.Roles ?? Enumerable.Empty<string?>();
        var existingRoles = new List<DomainRole>();

        foreach (var name in roleNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var role = await _roleRepository.GetByNameAsync(name, cancellationToken);
            if (role is not null)
            {
                existingRoles.Add(role);
            }
        }

        var missingRoles = roleNames.Except(existingRoles.Select(r => r.Name)).ToList();
        if (missingRoles.Any())
        {
            return Result<string>.Failure($"Следующие роли не найдены в системе: {string.Join(", ", missingRoles)}");
        }

        await _userRoleRepository.DeleteAllByUserIdAsync(user.Id, cancellationToken);

        
        foreach (var role in existingRoles)
        {
            var userRole = new DomainUserRole(user.Id, role.Id);
            await _userRoleRepository.AddAsync(userRole, cancellationToken);
        }
        
        // Права
        
        var permissionNames = request.Permissions ?? Enumerable.Empty<string?>();
        var existingPermissions = new List<DomainPermission>();

        foreach (var name in permissionNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var permission = await _permissionRepository.GetByNameAsync(name, cancellationToken);
            if (permission is not null)
            {
                existingPermissions.Add(permission);
            }
        }

        var missingPermissions = permissionNames.Except(existingPermissions.Select(p => p.Permission)).ToList();
        if (missingPermissions.Any())
        {
            return Result<string>.Failure($"Следующие права не найдены в системе: {string.Join(", ", missingPermissions)}");
        }

        await _userPermissionRepository.DeleteAllByUserIdAsync(user.Id, cancellationToken);

        foreach (var permission in existingPermissions)
        {
            var userPermission = new DomainUserPermission(user.Id, permission.Id);
            await _userPermissionRepository.AddAsync(userPermission, cancellationToken);
        }

        user.UpdateUser(request.FullName, request.Email, request.DateOfBirth);

        await _userRepository.UpdateAsync(user, cancellationToken);

        await _mediator.Publish(new UserUpdatedEvent(user.Id, user.FullName, user.Email, user.DateOfBirth, profile, user.UserRoles.ToList(), user.UserPermissions.ToList(), user.Languages.ToList()), cancellationToken);

        await _mediator.Publish(new UserLanguagesUpdatedEvent(user.Id, existingLanguages.Select(x => x.Name).ToList()), cancellationToken);
        
        await _mediator.Publish(new UserRolesUpdatedEvent(user.Id, existingRoles.Select(x => x.Name).ToList()), cancellationToken);
        
        await _mediator.Publish(new UserPermissionsUpdatedEvent(user.Id, existingPermissions.Select(x => x.Permission).ToList()), cancellationToken);

        return Result<string>.Success("Пользователь успешно обновлен");
    }
}