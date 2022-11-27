using ExpenseTracker.Domain.Validators;
using ExpenseTracker.Utilities.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Models.Entities
{
   /// <summary>
   /// UserAccounts entity.
   /// </summary>
   public class UserAccount : BaseModel
   {
      /// <summary>
      /// Primary key of the table UserAccounts.
      /// </summary>
      [Key]
      public int UserAccountID { get; set; }

      /// <summary>
      /// Name of the user.
      /// </summary>
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [StringLength(90)]
      [DataType(DataType.Text)]
      [Display(Name = "Name")]
      public string Name { get; set; }

      [Required]
      [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
      [DataType(DataType.DateTime)]
      [Column(TypeName = "smalldatetime")]
      [IfInvalidDOB]
      public DateTime DOB { get; set; }

      /// <summary>
      /// Gender of the user.
      /// </summary>        
      [Display(Name = "Gender")]
      [IfGenderNotSelected]
      public Enums.Gender Gender { get; set; }

      /// <summary>
      /// Login email address of the user.
      /// </summary>
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [StringLength(60)]
      [DataType(DataType.EmailAddress)]
      [Display(Name = "Login email")]
      //[IfInvalidDomain(ValidDomain: "ihmafrica.org", ErrorMessage = MessageConstants.UnofficialEmailAddressError)]
      public string Email { get; set; }

      /// <summary>
      /// Login password of the user.
      /// </summary> 
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [MinLength(5, ErrorMessage = MessageConstants.PasswordLengthError)]
      [DataType(DataType.Password)]
      [Display(Name = "Password")]
      public string Password { get; set; }

      /// <summary>
      /// Confirmed password.
      /// </summary>
      [NotMapped]
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [MinLength(5, ErrorMessage = MessageConstants.PasswordLengthError)]
      [DataType(DataType.Password)]
      [Display(Name = "Confirm password")]
      [Compare("Password", ErrorMessage = MessageConstants.ConfirmedPasswordNotMatchedError)]
      public string ConfirmPassword { get; set; }

      /// <summary>
      /// Expense categories of the user.
      /// </summary>
      //public virtual IEnumerable<ExpenseCategory> ExpenseCategories { get;}
   }
}