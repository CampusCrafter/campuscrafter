using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.ApplicationReview.Pages;

public class ViewApplication(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : PageModel
{
    [BindProperty] public CandidateApplication Application { get; set; } = default!;
    [BindProperty] public ApplicationUser ApplicantUser { get; set; } = default!;

    [BindProperty] public List<CandidateApplication> OtherApplications { get; set; } = default!;
    
    public async Task<IActionResult> OnGetAsync(int applicationId)
    {
        var application = await context.CandidateApplications
            .Include(a => a.Applicant)
                .ThenInclude(a => a.Progresses.OrderBy(p => p.Type))
            .Include(a => a.Applicant)
                .ThenInclude(a => a.ScholarlyAchievements.OrderBy(s => s.Type))
            .Include(a => a.Major)
            .ThenInclude(m => m.StudyPlans)
                .ThenInclude(s => s.AcceptanceCriteria)
                    .ThenInclude(s => s.ScoreWeights)
            .FirstOrDefaultAsync(a => a.Id == applicationId);
        if (application is null)
            return NotFound();

        OtherApplications = await context.CandidateApplications
            .Include(a => a.Applicant)
            .Include(a => a.Major)
            .Where(a => a.Applicant.UserId == application.Applicant.UserId && a.Id != application.Id)
            .ToListAsync();

        var user = await userManager.FindByNameAsync(application.Applicant.UserId);
        if (user is null)
            return NotFound();  // TODO: Handle users who deleted their accounts but their applications are still in the system

        ApplicantUser = user;
        Application = application;
        
        return Page();
    }
}