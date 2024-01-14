using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace CampusCrafter.Models;

public class ApplicationUser : IdentityUser
{
    [MaxLength(32)]
    public required string FirstName { get; set; }
    
    [MaxLength(32)]
    public required string LastName { get; set; }
}