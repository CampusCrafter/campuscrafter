using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;

namespace CampusCrafterTests;

public class SampleData
{
    public static List<ApplicationUser> GetUserData()
    {
        var user = new ApplicationUser
        {
            FirstName = "John",
            LastName = "Test",
            Email = "john@test.com",
            NormalizedEmail = "JOHN@TEST.COM",
            UserName = "JohnTest",
            NormalizedUserName = "JOHNTEST",
            PhoneNumber = "+1111111111",
            EmailConfirmed = true,
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString("D"),
        };

        var password = new PasswordHasher<ApplicationUser>();
        var hashed = password.HashPassword(user, "Password1!");
        user.PasswordHash = hashed;

        return [user];
    }
}