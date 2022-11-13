using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TestSENG.Models;
using TestSENG.Data;
using TestSENG.Models.BankingViewModels;
using Microsoft.VisualBasic;

namespace TestSENG.Pages.Accounts
{
	public class New_TransferMoneyModel : PageModel
    {
        
        private readonly TestSENG.Data.BankingContext _context;

        public New_TransferMoneyModel(TestSENG.Data.BankingContext context)
        {
            _context = context;
        }

       
        [BindProperty]
        public TransferMoney TransferMoney { get; set; } = default!;
        public Account Account { get; set; } = default!;
        public Account Account_receiver { get; set; } = default!;

        public Customer Customer { get; set; } = default!;


        public int idAccount { get; set; }
        public int idCustomer { get; set; }

        public int Old_Balance { get; set; }
        public int New_Balance { get; set; }

        public static readonly string AlertSuccess = "AlertSuccess";
        public static readonly string AlertDanger = "AlertDanger";
        public static readonly string AlertWarning = "AlertWarning";

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            idAccount = id.Value;

            if (id == null || _context.Account == null)
            {
                return RedirectToPage("./NotFound", new { id = idAccount });
            }

            var account = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == id);
            if (account == null)
            {
                return RedirectToPage("./NotFound", new { id = idAccount });
            }
            Account = account;
            Old_Balance = Account.Balance;
            idCustomer = Account.ID_Customer;

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.ID_Customer == idCustomer);
            if (customer == null)
            {
                return RedirectToPage("./NotFound", new { id = idAccount });
            }
            Customer = customer;
            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            idAccount = id.Value;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new TransferMoney());
            entry.CurrentValues.SetValues(TransferMoney);
            decimal Amount = _context.Entry(TransferMoney).Property(u => u.Amount).CurrentValue;
            string ReceiversIBAN = _context.Entry(TransferMoney).Property(u => u.ID_Account_Receive).CurrentValue;

            
             var checkAccout = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == idAccount);
            if (checkAccout != null)
            {
                Account = checkAccout;

                if (Amount > Account.Balance)
                {
                    var Message = "Your balance is not enough!!";
                 //   ViewData["Message"] = string.Format("Hello {0}.\\nCurrent Date and Time: {1}", Account.Balance, DateTime.Now.ToString());
                   // ScriptManager.RegisterStartupScript(Page, this.GetType(), "Key", string.Format("alert('{0}');", Message), true);

                    return RedirectToPage("./NotFoundAC", new { id = idAccount });
                }

            }
            var accountToUpdate = await _context.Account.FirstOrDefaultAsync(m => m.IBAN == ReceiversIBAN);
            if (accountToUpdate == null)
            {
                
               return RedirectToPage("./NotFoundAC", new { id = idAccount });

            }
            Account_receiver = accountToUpdate;




            return RedirectToPage("./CF_TransferMoney", new { id_sender = Account.ID_Account, amount= Amount, id_receiver = Account_receiver.ID_Account });
        }
    }
}
