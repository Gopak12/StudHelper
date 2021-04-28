using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using StudHelper.Data;
using StudHelper.Models;
using Task = StudHelper.Models.Task;

namespace StudHelper.Pages.MyTasks
{
    public class DetailsModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public DetailsModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
