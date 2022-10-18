using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebRazor.Models;

namespace WebRazor.Pages.Admin.Product
{
    public class IndexModel : PageModel
    {
        private readonly WebRazor.Models.PRN221DBContext _context;

        public IndexModel(WebRazor.Models.PRN221DBContext context)
        {
            _context = context;
        }

        public IList<Models.Product> Product { get;set; } = default!;

        public async Task OnGetAsync(string txtSearch)
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

            var qr = (from a in _context.Products orderby a.ProductId ascending select a).Skip((currentPage - 1) * pageSize).Take(pageSize);

            if (!string.IsNullOrEmpty(txtSearch))
            {
                Product = qr.Where(a => a.ProductName.Contains(txtSearch)).ToList();
            }
            else
            {
                Product = await qr.ToListAsync();
            }

        }

        public const int pageSize = 10;
        [BindProperty(SupportsGet = true, Name = "currentPage")]
        public int currentPage { get; set; }
        public int countPages { get; set; }
    }
}
