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
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace CampusCrafter.Pages.Admission;

[Authorize(Roles = "Candidate")]
public class ChooseMajorsModel(ApplicationRepository repository) : PageModel
{
    public List<Major> Majors { get; set; } = default!;

    [TempData] public int MajorDegree { get; set; }

    [TempData] public string SelectedMajors { get; set; } = default!;

    public async Task OnGetAsync(int value)
    {
        MajorDegree = value;
        if (MajorDegree == 2)
        {
            Majors = await repository.GetAllAsync<Major>();
        } 
        else
        {
            Majors = await repository.MajorsPrerequisitesFiltered(MajorDegree == 0);
        }
    }

    public IActionResult OnPost(List<string> selectedMajors)
    {
        var selected = selectedMajors.Where(i => i is { Length: > 1 }).ToList();

        var degree = TempData.Peek("MajorDegree");

        if (!degree!.Equals(2) && selected.Count > 5)
        {
            var p = RedirectToPage("ChooseMajors");
            p.RouteValues = new RouteValueDictionary
            {
                ["value"] = degree
            };
            return p;
        }
        SelectedMajors = selected.Aggregate("", (s, i) => s + i + "I");
        return RedirectToPage("./FillAdmission");
    }
}