using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudHelper.Data;
using StudHelper.Models;

namespace StudHelper.Pages.Offers
{
    public class CreateModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public CreateModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id");
        ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Offer Offer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Offers.Add(Offer);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
