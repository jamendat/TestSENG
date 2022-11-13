using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestSENG.Data;
using TestSENG.Models;

namespace TestSENG.Pages.Customers
{
    public class DetailsModel : PageModel
    {
        private readonly TestSENG.Data.BankingContext _context;

        public DetailsModel(TestSENG.Data.BankingContext context)
        {
            _context = context;
        }

      public Customer Customer { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Customer == null)
            {
                return NotFound();
            }


           // var customer = await _context.Customer
             //   .Include(s => s.Account)
               // .ThenInclude(e => e.Deposit)
                //.AsNoTracking()
                //.FirstOrDefaultAsync(m => m.ID_Customer == id);
            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.ID_Customer == id);
            if (customer == null)
            {
                return NotFound();
            }
            else 
            {
                Customer = customer;
            }
            return Page();
        }
    }
}
