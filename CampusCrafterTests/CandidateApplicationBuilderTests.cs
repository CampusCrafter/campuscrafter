using CampusCrafter.Models;

namespace CampusCrafterTests;

public class CandidateApplicationBuilderTests
{
    [Test]
    public void BuildApplicationWithFewFields()
    {
        // Arrange
        const string ApplicantId = "test";
        const int MajorId = 1;
        const StudyType StudyType = StudyType.FullTime;
        
        // Act
        var builder = new CandidateApplicationBuilder
        {
            ApplicantId = ApplicantId,
            MajorId = MajorId,
            StudyType = StudyType
        };
        
        // Assert
        Assert.That(builder.Build(),
            Is.EqualTo(new CandidateApplication { ApplicantId = "test", MajorId = 1, StudyType = StudyType.FullTime }));
    }

    [Test]
    public void BuildEmptyApplication()
    {
        // Arrange

        // Act
        var builder = new CandidateApplicationBuilder();

        // Assert
        Assert.Throws<ArgumentNullException>(() => builder.Build());
    }

}