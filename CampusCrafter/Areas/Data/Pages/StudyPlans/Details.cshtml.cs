using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;

namespace CampusCrafter.Areas.Data.Pages.StudyPlans
{
    public class DetailsModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public DetailsModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public StudyPlan StudyPlan { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyplan = await _context.StudyPlans.FirstOrDefaultAsync(m => m.Id == id);
            if (studyplan == null)
            {
                return NotFound();
            }
            else
            {
                StudyPlan = studyplan;
            }
            return Page();
        }
    }
}
