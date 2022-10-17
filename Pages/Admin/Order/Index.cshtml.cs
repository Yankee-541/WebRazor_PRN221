using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebRazor.Models;

namespace WebRazor.Pages.Admin.Order
{
    public class IndexModel : PageModel
    {
        private readonly WebRazor.Models.PRN221DBContext _context;

        public IndexModel(WebRazor.Models.PRN221DBContext context)
        {
            _context = context;
        }

        public IList<Models.Order> Order { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee).ToListAsync();
            }
        }
    }
}
