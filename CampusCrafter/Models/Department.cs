using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record Department(
    [property: Key] int Id,
    [Required, MaxLength(64)] string Name
);