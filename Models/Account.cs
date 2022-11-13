using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
using TestSENG.Pages;

namespace TestSENG.Models
{
    public class Account
    {
        [Key]
        [Display(Name = "ID_Account")]
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]

        public int ID_Account { get; set; }

        [RegularExpression(@"^[N][L][0-9]{2}[A-Z]{4}[0-9]{10}$"), Required, StringLength(18)]
        [Display(Name = "IBAN")]
        public string IBAN { get; set; } = string.Empty;

        [Range(1, 1000000), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public int Balance { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Enrollment Date")]
        public DateTime DateIssue { get; set; }

        public int ID_Customer { get; set; }

     //   public Customer Customer { get; set; } = default!;
       // public Deposit Deposit { get; set; } = default!;
       // public TransferMoney TransferMoney { get; set; } = default!;
        /// <summary>
        /// public  Deposit { get; set; } = default!;
        /// </summary>
       // public ICollection<TransferMoney> TransferMoney { get; set; } = default!;
    }
}

