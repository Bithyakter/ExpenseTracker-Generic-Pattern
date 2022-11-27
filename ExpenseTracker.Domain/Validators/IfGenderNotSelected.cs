using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Utilities.Constants;
using System.ComponentModel.DataAnnotations;

/*****************************
 * Created by: Pulak
 * Date Created: 2022.06.10
 * Modified by: Pulak
 * Date Modified: 2022.11.19
 ****************************/
namespace ExpenseTracker.Domain.Validators
{
   /// <summary>
   /// This custom validator ensures the gender of the user is selected. 
   /// </summary>
   public class IfGenderNotSelected : ValidationAttribute
   {
      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
         var userAccount = (UserAccount)validationContext.ObjectInstance;

         if (userAccount.Gender <= 0)
            return new ValidationResult(MessageConstants.GenderNotSelectedError);

         return ValidationResult.Success;
      }
   }
}
