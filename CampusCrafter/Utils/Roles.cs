namespace CampusCrafter.Utils;

public enum Role
{
    Admin,
    Department,
    Candidate
}

public static class Roles
{
    private static readonly Dictionary<Role, string> RoleNames = new()
    {
        [Role.Admin] = "Admin",
        [Role.Department] = "Department",
        [Role.Candidate] = "Candidate",
    };

    public static string GetRoleName(Role role) => RoleNames[role];
}