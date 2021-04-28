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
    public class IndexModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public IndexModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

        public IList<AspNetUserRole> AspNetUserRole { get;set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            AspNetUserRole = await _context.AspNetUserRole
                .Include(a => a.Role)
                .Include(a => a.User).ToListAsync();
        }
    }
}
