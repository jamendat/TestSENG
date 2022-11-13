using System;
using System.Diagnostics;
using TestSENG.Models;

namespace TestSENG.Data
{
	public class DbInitializer
	{
        public static void Initialize(BankingContext context)
        {
            // Look for any students.
            if (context.Customer.Any())
            {
                return;   // DB has been seeded
            }

            var Customers= new Customer[]
            {
                new Customer{FirstName="Carson",LastName="Alexander",DateIssue=DateTime.Parse("2019-09-01 1:11:11"), IDCardNo="1234567890987",Address="Address 1",Telephone="0123456789"},
                new Customer{FirstName="Meredith",LastName="Alonso",DateIssue=DateTime.Parse("2017-09-01 1:12:11"), IDCardNo="1234563423987",Address="Address 2",Telephone="0123456789"},
                new Customer{FirstName="Arturo",LastName="Anand",DateIssue=DateTime.Parse("2018-09-01 2:12:11"), IDCardNo="1234565863987",Address="Address 3",Telephone="0123456789"},
                new Customer{ FirstName="Gytis",LastName="Barzdukas",DateIssue=DateTime.Parse("2017-09-01 2:13:11"), IDCardNo="1234965490987",Address="Address 4",Telephone="0123456789"},
                new Customer{FirstName="Yan",LastName="Li",DateIssue=DateTime.Parse("2017-09-01 2:13:11"), IDCardNo="123925690987",Address="Address 5",Telephone="0123456789"},
                new Customer{FirstName="Peggy",LastName="Justice",DateIssue=DateTime.Parse("2016-09-01 2:13:11"), IDCardNo="1231234890987",Address="Address 6",Telephone="0123456789"},
                new Customer{FirstName="Laura",LastName="Norman",DateIssue=DateTime.Parse("2018-09-01 12:13:11"), IDCardNo="1235674890987",Address="Address 7",Telephone="0123456789"},
                new Customer{FirstName="Nino",LastName="Olivetto",DateIssue=DateTime.Parse("2019-09-01 12:13:11"), IDCardNo="1234145290987",Address="Address 8",Telephone="0123456789"}
            };

            context.Customer.AddRange(Customers);
            context.SaveChanges();

            var Accounts = new Account[]
            {
                new Account{IBAN="NL12KLKI0983746352",Balance=0,ID_Customer=1,DateIssue=DateTime.Parse("2019-09-01 12:13:11")},
                new Account{IBAN="NL12KLKI0983363512",Balance=0,ID_Customer=2,DateIssue=DateTime.Parse("2019-09-01 14:13:11")},
                new Account{IBAN="NL12KLKI0981146352",Balance=0,ID_Customer=3,DateIssue=DateTime.Parse("2019-09-01 15:13:11")},
                 new Account{IBAN="NL12KLKI0912746352",Balance=0,ID_Customer=4,DateIssue=DateTime.Parse("2019-09-01 11:13:11")},
                new Account{IBAN="NL12KLKI0983714352",Balance=0,ID_Customer=5,DateIssue=DateTime.Parse("2019-09-01 12:13:11")},
                new Account{IBAN="NL12KLKI1283746352",Balance=0,ID_Customer=6,DateIssue=DateTime.Parse("2019-09-01 15:13:11")}
            };

            context.Account.AddRange(Accounts);
            context.SaveChanges();
        }
    }
}

