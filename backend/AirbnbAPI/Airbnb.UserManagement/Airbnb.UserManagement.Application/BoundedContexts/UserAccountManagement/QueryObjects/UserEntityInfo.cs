using Airbnb.MongoRepository.Entities;
using Newtonsoft.Json;

namespace Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.QueryObjects;

public class UserEntityInfo : IQueryEntity
{
    public string FullName { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    
    public bool IsEmailVerified { get; set; }
    
    public bool IsDocumentVerified { get; set; }
    public PictureInfo Url { get; set; }
    public ProfileInfo? ProfileInfo { get; set; }
    public List<Role?>? Roles { get; set; }
    public List<Language> Languages { get; set; }
    public List<Permission?>? Permissions { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    
    public void UpdateRole(Role newRole)
    {
        Roles ??= new List<Role?>();
        var existing = Roles.FirstOrDefault(r => r != null && r.Id == newRole.Id);

        if (existing != null)
        {
            existing.Name = newRole.Name;
        }
        else
        {
            Roles.Add(newRole);
        }
    }

    public void UpdateLanguage(Language newLanguage)
    {
        Languages ??= new List<Language>();
        var existing = Languages.FirstOrDefault(l => l.Id == newLanguage.Id);

        if (existing != null)
        {
            existing.Name = newLanguage.Name;
        }
        else
        {
            Languages.Add(newLanguage);
        }
    }

    public void UpdatePermission(Permission newPermission)
    {
        Permissions ??= new List<Permission?>();
        var existing = Permissions.FirstOrDefault(p => p != null && p.Id == newPermission.Id);

        if (existing != null)
        {
            existing.Name = newPermission.Name;
        }
        else
        {
            Permissions.Add(newPermission);
        }
    }
}

public class PictureInfo
{
    public string Url { get; set; }
    [JsonIgnore]
    public int UserId { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; }
    [JsonIgnore]
    public int Id { get; set; }
}

public class ProfileInfo
{
    public string? School { get; set; }
    public string? Location { get; set; }
    public DateTime? Birthdate { get; set; }
    public string? Hobbies { get; set; }
    public string? LifeGoals { get; set; }
    public string? TimeSpentOn { get; set; }
    public string? Profession { get; set; }
    public string? FavSong { get; set; }
    public string? FunFact { get; set; }
    public string? BioTitle { get; set; }
    public string? Pets { get; set; }
    public string? About { get; set; }
}

public class UserLanguages
{
    public List<Language?>? Languages { get; private set; }
}

public class Language
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class UserPermissions
{
    public List<Permission?>? Permissions { get; private set; }
}

public class Permission
{
    public int Id { get; set; }
    public string? Name { get; set; }
}

public class UserRoles
{
    public List<Role?>? Roles { get; private set; }
}

public class Role
{
    public int Id { get; set; }
    public string? Name { get; set; }
}