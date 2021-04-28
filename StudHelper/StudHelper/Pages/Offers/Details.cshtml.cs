using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudHelper.Data;
using StudHelper.Models;

namespace StudHelper.Pages.Offers
{
    public class DetailsModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public DetailsModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

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
            Offer.Task.Employer = _context.Users.Where(u => u.Id == Offer.Task.EmployerId).FirstOrDefault();
            if (Offer == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
