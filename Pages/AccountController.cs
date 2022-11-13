using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TestSENG.Data;
using TestSENG.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestSENG.Pages
{
    public class AccountController : Controller
    {
        private readonly TestSENG.Data.BankingContext context;
        private readonly UserManager<BankingContext> _userManager;
        public AccountController(UserManager<BankingContext> userManager, TestSENG.Data.BankingContext _context)
        {
            context = _context;
            _userManager = userManager;
        }
        [BindProperty]
        public Account Account { get; set; } = default!;
        //public string IDCardNo { get; set; } = string.Empty;

        [AcceptVerbs("Post","Get")]
        public IActionResult IDCardNOEXIST(string IDCardNo)
        {
            var data = context.Customer.Where(m => m.IDCardNo == IDCardNo).SingleOrDefault();
            if (data != null)
            {
                return Json($"ID Card Number {IDCardNo} is already exist!!");
            }
            else
            {
                return Json(true);

            }
        }
    }
}

