using Airbnb.SharedKernel;
using Airbnb.UserManagement.Domain.BoundedContexts.LanguageManagement.Aggregates;

namespace Airbnb.UserManagement.Domain.BoundedContexts.UserAccountManagement.ValueObjects;

public class UserProfile : ValueObject
{
    public string? School { get; private set; }
    public string? Location { get; private set; }
    public DateTime? Birthdate { get; private set; }
    public string? Hobbies { get; private set; }
    public string? LifeGoals { get; private set; }
    public string? TimeSpentOn { get; private set; }
    public string? Profession { get; private set; }
    public string? FavSong { get; private set; }
    public string? FunFact { get; private set; }
    public string? BioTitle { get; private set; }
    public string? Pets { get; private set; }
    public string? About { get; private set; }

    private UserProfile() { }

    public UserProfile(
        string? school,
        string? location,
        string? hobbies,
        string? lifeGoals,
        string? timeSpentOn,
        string? profession,
        string? favSong,
        string? funFact,
        string? bioTitle,
        string? pets,
        string? about)
    {
        School = school;
        Location = location;
        Hobbies = hobbies;
        LifeGoals = lifeGoals;
        TimeSpentOn = timeSpentOn;
        Profession = profession;
        FavSong = favSong;
        FunFact = funFact;
        BioTitle = bioTitle;
        Pets = pets;
        About = about;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return School;
        yield return Location;
        yield return Birthdate;
        yield return Hobbies;
        yield return LifeGoals;
        yield return TimeSpentOn;
        yield return Profession;
        yield return FavSong;
        yield return FunFact;
        yield return BioTitle;
        yield return Pets;
        yield return About;
    }
}