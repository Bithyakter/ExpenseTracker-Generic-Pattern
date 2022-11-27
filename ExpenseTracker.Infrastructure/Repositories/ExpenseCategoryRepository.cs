using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Infrastructure.Contracts;

namespace ExpenseTracker.Infrastructure.Repositories
{
   public class ExpenseCategoryRepository : Repository<ExpenseCategory>, IExpenseCategoryRepository
   {
      public ExpenseCategoryRepository(DataContext context) : base(context)
      {

      }

      public async Task<ExpenseCategory> GetActiveExpenseCategoryByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(u => u.ExpenseCategoryID == key && u.IsRowDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<ExpenseCategory> GetActiveExpenseCategoryByName(string name)
      {
         try
         {
            return await FirstOrDefaultAsync(u => u.CategoryName.ToLower().Trim() == name.ToLower().Trim() && u.IsRowDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<ExpenseCategory>> GetAllActiveExpenseCategory()
      {
         try
         {
            return await QueryAsync(u => u.IsRowDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

   }
}