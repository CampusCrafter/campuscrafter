using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CampusCrafter.Data;
using CampusCrafter.Models;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.Data.Pages.Majors
{
    public class CreateModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public CreateModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            SetupViewData();
            return Page();
        }

        private void SetupViewData()
        {
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["OtherMajors"] = new SelectList(_context.Majors, "Id", "Name");
            ViewData["Specializations"] = new SelectList(_context.Majors.Where(m => m.MajorType == MajorType.Specialization && m.ParentMajor == null), "Id", "Name");
            ViewData["StudyPlans"] = new SelectList(_context.StudyPlans.Where(s => s.Major == null), "Id", "Name");
            ViewData["Courses"] = new SelectList(_context.Courses.Where(c => c.Major == null), "Id", "Name");
        }

        [BindProperty]
        public Major Major { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(List<int> majorPrerequisites, List<int> majorSpecializations, List<int> majorStudyPlans, List<int> majorCourses)
        {
            if (!ModelState.IsValid)
            {
                SetupViewData();
                return Page();
            }

            Major.Prerequisites = await _context.Majors
                .Join(majorPrerequisites, major => major.Id, id => id, (major, i) => major)
                .ToListAsync();
            
            var specializations = await _context.Majors
                .Join(majorSpecializations, m => m.Id, id => id, (m, i) => m)
                .ToListAsync();
            foreach (var specialization in specializations) specialization.ParentMajor = Major;
            Major.Specializations = specializations;

            var studyPlans = await _context.StudyPlans
                .Join(majorStudyPlans, s => s.Id, id => id, (plan, i) => plan)
                .ToListAsync();
            foreach (var plan in studyPlans) plan.Major = Major;
            Major.StudyPlans = studyPlans;
            
            var courses = await _context.Courses
                .Join(majorCourses, c => c.Id, id => id, (course, i) => course)
                .ToListAsync();
            foreach (var course in courses) course.Major = Major;
            Major.Courses = courses;

            _context.Majors.Add(Major);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
