using Airbnb.IntegrationTesting.Generators;
using Airbnb.UserManagement.Application.BoundedContexts.UserAccountManagement.Commands.UpdateUserCommand;

namespace Airbnb.UserManagement.IntegrationTestings.UserManagement.UpdateUser;

public class UpdateUserCommandGenerator : DataGenerator<UpdateUserCommand>
{
    protected override IEnumerable<UpdateUserCommand> GetData()
    {
        yield return new UpdateUserCommand
        {
            Id = 1,
            FullName = "User 1 Updated",
            Email = "user1.updated@example.com",
            Roles = new List<string?> { "Guest", "Admin" },
            Permissions = new List<string?> { "CanUseAdminController" },
            Languages = new List<string?> { "English" },
            DateOfBirth = new DateTime(1990, 1, 1),
            Profile = new UserProfileDto
            {
                School = "School 1",
                Location = "City 1",
                Hobbies = "Hiking",
                LifeGoals = "Grow",
                TimeSpentOn = "Tech",
                Profession = "Developer",
                FavSong = "Imagine",
                FunFact = "Loves chess",
                BioTitle = "Engineer",
                Pets = "None",
                About = "Bio 1"
            }
        };

        yield return new UpdateUserCommand
        {
            Id = 2,
            FullName = "User 2 Updated",
            Email = "user2.updated@example.com",
            Roles = new List<string?> { "Guest" },
            Permissions = new List<string?> { "CanAnswerSupportRequest" },
            Languages = new List<string?> { "Ukraine" },
            DateOfBirth = new DateTime(1995, 5, 5),
            Profile = new UserProfileDto
            {
                School = "School 2",
                Location = "City 2",
                Hobbies = "Reading",
                LifeGoals = "Explore",
                TimeSpentOn = "Design",
                Profession = "Designer",
                FavSong = "Bohemian Rhapsody",
                FunFact = "Plays violin",
                BioTitle = "Artist",
                Pets = "Cat",
                About = "Bio 2"
            }
        };
    }
}