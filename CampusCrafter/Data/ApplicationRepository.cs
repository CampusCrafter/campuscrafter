using System.Linq.Expressions;
using CampusCrafter.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Data;

public class ApplicationRepository(ApplicationDbContext context) : IRepository
{
    public ApplicationDbContext Context => context;
    
    public async Task<List<T>> GetAllAsync<T>(Func<IQueryable<T>, IQueryable<T>>? modifier = null) where T : class
    {
        var set = context.Set<T>();
        var modifiedSet = modifier is null ? set : modifier(set);
        return await modifiedSet.ToListAsync();
    }

    public async Task<T?> GetAsync<T>(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IQueryable<T>>? modifier = null) where T : class
    {
        var set = context.Set<T>();
        var modifiedSet = modifier is null ? set : modifier(set);
        return await modifiedSet.FirstOrDefaultAsync(filter);
    }

    public void Add<T>(T value) where T : class
    {
        context.Set<T>().Add(value);
    }

    public void Update<T>(T value) where T : class
    {
        context.Set<T>().Update(value);
    }

    public void Delete<T>(T value) where T : class
    {
        context.Set<T>().Remove(value);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public void AssignScoreToScholarlyAchievement(ScholarlyAchievement achievement, decimal score)
    {
        achievement.Score = score;
        Update(achievement);
    }

    private bool UpdateApplicationStatus(CandidateApplication application, CandidateApplicationStatus status, string? rejectReason = null)
    {
        var alreadyHandled =
            application.Status is CandidateApplicationStatus.Accepted or CandidateApplicationStatus.Rejected;
        var notAllScholarlyAchievementsHandled =
            application.Applicant.ScholarlyAchievements.Any(s => s.Score is null);

        switch (status)
        {
            case CandidateApplicationStatus.Accepted:
                if (alreadyHandled || notAllScholarlyAchievementsHandled)
                    return false;
                application.Status = CandidateApplicationStatus.Accepted;
                break;
            case CandidateApplicationStatus.Rejected:
                if (rejectReason?.Length == 0)
                    throw new ArgumentException("Reject reason cannot be empty");
                application.RejectReason = rejectReason;
                application.Status = CandidateApplicationStatus.Rejected;
                break;
            default: throw new NotSupportedException();
        }
        Update(application);

        return true;
    }

    public bool AcceptApplication(CandidateApplication application) =>
        UpdateApplicationStatus(application, CandidateApplicationStatus.Accepted);

    public bool RejectApplication(CandidateApplication application, string rejectReason) =>
        UpdateApplicationStatus(application, CandidateApplicationStatus.Rejected, rejectReason);

    public async Task<List<Major>> MajorsWithIdsAsync(IEnumerable<int> ids)
    {
        List<Major> result = [];
        
        foreach (var i in ids)
        {
            var major = await GetAsync<Major>(m => m.Id == i, q => q
                .Include(m => m.StudyPlans)
                .ThenInclude(s => s.AcceptanceCriteria)
                .ThenInclude(a => a.ScoreWeights)
                .Include(m => m.Department)
                .Include(m => m.ParentMajor)
                .Include(m => m.Specializations)
                .Include(m => m.Courses)
                .Include(m => m.Prerequisites)
            );
            if (major != null)
            {
                result.Add(major);
            }
        }
        return result;
    }

    public async Task<List<Major>> MajorsPrerequisitesFiltered(bool hasAny)
    {
        var result = await GetAllAsync<Major>(q => q
            .Where(m => m.Prerequisites.Count == 0 == hasAny) 
            // for second degree number of prerequisites can't be 0
            // for first degree number of prerequisites must be 0
            .Include(m => m.Department)
            .Include(m => m.Prerequisites)
            .Include(m => m.Specializations)
            .Include(m => m.StudyPlans)
        );
        return result;
    }

    public async Task<List<List<ScoreWeight>>> ScoreWeightsFromStudyPlansIds(IEnumerable<int> ids)
    {
        List<List<ScoreWeight>> result = [];
        foreach (var id in ids)
        {
            var s = await GetAsync<StudyPlan>(q => q.Id == id, q => q
                .Include(s => s.AcceptanceCriteria)
                .ThenInclude(a => a.ScoreWeights)
            );
            result.Add(s!.AcceptanceCriteria.ScoreWeights);
        }

        return result;
    }
}