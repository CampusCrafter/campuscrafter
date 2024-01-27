using System.ComponentModel.DataAnnotations;
using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.ApplicationReview.Pages;

public class Index(ApplicationRepository repository, UserManager<ApplicationUser> userManager)
    : PageModel
{
    [BindProperty] public List<CandidateApplication> Applications { get; set; } = default!;

    [BindProperty] public List<ApplicationUser> ApplicantUsers { get; set; } = default!;

    public enum SortingOptions
    {
        [Display(Name = "ID \u25b2")] IdAscending,
        [Display(Name = "ID \u25bc")] IdDescending,
        [Display(Name = "Date \u25b2")] DateAscending,
        [Display(Name = "Date \u25bc")] DateDescending,
    }

    public enum FilteringOptions
    {
        [Display(Name = "None")] None,
        [Display(Name = "Unhandled")] Unhandled,
        [Display(Name = "Accepted")] Accepted,
        [Display(Name = "Rejected")] Rejected,
        [Display(Name = "Accepted or Rejected")] AcceptedOrRejected,
    }

    [BindProperty, Display(Name = "Sort")] public SortingOptions SortingOpts { get; set; } = SortingOptions.IdAscending;
    [BindProperty, Display(Name = "Filter")] public FilteringOptions FilteringOpts { get; set; } = FilteringOptions.None;
    
    public async Task OnGetAsync()
    {
        await GetApplications();
    }

    public async Task OnPostAsync()
    {
        await GetApplications();
    }

    private async Task GetApplications()
    {
        Applications = await repository.GetAllAsync<CandidateApplication>(q =>
        {
            var applications = q.Include(a => a.Applicant)
                .ThenInclude(a => a.Progresses)
                .Include(a => a.Applicant)
                .ThenInclude(a => a.ScholarlyAchievements)
                .Include(a => a.Major);

            var sortedApplications = SortingOpts switch
            {
                SortingOptions.IdAscending => applications.OrderBy(a => a.Id),
                SortingOptions.IdDescending => applications.OrderByDescending(a => a.Id),
                SortingOptions.DateAscending => applications.OrderBy(a => a.Date),
                SortingOptions.DateDescending => applications.OrderByDescending(a => a.Date),
                _ => throw new ArgumentOutOfRangeException()
            };

            var filteredApplications = FilteringOpts switch
            {
                FilteringOptions.None => sortedApplications,
                FilteringOptions.Unhandled => sortedApplications.Where(a =>
                    a.Status != CandidateApplicationStatus.Accepted && a.Status != CandidateApplicationStatus.Rejected),
                FilteringOptions.Accepted => sortedApplications.Where(a =>
                    a.Status == CandidateApplicationStatus.Accepted),
                FilteringOptions.Rejected => sortedApplications.Where(a =>
                    a.Status == CandidateApplicationStatus.Rejected),
                FilteringOptions.AcceptedOrRejected => sortedApplications.Where(a =>
                    a.Status == CandidateApplicationStatus.Accepted || a.Status == CandidateApplicationStatus.Rejected),
                _ => throw new ArgumentOutOfRangeException()
            };

            return filteredApplications;
        });

        ApplicantUsers = [];
        foreach (var application in Applications)
        {
            var user = await userManager.FindByNameAsync(application.Applicant.UserId);
            if (user is not null)
                ApplicantUsers.Add(user);
        }
    }
}