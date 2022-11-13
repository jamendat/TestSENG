using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestSENG.Data;
using TestSENG.Models;

namespace TestSENG.Pages.Accounts
{
    public class DetailsModel : PageModel
    {
        private readonly TestSENG.Data.BankingContext _context;

        public DetailsModel(TestSENG.Data.BankingContext context)
        {
            _context = context;
        }

        public Account Account { get; set; } = default!;
        public Customer Customer { get; set; } = default!;

        public int id_cus { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
        //    id_cus = id.Value;
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == id);
            if (account == null)
            {
                return NotFound();
            }
            else 
            {
                Account = account;
                id_cus = Account.ID_Customer;
            }

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.ID_Customer == id_cus);
            if (account == null)
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
