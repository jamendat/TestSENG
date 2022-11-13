using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TestSENG.Data;
using TestSENG.Models;

namespace TestSENG.Pages.Customers
{
    public class CreateModel : PageModel
    {
        private readonly TestSENG.Data.BankingContext _context;

        public CreateModel(TestSENG.Data.BankingContext context)
        {
            _context = context;
        }
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
    
        public Customer Customer { get; set; } = default!;

        // public BankingIndexData Customer { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (_context.Customer==null)
            {

                return Page();
            }
           

            var entry = _context.Add(new Customer());
            entry.CurrentValues.SetValues(Customer);
             await _context.SaveChangesAsync();

            return RedirectToPage("./Index");

        }

     
    }
}
