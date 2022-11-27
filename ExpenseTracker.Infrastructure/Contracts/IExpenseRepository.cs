using ExpenseTracker.Domain.Models.Entities;

namespace ExpenseTracker.Infrastructure.Contracts
{
   public interface IExpenseRepository : IRepository<Expense>
   {
      /// <summary>
      /// Returns a expense if key matched.
      /// </summary>
      /// <param name="key">Primary key of the table Expense</param>
      /// <returns>Instance of a Expense object.</returns>
      public Task<Expense> GetActiveExpenseByKey(int key);

      /// <summary>
      /// Returns a expense if the name matched.
      /// </summary>
      /// <param name="name">Name of the Expense.</param>
      /// <returns>Instance of a Expense object.</returns>
      //public Task<Expense> GetActiveExpenseByName(string name);

      /// <summary>
      /// Returns all active expenses.
      /// </summary>
      /// <returns>List of Expense object.</returns>
      public Task<IEnumerable<Expense>> GetAllActiveExpenses();

      /// <summary>
      /// The method is used to get the expenses by CategoryID.
      /// </summary>
      /// <param name="categoryId">CategoryID of the table Districts.</param>
      /// <returns>Returns a expense if the CategoryID is matched.</returns>
      public Task<IEnumerable<Expense>> GetExpenseByCategory(int categoryId);
   }
}