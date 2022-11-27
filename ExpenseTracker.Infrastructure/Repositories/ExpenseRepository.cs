using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Infrastructure.Contracts;

namespace ExpenseTracker.Infrastructure.Repositories
{
   public class ExpenseRepository : Repository<Expense>, IExpenseRepository
   {
      public ExpenseRepository(DataContext context) : base(context)
      {
      }

      public async Task<Expense> GetActiveExpenseByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(u => u.ExpenseID == key && u.IsRowDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      //public async Task<Expense> GetActiveExpenseByName(string name)
      //{
      //   try
      //   {
      //      return await FirstOrDefaultAsync(u => u.name.ToLower().Trim() == email.ToLower().Trim() && u.IsRowDeleted == false);
      //   }
      //   catch (Exception)
      //   {
      //      throw;
      //   }
      //}
      
      public async Task<IEnumerable<Expense>> GetAllActiveExpenses()
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

      public async Task<IEnumerable<Expense>> GetExpenseByCategory(int categoryId)
      {
         try
         {
            return await QueryAsync(d => d.IsRowDeleted == false && d.ExpenseCatagoryID == categoryId);
         }
         catch (Exception)
         {
            throw;
         }
      }
   }
}