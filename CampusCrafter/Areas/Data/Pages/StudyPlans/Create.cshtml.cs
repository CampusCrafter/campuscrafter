using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using CampusCrafter.Data;
using CampusCrafter.Models;
using CampusCrafter.Utils;
using Microsoft.EntityFrameworkCore;

namespace CampusCrafter.Areas.Data.Pages.StudyPlans
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
            AcceptanceCriteria = new AcceptanceCriteria { ScoreWeights = [] };
            
            return Page();
        }

        [BindProperty]
        public StudyPlan StudyPlan { get; set; } = default!;

        [BindProperty] 
        public AcceptanceCriteria AcceptanceCriteria { get; set; } = default!;

        [BindProperty, Display(Name = "Score Weights")] public string ScoreWeightText { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            // This is for testing only
            AcceptanceCriteria.ScoreWeights = ScoreWeightUtil.Deserialize(ScoreWeightText);
            foreach (var scoreWeight in AcceptanceCriteria.ScoreWeights)
                _context.ScoreWeights.Add(scoreWeight);

            StudyPlan.AcceptanceCriteria = AcceptanceCriteria;

            _context.AcceptanceCriteria.Add(AcceptanceCriteria);
            _context.StudyPlans.Add(StudyPlan);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
