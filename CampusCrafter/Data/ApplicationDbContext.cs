using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    public DbSet<Candidate> Candidates { get; set; }
    public DbSet<Progress> Progresses { get; set; }
    public DbSet<ScholarlyAchievement> ScholarlyAchievements { get; set; }
    
    public DbSet<Major> Majors { get; set; }
    public DbSet<Semester> Semesters { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Department> Departments { get; set; }
    
    public DbSet<CandidateApplication> CandidateApplications { get; set; }
    
    public DbSet<StudyPlan> StudyPlans { get; set; }
    public DbSet<AcceptanceCriteria> AcceptanceCriteria { get; set; }
    public DbSet<ScoreWeight> ScoreWeights { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Major>()
            .HasOne(m => m.ParentMajor)
            .WithOne();
        
        base.OnModelCreating(modelBuilder);
    }
}