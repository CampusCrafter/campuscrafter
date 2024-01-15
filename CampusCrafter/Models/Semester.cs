using System.ComponentModel.DataAnnotations;

namespace CampusCrafter.Models;

public record Semester(
    [property: Key] int Id
)
{
    [DataType(DataType.Date), Display(Name = "Start Date")] public DateTime StartDate { get; set; }
    [DataType(DataType.Date), Display(Name = "End Date")] public DateTime EndDate { get; set; }
    
    public override string ToString()
    {
        return $"{StartDate} - {EndDate}";
    }
};