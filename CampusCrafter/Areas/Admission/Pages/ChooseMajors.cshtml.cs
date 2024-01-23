using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;
using Newtonsoft.Json;

namespace CampusCrafter.Pages.Admission
{
    public class ChooseMajorsModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public ChooseMajorsModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        
        public IList<Major> Major { get;set; } = default!;

        [TempData]
        public MajorType MajorType { get; set; }
        
        [TempData]
        public string SelectedMajors { get; set; }

        public async Task OnGetAsync(MajorType majorType)
        {
            MajorType = majorType;
            Major = await _context.Majors.Where(m => m.MajorType == MajorType)
                .Include(m => m.Department)
                .Include(m => m.Prerequisites)
                .Include(m => m.Specializations)
                .ToListAsync();
        }

        public IActionResult OnPost(List<int> selectedMajors)
        {
            SelectedMajors = selectedMajors.Aggregate("", (a, b) => a + b + "I");
            Console.Out.WriteLine(SelectedMajors);
            Console.Out.WriteLine(0);
            return RedirectToPage("./FillAdmission");
        }
    }
}
