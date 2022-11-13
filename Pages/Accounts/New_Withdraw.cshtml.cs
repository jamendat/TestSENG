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
using System.Data;
using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace TestSENG.Pages.Accounts
{
	public class New_WithdrawModel : PageModel
    {
        private readonly BankingContext _context;

        public New_WithdrawModel(BankingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Withdraw Withdraw { get; set; } = default!;
        public Account Account { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public int fee { get; set; }
        public int Old_Balance { get; set; }
        public int New_Balance { get; set; }
        public DateTime dateNow { get; set; }
        //  public BankingIndexData AccountData { get; set; } = default!;

        public int idAccount { get; set; }
        public int idCustomer { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {


            idAccount = id.Value;

            if (id == null || _context.Account == null)
            {
                return NotFound();
            }

            var account = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == id);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            idCustomer = Account.ID_Customer;
            Old_Balance = Account.Balance;

            var customer = await _context.Customer.FirstOrDefaultAsync(m => m.ID_Customer == idCustomer);
            if (account == null)
            {
                return NotFound();
            }
            Customer = customer;

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {

            // idAccount = id.Value;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var account = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == id);
            if (account == null)
            {
                return NotFound();
            }
            Account = account;
            idAccount = Account.ID_Account;
            idCustomer = Account.ID_Customer;
            Old_Balance = Account.Balance;


            var entry = _context.Add(new Withdraw());
            entry.CurrentValues.SetValues(Withdraw);
            decimal Amount = _context.Entry(Withdraw).Property(u => u.Amount).CurrentValue;
            double n = Convert.ToDouble(Amount);
            decimal l_balance = Convert.ToDecimal(n);
            DateTime date = DateTime.Now;
            String date_Convert = date.ToString("yyyy-MM-dd H:mm:ss");

            //to update for Account Table
            double last_balance = Old_Balance - n;
            decimal l_n_balance = Convert.ToDecimal(last_balance);
            if (last_balance < 0)
            {
                return RedirectToPage("./NotFoundAC", new { id = idAccount });

            }
            //Use DB in project directory.  If it does not exist, create it:
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./TestSENG.Data.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                //Read the newly inserted data:
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "INSERT INTO Withdraw (ID_Withdraw, ID_Account, Amount, Balance, Status, DateIssue)\nVALUES (NULL, " + idAccount + " , " + l_balance + ", " + l_n_balance + ", " + 1 + ", '" + date_Convert + "');";

                var updateAccount = connection.CreateCommand();
                updateAccount.CommandText = "Update Account SET Balance = " + l_n_balance + " Where ID_Account = " + idAccount + " ;";

                var InsertCmd = connection.CreateCommand();
                InsertCmd.CommandText = "INSERT INTO Log_Account (ID_Log_Account, ID_Account, ID_Account_Receive, Amount, Balance, Status, DateIssue)\nVALUES (NULL, " + idAccount + " , 0 , " + l_balance + ", " + l_n_balance + ", " + 2 + ", '" + date_Convert + "');";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = reader.GetString(0);
                        Console.WriteLine(message);
                    }
                }
                using (var reader = InsertCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = reader.GetString(0);
                        Console.WriteLine(message);
                    }
                }
                using (var reader = updateAccount.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = reader.GetString(0);
                        Console.WriteLine(message);
                    }
                }
                connection.Close();
            }


            return RedirectToPage("./Account_Setting", new { id = Withdraw.ID_Account });
        }
    }
}

