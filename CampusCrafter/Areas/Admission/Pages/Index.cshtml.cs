// Pages/Index.cshtml.cs

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace CampusCrafter.Areas.Admission.Pages;

public class IndexModel : PageModel
{
    public IActionResult OnPost(string startButton)
    {
        if (startButton == "true")
        {
            return RedirectToPage("/chooseMajors", 0);
        }
        else if (startButton == "false")
        {
            return RedirectToPage("/chooseMajors", 1);
        }

        // Handle other cases if needed
        return Page();
    }
}