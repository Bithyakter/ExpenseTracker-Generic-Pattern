using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Infrastructure.Repositories;

namespace ExpenseTracker.Infrastructure
{
   public class UnitOfWork : IUnitOfWork
   {
      private readonly DataContext context;

      public UnitOfWork(DataContext context)
      {
         this.context = context;
      }

      public async Task<int> SaveChangesAsync()
      {
         return await context.SaveChangesAsync();
      }

      private IUserAccountRepository userAccountRepository;

      public IUserAccountRepository UserAccountRepository
      {
         get
         {
            if (userAccountRepository == null)
            {
               userAccountRepository = new UserAccountRepository(context);
            }

            return userAccountRepository;
         }
      }

      private IExpenseRepository expenseRepository;

      public IExpenseRepository ExpenseRepository
      {
         get
         {
            if (expenseRepository == null)
            {
               expenseRepository = new ExpenseRepository(context);
            }

            return expenseRepository;
         }
      }

      private IExpenseCategoryRepository categoryRepository;

      public IExpenseCategoryRepository ExpenseCategoryRepository
      {
         get
         {
            if (categoryRepository == null)
            {
               categoryRepository = new ExpenseCategoryRepository(context);
            }

            return categoryRepository;
         }
      }
   }
}