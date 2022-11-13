using System;
namespace TestSENG.Models.BankingViewModels
{
    public class BankingIndexData
    {
        public IEnumerable<Customer> Customer { get; set; } = default!;

        public IEnumerable<Account> Account { get; set; } = default!;
        public IEnumerable<Deposit> Deposit { get; set; } = default!;
        public IEnumerable<TransferMoney> TransferMoney { get; set; } = default!;
        public IEnumerable<Withdraw> Withdraw { get; set; } = default!;
        public IEnumerable<Log_Account> Log_Account { get; set; } = default!;


    }
}

