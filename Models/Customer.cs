using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using TestSENG.Data;
using TestSENG.Pages.Customers;



namespace TestSENG.Models
{
    public class Customer
    {
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]


        [Key]
        public int ID_Customer { get; set; }

        
        [Required, StringLength(50)]
    //    [Display(Name = "FirstName")]
        public string FirstName { get; set; } = String.Empty;

  
        [ Required, StringLength(50, ErrorMessage = "Last name cannot be longer than 50 characters.")]
      //  [Display(Name = "LastName")]
        public string LastName { get; set; } = String.Empty;

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd H:mm:ss}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Issue")]
        public DateTime DateIssue { get; set; }

     //  [PageRemote(PageHandler = "IDCardNOEXIST",HttpMethod ="get",ErrorMessage = "Please enter a new ID card number\"")]
        [RegularExpression(@"^[0-9]{13}$"), Required, StringLength(13)]
        [Display(Name = "ID Card No.")]
        public string IDCardNo { get; set; } = string.Empty;

        [Display(Name = "Address"),Required]
        public string Address { get; set; } = string.Empty;

        [RegularExpression(@"^[0-9]{10}$"), Required, StringLength(10, ErrorMessage = "Telephone cannot be longer than 10 characters.")]
        [Display(Name = "Telephone")]
        public string Telephone { get; set; } = string.Empty;

        [Display(Name = "Full Name")]
        public string FullName
       {
           get
           {
               return FirstName + ", " + LastName;
            }
        }

      //  public ICollection<Account> Account { get; set; } = default!;


    }
}

