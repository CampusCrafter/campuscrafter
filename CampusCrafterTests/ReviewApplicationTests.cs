using System.Runtime.InteropServices;
using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace CampusCrafterTests;

public class ReviewApplicationTests
{
    private DbContextOptions<ApplicationDbContext> options;
    private Mock<UserManager<ApplicationUser>> mockedUserManager;
    
    private async Task SeedApplicationData(ApplicationRepository repository)
    {
        mockedUserManager = Mocks.MockUserManager(SampleData.GetUserData());
        
        Candidate applicant = new()
        {
            UserId = "JohnTest",
            Progresses = [
                new Progress(ProgressType.MaturaBasicMath, 96),
                new Progress(ProgressType.MaturaBasicPolish, 60),
                new Progress(ProgressType.MaturaExtendedMath, 60),
                new Progress(ProgressType.MaturaExtendedPhysics, 90),
            ],
            ScholarlyAchievements = [
                new ScholarlyAchievement { Type = ScholarlyAchievementType.Research, Description = "Blah blah blah research", Score = null },
                new ScholarlyAchievement { Type = ScholarlyAchievementType.ScientificArticle, Description = "Blah blah blah scientific article", Score = null },
                new ScholarlyAchievement { Type = ScholarlyAchievementType.Other, Description = "Blah blah blah other", Score = null },
            ],
            CompletedMajors = []
        };

        AcceptanceCriteria acceptanceCriteria = new()
        {
            MaxStudents = 100,
            ScoreWeights = [
                new ScoreWeight { ProgressType = ProgressType.MaturaBasicMath, Weight = 2.0m },
                new ScoreWeight { ProgressType = ProgressType.MaturaBasicPolish, Weight = 0.1m },
                new ScoreWeight { ProgressType = ProgressType.MaturaExtendedMath, Weight = 2.0m },
                new ScoreWeight { ProgressType = ProgressType.MaturaExtendedCompSci, Weight = 2.5m },
            ],
        };

        StudyPlan studyPlan = new()
        {
            Name = "Test Major Study Plan",
            Language = "pl",
            AcceptanceCriteria = acceptanceCriteria,
        };

        Major major = new()
        {
            Name = "Test Major",
            Department = null,
            Prerequisites = [],
            Specializations = [],
            Courses = [],
            StudyPlans = [studyPlan]
        };
        studyPlan.Major = major;

        CandidateApplication application = new()
        {
            Applicant = applicant,
            StudyType = StudyType.FullTime,
            Major = major
        };
        
        repository.Add(application);
        await repository.SaveChangesAsync();
    }
    
    [SetUp]
    public async Task Setup()
    {
        options = Utils.GetDbContextOptions();

        await using var db = new ApplicationDbContext(options);
        var repo = new ApplicationRepository(db);
        await SeedApplicationData(repo);
    }

    [Test]
    public async Task WhenAllScholarlyAchievementsAreScored_ThenApplicationCanBeAccepted()
    {
        // Arrange
        await using var db = new ApplicationDbContext(options);
        var repo = new ApplicationRepository(db);

        var application = (await repo.GetAllAsync<CandidateApplication>(q => q
            .Include(a => a.Applicant)
            .ThenInclude(a => a.ScholarlyAchievements)
        )).FirstOrDefault();
        Assert.That(application, Is.Not.Null);

        var achievements = application.Applicant.ScholarlyAchievements;
        foreach (var achievement in achievements) repo.AssignScoreToScholarlyAchievement(achievement, 1);
        
        // Act
        var success = repo.AcceptApplication(application);
        await repo.SaveChangesAsync();
        
        // Assert
        Assert.That(success, Is.True);
        Assert.That(application.Status, Is.EqualTo(CandidateApplicationStatus.Accepted));
    }
    
    [Test]
    public async Task WhenNotAllScholarlyAchievementsAreScored_ThenApplicationCannotBeAccepted()
    {
        // Arrange
        await using var db = new ApplicationDbContext(options);
        var repo = new ApplicationRepository(db);

        var application = (await repo.GetAllAsync<CandidateApplication>(q => q
            .Include(a => a.Applicant)
            .ThenInclude(a => a.ScholarlyAchievements)
        )).FirstOrDefault();
        Assert.That(application, Is.Not.Null);

        var achievements = application.Applicant.ScholarlyAchievements;
        for (var i = 0; i < achievements.Count - 1; i++) repo.AssignScoreToScholarlyAchievement(achievements[i], 1);

        // Act
        var success = repo.AcceptApplication(application);
        await repo.SaveChangesAsync();
        
        // Assert
        Assert.Multiple(() =>
        {
            Assert.That(success, Is.False);
            Assert.That(application.Status, Is.EqualTo(CandidateApplicationStatus.Submitted));
        });
    }
}