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

    public async Task OnGetAsync(int majorDegree)
    {
        //Major Degree:
        //0 - first degree
        //1 - second degree
        //2 - count score mode
        MajorDegree = majorDegree;
        
        if (MajorDegree == 2)
        {
            Majors = await repository.GetMajorsWithScoreWeightsAsync();
        } 
        else
        {
            Majors = await repository.MajorsPrerequisitesFiltered(MajorDegree == 0);
        }
        
        TempData.Keep();
        
    }

    public IActionResult OnPost(List<string> selectedMajors)
    {
        var selected = selectedMajors.Where(i => i is { Length: > 1 }).ToList();

        if (selected.Count == 0)
        {
            return BadRequest("Choose any major");
        }

        if (!MajorDegree.Equals(2) && selected.Count > 5)
        {
            return BadRequest("You cant apply to more than 5 majors");
        }
        
        TempData.Keep();
        
        SelectedMajors = selected.Aggregate("", (s, i) => s + i + "I");
        return RedirectToPage("./FillAdmission");
    }
}