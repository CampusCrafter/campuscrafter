using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.ApplicationReview.Pages;

public class ViewApplication(ApplicationDbContext context, UserManager<ApplicationUser> userManager) : PageModel
{
    public CandidateApplication Application { get; set; } = default!;
    public ApplicationUser ApplicantUser { get; set; } = default!;

    public List<CandidateApplication> OtherApplications { get; set; } = default!;

    [BindProperty] public string RejectReason { get; set; } = default!;

    private async Task<bool> PrepareDataAsync(int applicationId)
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
        return true;
    }
    
    public async Task<IActionResult> OnGetAsync(int applicationId)
    {
        if (!await PrepareDataAsync(applicationId))
            return NotFound();
        return Page();
    }

    public async Task<IActionResult> OnPostAsync(int applicationId, string actionType)
    {
        // Skip validating RejectReason if we're accepting an application
        if (!ModelState.IsValid && actionType == "reject" && ModelState["RejectReason"].ValidationState == ModelValidationState.Invalid)
        {
            if (!await PrepareDataAsync(applicationId))
                return NotFound();
            return Page();
        }

        var application = await context.CandidateApplications.FirstOrDefaultAsync(a => a.Id == applicationId);
        if (application is null)
            return NotFound();

        switch (actionType.Trim().ToLowerInvariant())
        {
            case "reject":
                application.RejectReason = RejectReason;
                application.Status = CandidateApplicationStatus.Rejected;
                break;
            case "accept":
                application.Status = CandidateApplicationStatus.Accepted;
                break;
        }

        context.CandidateApplications.Update(application);
        await context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}