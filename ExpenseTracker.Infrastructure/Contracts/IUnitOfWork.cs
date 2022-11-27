namespace ExpenseTracker.Infrastructure.Contracts
{
   public interface IUnitOfWork
   {
      IUserAccountRepository UserAccountRepository { get; }

      IExpenseRepository ExpenseRepository { get; }

      IExpenseCategoryRepository ExpenseCategoryRepository { get; }

      Task<int> SaveChangesAsync();
   }
}