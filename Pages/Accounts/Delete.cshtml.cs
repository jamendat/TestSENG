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
    public class DeleteModel : PageModel
    {
        private readonly TestSENG.Data.BankingContext _context;

        public DeleteModel(TestSENG.Data.BankingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        public Customer Customer { get; set; } = default!;

        public int id_cus { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
           
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Account == null)
            {
                return NotFound();
            }
            var account = await _context.Account.FindAsync(id);

            if (account != null)
            {
                Account = account;
                _context.Account.Remove(Account);
                await _context.SaveChangesAsync();
                id_cus = Account.ID_Customer;
            }

          //  return RedirectToPage("./Index?id=id");
            //return RedirectToAction("Index", "id", new { id = id });
            return RedirectToPage("./Index", new { id = Account.ID_Customer });


        }
    }
}
