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
    public class IndexModel : PageModel
    {
        private readonly StudHelper.Data.StudHelperContext _context;

        public IndexModel(StudHelper.Data.StudHelperContext context)
        {
            _context = context;
        }

        public IList<Offer> Offer { get;set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Offer = await _context.Offers
                .Include(o => o.Employee)
                .Include(o => o.Task).ToListAsync();
        }
    }
}
