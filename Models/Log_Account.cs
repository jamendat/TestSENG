using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestSENG.Models
{
	public class Log_Account
	{
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Display(Name = "ID_Log_Account")]
        public int ID_Log_Account { get; set; }

        [Display(Name = "ID_Account")]
        public int ID_Account { get; set; }

        [RegularExpression(@"^[N][L][0-9]{2}[A-Z]{4}[0-9]{10}$"), StringLength(18)]
        [Display(Name = "IBAN of Receiver")]
        public string ID_Account_Receive { get; set; } = string.Empty;

        [Range(1, 1000000), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public int Amount { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public int Balance { get; set; }

        // status = 2
        public int status { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Issue")]
        public DateTime DateIssue { get; set; }

        //public ICollection<Account> Account { get; set; } = default!;

    }
}

