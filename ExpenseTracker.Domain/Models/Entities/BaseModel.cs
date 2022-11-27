using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

/*****************************
 * Created by: Pulak
 * Date Created: 2022.06.10
 * Modified by: Pulak
 * Date Modified: 2022.11.19
 ****************************/
namespace ExpenseTracker.Domain.Models.Entities
{
   /// <summary>
   /// Signature fields of the entities.
   /// </summary>
   public class BaseModel
   {
      /// <summary>
      /// Referance of the user who created the row.
      /// </summary>
      public int? CreatedBy { get; set; }

      /// <summary>
      /// Creation date of the row.
      /// </summary>
      [Column(TypeName = "smalldatetime")]
      [Display(Name = "Date created")]
      public DateTime? DateCreated { get; set; }

      /// <summary>
      /// Referance of the user who last modified the row.
      /// </summary>
      public int? ModifiedBy { get; set; }

      /// <summary>
      /// Last modification date of the row.
      /// </summary>
      [Column(TypeName = "smalldatetime")]
      [Display(Name = "Date modified")]
      public DateTime? DateModified { get; set; }

      /// <summary>
      /// Status of the row. It indicates the row is deleted or not.
      /// If the value is TRUE then the system will treate this row as a deleted row.
      /// </summary>
      [Display(Name = "Row status")]
      public bool? IsRowDeleted { get; set; }

      /// <summary>
      /// Synced status of the row. It indicates whether the row is synced to 
      /// the central database instance.The system always set its value FALSE 
      /// during the time of row creation and row update. After the data sync 
      /// framewok sync this row to the central database instance then its value 
      /// will be set to TRUE.
      /// </summary>
      public bool? IsSynced { get; set; }
   }
}