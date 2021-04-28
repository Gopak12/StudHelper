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

namespace StudHelper.Areas.Identity.Pages.Account.Profile
{
    public class EditModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public EditModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

        [BindProperty]
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
           ViewData["RoleId"] = new SelectList(_context.Set<AspNetRole>(), "Id", "Id");
           ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
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

            _context.Attach(AspNetUserRole).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AspNetUserRoleExists(AspNetUserRole.UserId))
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

        private bool AspNetUserRoleExists(string id)
        {
            return _context.AspNetUserRole.Any(e => e.UserId == id);
        }
    }
}
