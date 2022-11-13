using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using TestSENG.Data;
using TestSENG.Models;
using Microsoft.Extensions.Configuration;

namespace TestSENG.Pages.Customers
{
    public class IndexModel : PageModel
    {
        private readonly TestSENG.Data.BankingContext _context;
        private readonly IConfiguration Configuration;

        public IndexModel(TestSENG.Data.BankingContext context, IConfiguration configuration)
        {
            _context = context;
            Configuration = configuration;
        }

        public string NameSort { get; set; } = string.Empty;
        public string DateSort { get; set; } = string.Empty;
        public string CurrentFilter { get; set; } = string.Empty;
        public string CurrentSort { get; set; } = string.Empty;

        //public IList<Customer> Customer { get;set; } = default!;

        public PaginatedList<Customer> Customer { get; set; }

        public async Task OnGetAsync(string sortOrder,
            string currentFilter, string searchString, int? pageIndex)
        {
            CurrentSort = sortOrder;
            NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            DateSort = sortOrder == "Date" ? "date_desc" : "Date";
            if (searchString != null)
            {
                pageIndex = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            IQueryable<Customer> CustomersIQ = from s in _context.Customer
                                              select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                CustomersIQ = CustomersIQ.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString)
                                       || s.IDCardNo.Contains(searchString)
                                       || s.Telephone.Contains(searchString));
            }
          

            switch (sortOrder)
            {
                case "name_desc":
                    CustomersIQ = CustomersIQ.OrderByDescending(s => s.FirstName);
                    break;
                case "Date":
                    CustomersIQ = CustomersIQ.OrderBy(s => s.DateIssue);
                    break;
                case "date_desc":
                    CustomersIQ = CustomersIQ.OrderByDescending(s => s.DateIssue);
                    break;
                default:
                    CustomersIQ = CustomersIQ.OrderBy(s => s.FirstName);
                    break;
            }

            var pageSize = Configuration.GetValue("PageSize", 4);
            Customer = await PaginatedList<Customer>.CreateAsync(
                CustomersIQ.AsNoTracking(), pageIndex ?? 1, pageSize);
            //Customer = await CustomersIQ.AsNoTracking().ToListAsync();
            // if (_context.Customer != null)
            // {
            //     Customer = await _context.Customer.ToListAsync();
            //  }

        }

    }
}
