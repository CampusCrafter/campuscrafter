using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;

namespace CampusCrafter.Areas.Data.Pages.Semesters
{
    public class DetailsModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public DetailsModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Semester Semester { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var semester = await _context.Semesters.FirstOrDefaultAsync(m => m.Id == id);
            if (semester == null)
            {
                return NotFound();
            }
            else
            {
                Semester = semester;
            }
            return Page();
        }
    }
}
