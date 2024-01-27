using System.ComponentModel.DataAnnotations;
using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.ApplicationReview.Pages;

public class ViewApplication(ApplicationRepository repository, UserManager<ApplicationUser> userManager) : PageModel
{
    public CandidateApplication Application { get; set; } = default!;
    public ApplicationUser ApplicantUser { get; set; } = default!;

    public List<CandidateApplication> OtherApplications { get; set; } = default!;

    [BindProperty, Required] public string RejectReason { get; set; } = default!;

    private async Task<bool> PrepareDataAsync(int applicationId)
    {
        var application = await repository.GetAsync<CandidateApplication>(a => a.Id == applicationId, q => q
            .Include(a => a.Applicant)
            .ThenInclude(a => a.Progresses.OrderBy(p => p.Type))
            .Include(a => a.Applicant)
            .ThenInclude(a => a.ScholarlyAchievements.OrderBy(s => s.Type))
            .Include(a => a.Major)
            .ThenInclude(m => m.StudyPlans)
            .ThenInclude(s => s.AcceptanceCriteria)
            .ThenInclude(s => s.ScoreWeights)
        );
        if (application is null)
            return false;

        OtherApplications = await repository.GetAllAsync<CandidateApplication>(q => q
            .Include(a => a.Applicant)
            .Include(a => a.Major)
            .Where(a => a.Applicant.UserId == application.Applicant.UserId && a.Id != application.Id)
        );

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
        if (!ModelState.IsValid && actionType == "reject" && ModelState["RejectReason"]?.ValidationState == ModelValidationState.Invalid)
        {
            if (!await PrepareDataAsync(applicationId))
                return NotFound();
            return Page();
        }

        var application = await repository.GetAsync<CandidateApplication>(a => a.Id == applicationId, q => q
            .Include(candidateApplication => candidateApplication.Applicant)
            .ThenInclude(candidate => candidate.ScholarlyAchievements)
        );
            
        if (application is null)
            return NotFound();

        var success = actionType.Trim().ToLowerInvariant() switch
        {
            "accept" => repository.AcceptApplication(application),
            "reject" => repository.RejectApplication(application, RejectReason),
            _ => throw new ArgumentOutOfRangeException()
        };
        if (!success)
        {
            if (!await PrepareDataAsync(applicationId))
                return NotFound();
            return Page();
        }

        await repository.Context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}