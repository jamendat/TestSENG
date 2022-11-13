using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace TestSENG.Pages.Accounts
{
	public class NotFoundACModel : PageModel
    {
        public int idAcccout { get; set; }
        public void OnGet(int? id)
        {
            idAcccout = id.Value;
        }
    }
}
