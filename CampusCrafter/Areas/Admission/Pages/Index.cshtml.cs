using CampusCrafter.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusCrafter.Areas.Admission.Pages;

[Authorize(Roles = "Candidate")]
public class IndexModel : PageModel
{
    
}