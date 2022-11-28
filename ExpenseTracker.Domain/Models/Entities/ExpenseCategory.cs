using ExpenseTracker.Utilities.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Models.Entities
{
   /// <summary>
   /// ExpenseCategories entity.
   /// </summary>
   public class ExpenseCategory : BaseModel
   {
      /// <summary>
      /// Primary key of the table ExpenseCategories.
      /// </summary>
      [Key]
      public int ExpenseCategoryID { get; set; }

      /// <summary>
      /// Name of the expense category.
      /// </summary>
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [StringLength(60)]
      [DataType(DataType.Text)]
      [Display(Name = "Expense category")]
      public string CategoryName { get; set; }

      /// <summary>
      /// Foreign key. Primary key of the entity UserAccounts. 
      /// </summary>
      //[Display(Name = "User account")]
      //public int UserAccountID { get; set; }

      //[ForeignKey("UserAccountID")]
      //public virtual UserAccount UserAccount { get; set; }

      /// <summary>
      /// Expences of an expense category.
      /// </summary>
      //public virtual IEnumerable<Expense> Expenses { get; set; }
   }
}