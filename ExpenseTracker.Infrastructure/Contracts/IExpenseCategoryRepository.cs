using ExpenseTracker.Domain.Models.Entities;

namespace ExpenseTracker.Infrastructure.Contracts
{
   public interface IExpenseCategoryRepository : IRepository<ExpenseCategory>
   {
      /// <summary>
      /// Returns a expense category if key matched.
      /// </summary>
      /// <param name="key">Primary key of the table Expense category</param>
      /// <returns>Instance of a Expense category object.</returns>
      public Task<ExpenseCategory> GetActiveExpenseCategoryByKey(int key);

      /// <summary>
      /// Returns a expense if the name matched.
      /// </summary>
      /// <param name="name">Name of the Expense category.</param>
      /// <returns>Instance of a Expense category object.</returns>
      public Task<ExpenseCategory> GetActiveExpenseCategoryByName(string name);

      /// <summary>
      /// Returns all active expense category.
      /// </summary>
      /// <returns>List of Expense category object.</returns>
      public Task<IEnumerable<ExpenseCategory>> GetAllActiveExpenseCategory();
   }
}
