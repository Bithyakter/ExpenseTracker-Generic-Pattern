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
   /// 
   /// </summary>
   public class IfInvalidDomain : ValidationAttribute
   {
      private string ValidDomain { get; set; }

      public IfInvalidDomain(String ValidDomain)
      {
         this.ValidDomain = ValidDomain;
      }

      public override bool IsValid(object value)
      {
         string[] array = value.ToString().Split("@");

         if (array.Length > 1)
            return array[1].ToLower() == ValidDomain.ToLower();
         else
            return false;
      }
   }
}