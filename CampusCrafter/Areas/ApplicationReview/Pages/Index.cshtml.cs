using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.ApplicationReview.Pages;

public class Index : PageModel
{
    private ApplicationDbContext context;
    private UserManager<ApplicationUser> userManager;

    public Index(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    } 
    
    [BindProperty] public List<CandidateApplication> Applications { get; set; } = default!;

    [BindProperty] public List<ApplicationUser> ApplicantUsers { get; set; } = default!;
    
    public async Task OnGetAsync()
    {
        Applications = await context.CandidateApplications
            .Include(a => a.Applicant)
            .ThenInclude(a => a.Progresses)
            .Include(a => a.Applicant)
            .ThenInclude(a => a.ScholarlyAchievements)
            .Include(a => a.Major)
            .ToListAsync();

        ApplicantUsers = [];
        foreach (var application in Applications)
        {
            var user = await userManager.FindByNameAsync(application.Applicant.UserId);
            if (user is not null)
                ApplicantUsers.Add(user);
        }
    }
}