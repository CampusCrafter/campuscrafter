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
    public class DetailsModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public DetailsModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
