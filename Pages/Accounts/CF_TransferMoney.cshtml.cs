using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Plugins;
using TestSENG.Models;

namespace TestSENG.Pages.Accounts
{
	public class CF_TransferMoneyModel : PageModel
    {
        private readonly TestSENG.Data.BankingContext _context;

        public CF_TransferMoneyModel(TestSENG.Data.BankingContext context)
        {
            _context = context;
        }


        [BindProperty]
        public TransferMoney TransferMoney { get; set; } = default!;
        public Account Account { get; set; } = default!;
        public Customer Customer { get; set; } = default!;
        public Account Account_Receiver { get; set; } = default!;
        public Customer Customer_Receiver { get; set; } = default!;


        public int idAccount { get; set; }
        public int idAccount_Receiver { get; set; }
        public decimal Trans_Amount { get; set; }

        public decimal Receiver_o_balance { get; set; }
        public int idCustomer { get; set; }

        public int Old_Balance { get; set; }
        public int New_Balance { get; set; }

        public static readonly string AlertSuccess = "AlertSuccess";
        public static readonly string AlertDanger = "AlertDanger";
        public static readonly string AlertWarning = "AlertWarning";

        public async Task<IActionResult> OnGetAsync(int? id_sender, decimal? amount, int? id_receiver)
        {
            idAccount = id_sender.Value;
            Trans_Amount = amount.Value;
            idAccount_Receiver = id_receiver.Value;

            if (idAccount == null || _context.Account == null)
            {
                return RedirectToPage("./NotFound", new { id = idAccount });
            }

            var account = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == idAccount);
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


            var account_receiver = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == idAccount_Receiver);
            if (account == null)
            {
                return RedirectToPage("./NotFound", new { id = idAccount });
            }
            Account_Receiver = account_receiver;
            Receiver_o_balance = Account_Receiver.Balance;
            int idCustomer_receiver = Account_Receiver.ID_Customer;

            var customer_receiver = await _context.Customer.FirstOrDefaultAsync(m => m.ID_Customer == idCustomer_receiver);
            if (customer == null)
            {
                return RedirectToPage("./NotFound", new { id = idAccount });
            }
            Customer_Receiver = customer_receiver;
            

            return Page();
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(int? id_sender, decimal? amount, int? id_receiver)
        {
            idAccount = id_sender.Value;
            DateTime date = DateTime.Now;
            String date_Convert = date.ToString("yyyy-MM-dd H:mm:ss");
            decimal amount_r = amount.Value;
            idAccount_Receiver = id_receiver.Value;


            // 
            if (!ModelState.IsValid)
            {
                var account = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == idAccount);
                if (account == null)
                {
                    return NotFound();
                }
                Account = account;
                Old_Balance = Account.Balance;

                decimal n_Old_Balance = Convert.ToDecimal(Old_Balance);
                decimal update_new_Balance = n_Old_Balance - amount_r;


                //Use DB in project directory.  If it does not exist, create it:
                var connectionStringBuilder = new SqliteConnectionStringBuilder();
                connectionStringBuilder.DataSource = "./TestSENG.Data.db";

                using (var connection = new SqliteConnection(connectionStringBuilder.ConnectionString))
                {
                    connection.Open();

                    var updateAccount = connection.CreateCommand();
                    updateAccount.CommandText = "Update Account SET Balance = " + update_new_Balance + " Where ID_Account = " + idAccount + " ;";
                    using (var reader = updateAccount.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = reader.GetString(0);
                            Console.WriteLine(message);
                        }
                    }

                    //Read the newly inserted data:
                    var selectCmd = connection.CreateCommand();
                    selectCmd.CommandText = "INSERT INTO TransferMoney (ID_TransferMoney, ID_Account, ID_Account_Receive, Amount, Balance, Status, DateIssue)\nVALUES (NULL, " + idAccount + " , " + idAccount_Receiver + " , " + amount_r + ", " + update_new_Balance + ", " + 3 + ", '" + date_Convert + "');";
                    using (var reader = selectCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = reader.GetString(0);
                            Console.WriteLine(message);
                        }
                    }

                   
                    var InsertCmd = connection.CreateCommand();
                    InsertCmd.CommandText = "INSERT INTO Log_Account (ID_Log_Account, ID_Account, ID_Account_Receive, Amount, Balance, Status, DateIssue)\nVALUES (NULL, " + idAccount + " , " + idAccount_Receiver + " , " + amount_r + ", " + update_new_Balance + ", " + 3 + ", '" + date_Convert + "');";


                    using (var reader = InsertCmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = reader.GetString(0);
                            Console.WriteLine(message);
                        }
                    }

                    var account_re = await _context.Account.FirstOrDefaultAsync(m => m.ID_Account == idAccount_Receiver);
                    if (account_re == null)
                    {
                        return NotFound();
                    }
                    Account_Receiver = account_re;
                    decimal Receiver_o_balance = Account_Receiver.Balance;

                    decimal r_n_balance = Receiver_o_balance + amount_r;

                    var InsertCmd_receiver = connection.CreateCommand();
                    InsertCmd_receiver.CommandText = "INSERT INTO Log_Account (ID_Log_Account, ID_Account, ID_Account_Receive, Amount, Balance, Status, DateIssue)\nVALUES (NULL, " + idAccount_Receiver + " , 0 , " + amount_r + ", " + r_n_balance + ", " + 4 + ", '" + date_Convert + "');";

                    using (var reader = InsertCmd_receiver.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = reader.GetString(0);
                            Console.WriteLine(message);
                        }
                    }
                    var update_receiver = connection.CreateCommand();
                    update_receiver.CommandText = "Update Account SET Balance = " + r_n_balance + " Where ID_Account = " + idAccount_Receiver + " ;";
                    using (var reader = update_receiver.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var message = reader.GetString(0);
                            Console.WriteLine(message);
                        }
                    }

                    connection.Close();
                }

            }



            return RedirectToPage("./Account_Setting", new { id = idAccount });
        }
            
         
    }
}
