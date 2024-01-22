using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;

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

        public MajorType MajorType { get; set; }

        public async Task OnGetAsync(int value)
        {
            MajorType = value == 0 ? MajorType.Specialization : MajorType.Major;
            Major = await _context.Majors.Where(m => m.MajorType == MajorType)
                .Include(m => m.Department)
                .Include(m => m.Prerequisites)
                .Include(m => m.Specializations)
                .ToListAsync();
        }

        public IActionResult OnPost(List<int> selectedMajors)
        {
            return RedirectToPage("./FillAdmission", (selectedMajors, MajorType));
        }
    }
}
