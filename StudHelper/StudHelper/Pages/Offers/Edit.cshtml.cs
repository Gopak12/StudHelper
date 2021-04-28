using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using StudHelper.Data;
using StudHelper.Models;

namespace StudHelper.Pages.Offers
{
    public class EditModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public EditModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Offer Offer { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Offer = await _context.Offers
                .Include(o => o.Employee)
                .Include(o => o.Task).FirstOrDefaultAsync(m => m.Id == id);

            if (Offer == null)
            {
                return NotFound();
            }
           ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["TaskId"] = new SelectList(_context.Tasks, "Id", "Id");
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

            _context.Attach(Offer).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OfferExists(Offer.Id))
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

        private bool OfferExists(string id)
        {
            return _context.Offers.Any(e => e.Id == id);
        }
    }
}
