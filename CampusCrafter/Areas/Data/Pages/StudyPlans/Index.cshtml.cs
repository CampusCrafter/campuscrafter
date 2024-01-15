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
    public class IndexModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public IndexModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<StudyPlan> StudyPlan { get;set; } = default!;

        public async Task OnGetAsync()
        {
            StudyPlan = await _context.StudyPlans.ToListAsync();
        }
    }
}
