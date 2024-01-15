using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;

namespace CampusCrafter.Areas.Data.Pages.Departments
{
    public class IndexModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public IndexModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Department> Department { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Department = await _context.Departments.ToListAsync();
        }
    }
}