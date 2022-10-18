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
            int totalProduct = await _context.Products.CountAsync();
            countPages = (int)Math.Ceiling((double)totalProduct / pageSize);

            if (currentPage < 1)
            {
                currentPage = 1;
            }
            if (currentPage > countPages)
            {
                currentPage = countPages;
            }

            if (_context.Orders != null)
            {
                Order = await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Employee).Skip((currentPage - 1) * pageSize).Take(pageSize).ToListAsync();
            }
        }
        public const int pageSize = 10;
        [BindProperty(SupportsGet = true, Name = "currentPage")]
        public int currentPage { get; set; }
        public int countPages { get; set; }

        public void OnPost(DateTime txtStartOrderDate, DateTime txtEndOrderDate)
        {
            Order = (from x in _context.Orders where (x.OrderDate >= txtStartOrderDate) && (x.OrderDate <= txtEndOrderDate) select x).ToList();
        }

    }
}
