using CampusCrafter.Data;
using CampusCrafter.Models;

namespace CampusCrafterTests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        using var db = new ApplicationDbContext(Utils.GetDbContextOptions());
        
        var semester = new Semester(1) { StartDate = DateTime.Today, EndDate = DateTime.Today.AddDays(7) };
        db.Semesters.Add(semester);
        db.Courses.Add(new Course(1) { Name = "TestCourse", Semester = semester });
        db.SaveChanges();
            
        Assert.That(db.Courses.FirstOrDefault(c => c.Name == "TestCourse"), Is.Not.Null);
    }
}