using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CampusCrafter.Models;

public class ApplicationUser : IdentityUser
{
    public required string FirstName { get; set; }
    
    public required string LastName { get; set; }
}