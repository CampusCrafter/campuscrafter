using CampusCrafter.Data;
using CampusCrafter.Models;
using CampusCrafter.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusCrafter.Areas.Admission.Pages;

[Authorize(Roles = "Candidate")]
public class Index(ApplicationRepository repository) : PageModel
{
    public async Task OnGetAsync()
    {
        MadeAdmission = await repository.GetAsync<Candidate>(a => a.UserId == User.Identity!.Name!) != null;
    }
    public bool MadeAdmission { get; set; }
}