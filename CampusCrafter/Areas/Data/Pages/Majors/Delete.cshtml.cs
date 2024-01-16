using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;

namespace CampusCrafter.Areas.Data.Pages.Majors
{
    public class DeleteModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public DeleteModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Major Major { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var major = await _context.Majors.FirstOrDefaultAsync(m => m.Id == id);

            if (major == null)
            {
                return NotFound();
            }
            else
            {
                Major = major;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var major = await _context.Majors
                .Include(m => m.StudyPlans)
                .Include(m => m.Courses)
                .Include(m => m.Specializations)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (major != null)
            {
                Major = major;

                foreach (var plan in major.StudyPlans)
                    plan.Major = null;
                foreach (var course in major.Courses)
                    course.Major = null;
                foreach (var specialization in major.Specializations)
                    specialization.ParentMajor = null;
                
                _context.Majors.Remove(Major);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
