using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using StudHelper.Data;
using StudHelper.Models;
using Task = StudHelper.Models.Task;

namespace StudHelper.Pages.MyTasks
{
    [Authorize]
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
        ViewData["EmployerId"] = new SelectList(_context.Users, "Id", "Id");
            return Page();
        }

        [BindProperty]
        public Task Task { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Task.Status = 0;

            var usr = _context.Users.Where(u => u.UserName == User.Identity.Name).FirstOrDefault();
            Task.Employer = (User)usr;

            Task.EmployerId = usr.Id;

            Task.EmployeeId = usr.Id;

            _context.Tasks.Add(Task);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
