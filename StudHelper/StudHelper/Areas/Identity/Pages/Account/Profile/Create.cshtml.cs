using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudHelper.Data;
using StudHelper.Models;

namespace StudHelper.Areas.Identity.Pages.Account.Profile
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
        ViewData["RoleId"] = new SelectList(_context.Set<AspNetRole>(), "Id", "Id");
        ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public AspNetUserRole AspNetUserRole { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AspNetUserRole.Add(AspNetUserRole);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
