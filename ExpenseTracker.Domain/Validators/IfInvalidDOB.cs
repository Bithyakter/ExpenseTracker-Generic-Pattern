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
   /// This custom validator validates the DOB of the user. It ensures the DOB is not a 
   /// future date and the user is 18 years old.
   /// </summary>
   public class IfInvalidDOB : ValidationAttribute
   {
      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
         var userAccount = (UserAccount)validationContext.ObjectInstance;

         //Checking whether DOB is a furture date:
         if (userAccount.DOB > DateTime.Now)
            return new ValidationResult(MessageConstants.InvalidDOBError);


         //Checking whether the user is under 18 years ole:
         var age = DateTime.Today.Year - userAccount.DOB.Year;

         if (age < 18)
            return new ValidationResult(MessageConstants.UnderAgedUserError);

         return ValidationResult.Success;
      }
   }
}