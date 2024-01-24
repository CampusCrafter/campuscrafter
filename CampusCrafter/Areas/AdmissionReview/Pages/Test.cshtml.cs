using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.AdmissionReview.Pages;

public class Test : PageModel
{
    private ApplicationDbContext context;
    private UserManager<ApplicationUser> userManager;

    public Test(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
    {
        this.context = context;
        this.userManager = userManager;
    } 
    
    [BindProperty] public List<CandidateApplication> Applications { get; set; } = default!;

    [BindProperty] public List<ApplicationUser> ApplicantUsers { get; set; } = default!;
    
    public async Task OnGet()
    {
        var a = await context.CandidateApplications.ToListAsync();
        
        Applications = await context.CandidateApplications
            .Include(a => a.Applicant)
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