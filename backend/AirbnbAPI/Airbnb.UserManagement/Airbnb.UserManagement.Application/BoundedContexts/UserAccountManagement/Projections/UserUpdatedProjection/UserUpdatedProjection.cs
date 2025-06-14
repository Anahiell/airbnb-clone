using Airbnb.MongoRepository.Interfaces;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;
using Airbnb.UserManagement.Application.BoundedContexts.UserPermissionManagement.QueryObjects;
using Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.Events;
using Airbnb.UserManagement.Domain.BoundedContexts.UserRoleManagement.Interfaces;
using MediatR;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Projections.UserUpdatedProjection;

public class UserUpdatedProjection : INotificationHandler<UserUpdatedEvent>
{
    private readonly IProjectionRepository<UserEntityInfo> _repository;
    private readonly IRoleRepository _roleRepository;
    private readonly IPermissionRepository _permissionRepository;

    public UserUpdatedProjection(IProjectionRepository<UserEntityInfo> repository, IRoleRepository roleRepository,
        IPermissionRepository permissionRepository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        _roleRepository = roleRepository;
        _permissionRepository = permissionRepository;
    }

    public async Task Handle(UserUpdatedEvent @event, CancellationToken cancellationToken)
    {
        var Roles = new List<Role?>();
        foreach (var userRole in @event.Roles)
        {
            var role = await _roleRepository.GetByIdAsync(userRole.RoleId, cancellationToken);
            if (role != null)
            {
                Roles.Add(new Role{ Id = role.Id, Name = role.Name });
            }
        }

        var Permissions = new List<Permission?>();
        foreach (var userPermission in @event.Permissions)
        {
            var permission = await _permissionRepository.GetByIdAsync(userPermission.PermissionId, cancellationToken);
            if (permission != null)
                Permissions.Add(new Permission{ Id = permission.Id, Name = permission.Permission });
        }
        
        var updatedUser = new UserEntityInfo
        {
            Id = @event.AggregateId,
            FullName = @event.FullName,
            Email = @event.Email,
            IsDocumentVerified = @event.IsDocumentVerified,
            IsEmailVerified = @event.IsEmailVerified,
            ProfileInfo = @event.Profile is null ? null : new ProfileInfo
            {
                School = @event.Profile?.School,
                Location = @event.Profile?.Location,
                Birthdate = @event.Profile?.Birthdate,
                Hobbies = @event.Profile?.Hobbies,
                LifeGoals = @event.Profile?.LifeGoals,
                TimeSpentOn = @event.Profile?.TimeSpentOn,
                Profession = @event.Profile?.Profession,
                FavSong = @event.Profile?.FavSong,
                FunFact = @event.Profile?.FunFact,
                BioTitle = @event.Profile?.BioTitle,
                Pets = @event.Profile?.Pets,
                About = @event.Profile?.About
            },
            Roles = Roles,
            Permissions = Permissions,
            Languages = @event.Languages.Select(l => new Language
            {
                Id = l.LanguageId,
                Name = l.Language.Name
            }).ToList() ?? [],
        };

        await _repository.UpdateAsync(updatedUser, cancellationToken);
    }
}