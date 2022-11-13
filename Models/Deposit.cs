using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace TestSENG.Models
{
    public class Deposit
    {
   //     [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
       // [Display(Name = "ID_Deposit")]
        public int ID_Deposit { get; set; }

     //   [Display(Name = "ID_Account")]
        public int ID_Account { get; set; }

        [Range(1, 1000000), DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public int Amount { get; set; }

        [ DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public int Balance { get; set; }

        // status = 1
        public int status { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Issue")]
        public DateTime DateIssue { get; set; }

       // public ICollection<Account> Account { get; set; } = default!;


    }
}

