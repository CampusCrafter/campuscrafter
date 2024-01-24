using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;

namespace CampusCrafter.Areas.Data.Pages.Majors
{
    public class EditModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public EditModel(CampusCrafter.Data.ApplicationDbContext context)
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

            var major =  await _context.Majors.FirstOrDefaultAsync(m => m.Id == id);
            if (major == null)
            {
                return NotFound();
            }
            Major = major;
            ViewData["DepartmentId"] = new SelectList(_context.Departments, "Id", "Name");
            ViewData["OtherMajors"] = new SelectList(_context.Majors.Where(m => m.Id != Major.Id), "Id", "Name");
            ViewData["Specializations"] = new SelectList(_context.Majors.Where(m => m.MajorType == MajorType.Specialization), "Id", "Name");
            ViewData["StudyPlans"] = new SelectList(_context.StudyPlans, "Id", "Name");
            ViewData["Courses"] = new SelectList(_context.Courses, "Id", "Name");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(List<int> majorPrerequisites, List<int> majorSpecializations, List<int> majorStudyPlans, List<int> majorCourses)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var oldMajor = await _context.Majors.AsNoTracking()
                .Include(m => m.Specializations)                
                .Include(m => m.StudyPlans)                
                .Include(m => m.Courses)                
                .FirstOrDefaultAsync(m => m.Id == Major.Id);
            if (oldMajor is not null)
            {
                foreach (var specialization in oldMajor.Specializations) specialization.ParentMajor = null;
                foreach (var plan in oldMajor.StudyPlans) plan.Major = null;
                foreach (var course in oldMajor.Courses) course.Major = null;
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
            
            _context.Attach(Major).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MajorExists(Major.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MajorExists(int id)
        {
            return _context.Majors.Any(e => e.Id == id);
        }
    }
}
