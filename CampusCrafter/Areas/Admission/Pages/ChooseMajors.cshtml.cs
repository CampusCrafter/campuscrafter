using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;
using CampusCrafter.Utils;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace CampusCrafter.Pages.Admission;

[Authorize(Roles = "Candidate")]
public class ChooseMajorsModel(ApplicationRepository repository) : PageModel
{
    public List<Major> Major { get; set; } = default!;

    [TempData]
    public MajorType MajorType { get; set; }

    [TempData] public string SelectedMajors { get; set; } = default!;

    public async Task OnGetAsync(MajorType majorType)
    {
        MajorType = majorType;
        Major = await repository.GetAllAsync<Major>(q => q
            .Where(m => m.MajorType == MajorType)
            .Include(m => m.Department)
            .Include(m => m.Prerequisites)
            .Include(m => m.Specializations)
        );
    }

    public IActionResult OnPost(List<int> selectedMajors)
    {
        SelectedMajors = selectedMajors.Aggregate("", (a, b) => a + b + "I");
        return RedirectToPage("./FillAdmission");
    }
}