using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using TestSENG.Data;
using TestSENG.Models;
using TestSENG.Models.BankingViewModels;

namespace TestSENG.Pages.Accounts
{
    public class CreateModel : PageModel
    {
        private readonly TestSENG.Data.BankingContext _context;

        public CreateModel(TestSENG.Data.BankingContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Account Account { get; set; } = default!;
        public Account Accounts_s { get; set; } = default!;
        public Account Find_Max_Account { get; set; } = default!;
        public int CustomerID { get; set; }
        public Deposit Deposit { get; set; } = default!;
        public BankingIndexData AccountData { get; set; } = default!;


        public string result { get; set; } = string.Empty;


        public IActionResult OnGet(int? id)
        {
            CustomerID = id.Value;
            return Page();
        }

       
        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            CustomerID = id.Value;
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entry = _context.Add(new Account());
             entry.CurrentValues.SetValues(Account);
            string IBAN = _context.Entry(Account).Property(u => u.IBAN).CurrentValue;
            decimal Balance = _context.Entry(Account).Property(u => u.Balance).CurrentValue;
            double n = Convert.ToDouble(Balance);
            double n_balance = (n - (n * 0.001));
            decimal l_balance = Convert.ToDecimal(n_balance);
            DateTime date = DateTime.Now;
            String date_Convert = date.ToString("yyyy-MM-dd H:mm:ss");


            //Use DB in project directory.  If it does not exist, create it:
            var connectionStringBuilder = new SqliteConnectionStringBuilder();
            connectionStringBuilder.DataSource = "./TestSENG.Data.db";

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                //Read the newly inserted data:
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "INSERT INTO Account (ID_Account, IBAN, Balance, DateIssue, ID_Customer)\nVALUES (NULL, '" + IBAN + "', '" + l_balance + "', '" + date_Convert + "'," + CustomerID + ");";

                using (var reader = selectCmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var message = reader.GetString(0);
                        Console.WriteLine(message);
                    }
                }
                connection.Close();
            }


            var id_account_new = (from c in _context.Account
                            where c.ID_Customer == CustomerID
                                  select c).Max(c => c.ID_Account);

            var account = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == id_account_new);
            var Balance_n = account.Balance;           

            using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
            {
                connection.Open();

                //Read the newly inserted data:
                var selectCmd = connection.CreateCommand();
                selectCmd.CommandText = "INSERT INTO Deposit (ID_Deposit, ID_Account, Amount,Balance, Status, DateIssue)\nVALUES (NULL, " + id_account_new + " , " + l_balance + ", " + l_balance + ", " + 1 + ", '" + date_Convert + "');";

                var InsertCmd = connection.CreateCommand();
                InsertCmd.CommandText = "INSERT INTO Log_Account (ID_Log_Account, ID_Account, ID_Account_Receive, Amount, Balance, Status, DateIssue)\nVALUES (NULL, " + id_account_new + " , 0 , " + l_balance + ", " + l_balance + ", " + 1 + ", '" + date_Convert + "');";

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
                connection.Close();
            }
        

            return RedirectToPage("./Index", new { id = CustomerID });





        }
    }
}
