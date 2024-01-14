using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record Semester(
    [property: Key] int Id,
    DateTime StartDate,
    DateTime EndDate
);