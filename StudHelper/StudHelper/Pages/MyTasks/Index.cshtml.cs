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
    public class IndexModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public IndexModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

        public IList<Task> Task { get;set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Task = await _context.Tasks
                .Include(t => t.Employee)
                .Include(t => t.Employer).Where(t => t.Employer.UserName == User.Identity.Name).ToListAsync();
        }
    }
}
