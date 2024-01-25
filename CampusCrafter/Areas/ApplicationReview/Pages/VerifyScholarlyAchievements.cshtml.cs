using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.ApplicationReview.Pages;

public class VerifyScholarlyAchievements(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : PageModel
{
    public CandidateApplication Application { get; set; } = default!;
    public ApplicationUser ApplicantUser { get; set; } = default!;

    public List<CandidateApplication> OtherApplications { get; set; } = default!;

    [BindProperty] public List<ScholarlyAchievement> ScholarlyAchievements { get; set; } = default!;

    private async Task<bool> PrepareDataAsync(int applicationId)
    {
        var application = await context.CandidateApplications
            .Include(a => a.Applicant)
            .ThenInclude(a => a.ScholarlyAchievements.OrderBy(s => s.Type))
            .FirstOrDefaultAsync(a => a.Id == applicationId);
        if (application is null)
            return false;

        OtherApplications = await context.CandidateApplications
            .Include(a => a.Applicant)
            .Include(a => a.Major)
            .Where(a => a.Applicant.UserId == application.Applicant.UserId && a.Id != application.Id)
            .ToListAsync();

        var user = await userManager.FindByNameAsync(application.Applicant.UserId);
        if (user is null)
            return false;  // TODO: Handle users who deleted their accounts but their applications are still in the system

        ApplicantUser = user;
        Application = application;
        ScholarlyAchievements = Application.Applicant.ScholarlyAchievements;
        return true;
    } 
    
    public async Task<IActionResult> OnGetAsync(int applicationId)
    {
        var success = await PrepareDataAsync(applicationId);

        if (!success)
            return NotFound();
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int applicationId)
    {
        if (!ModelState.IsValid)
        {
            var success = await PrepareDataAsync(applicationId);
            if (!success) return NotFound();
            return Page();
        }

        var application = await context.CandidateApplications
            .Include(candidateApplication => candidateApplication.Applicant)
            .ThenInclude(a => a.ScholarlyAchievements)
            .FirstOrDefaultAsync(a => a.Id == applicationId);
        if (application is null)
            return NotFound();
        
        application.Applicant.ScholarlyAchievements = ScholarlyAchievements;
        context.CandidateApplications.Update(application);
        await context.SaveChangesAsync();
        return RedirectToPage("./ViewApplication", new { applicationid = applicationId });
    }
}