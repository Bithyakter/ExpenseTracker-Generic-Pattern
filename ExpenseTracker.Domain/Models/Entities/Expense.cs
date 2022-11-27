using ExpenseTracker.Utilities.Constants;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpenseTracker.Domain.Models.Entities
{
   /// <summary>
   /// Expenses entity.
   /// </summary>
   public class Expense : BaseModel
   {
      /// <summary>
      /// Primary key of the table Expenses.
      /// </summary>
      [Key]
      public int ExpenseID { get; set; }

      /// <summary>
      /// Date of expense.
      /// </summary>
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
      [Column(TypeName = "smalldatetime")]
      [Display(Name = "Expense date")]
      public DateTime ExpenseDate { get; set; }

      /// <summary>
      /// Expense amount.
      /// </summary>
      [Required(ErrorMessage = MessageConstants.RequiredFieldError)]
      [Column(TypeName = "decimal(18,2)")]
      [Display(Name = "Amount")]
      public decimal Amount { get; set; }

      /// <summary>
      /// Foreign key. Primary key of the entity ExpenseCategories. 
      /// </summary>
      public int ExpenseCatagoryID { get; set; }

      [ForeignKey("ExpenseCatagoryID")]
      public virtual ExpenseCategory ExpenseCategory { get; set; }
   }
}