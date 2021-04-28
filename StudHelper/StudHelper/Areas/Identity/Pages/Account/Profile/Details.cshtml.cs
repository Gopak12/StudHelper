using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudHelper.Data;
using StudHelper.Models;

namespace StudHelper.Areas.Identity.Pages.Account.Profile
{
    public class DetailsModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public DetailsModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

        public AspNetUserRole AspNetUserRole { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AspNetUserRole = await _context.AspNetUserRole
                .Include(a => a.Role)
                .Include(a => a.User).FirstOrDefaultAsync(m => m.UserId == id);

            if (AspNetUserRole == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
