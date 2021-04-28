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
using Task = StudHelper.Models.Task;

namespace StudHelper.Pages.MyTasks
{
    public class EditModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public EditModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Task Task { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Task = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Employer).FirstOrDefaultAsync(m => m.Id == id);

            if (Task == null)
            {
                return NotFound();
            }
           ViewData["EmployeeId"] = new SelectList(_context.Users, "Id", "Id");
           ViewData["EmployerId"] = new SelectList(_context.Users, "Id", "Id");
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

            _context.Attach(Task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(Task.Id))
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

        private bool TaskExists(string id)
        {
            return _context.Tasks.Any(e => e.Id == id);
        }
    }
}
