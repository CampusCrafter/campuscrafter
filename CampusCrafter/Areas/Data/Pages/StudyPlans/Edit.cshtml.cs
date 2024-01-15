using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CampusCrafter.Data;
using CampusCrafter.Models;
using CampusCrafter.Utils;

namespace CampusCrafter.Areas.Data.Pages.StudyPlans
{
    public class EditModel : PageModel
    {
        private readonly CampusCrafter.Data.ApplicationDbContext _context;

        public EditModel(CampusCrafter.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public StudyPlan StudyPlan { get; set; } = default!;

        [BindProperty, Display(Name = "Score Weights")]
        public string ScoreWeightText { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studyplan =  await _context.StudyPlans.Include(studyPlan => studyPlan.AcceptanceCriteria)
                .ThenInclude(acceptanceCriteria => acceptanceCriteria.ScoreWeights).FirstOrDefaultAsync(m => m.Id == id);
            if (studyplan == null)
            {
                return NotFound();
            }
            StudyPlan = studyplan;
            ScoreWeightText = ScoreWeightUtil.Serialize(StudyPlan.AcceptanceCriteria.ScoreWeights);
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            // This is for testing only
            StudyPlan.AcceptanceCriteria.ScoreWeights = ScoreWeightUtil.Deserialize(ScoreWeightText);
            foreach (var scoreWeight in StudyPlan.AcceptanceCriteria.ScoreWeights)
                _context.ScoreWeights.Update(scoreWeight);

            _context.Attach(StudyPlan).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudyPlanExists(StudyPlan.Id))
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

        private bool StudyPlanExists(int id)
        {
            return _context.StudyPlans.Any(e => e.Id == id);
        }
    }
}
