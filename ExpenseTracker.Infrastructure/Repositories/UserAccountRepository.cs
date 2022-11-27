using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Infrastructure.Contracts;

namespace ExpenseTracker.Infrastructure.Repositories
{
   public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
   {
      public UserAccountRepository(DataContext context) : base(context)
      {
      }

      public async Task<UserAccount> GetActiveUserAccountByKey(int key)
      {
         try
         {
            return await FirstOrDefaultAsync(u => u.UserAccountID == key && u.IsRowDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<UserAccount> GetActiveUserAccountByEmail(string email)
      {
         try
         {
            return await FirstOrDefaultAsync(u => u.Email.ToLower().Trim() == email.ToLower().Trim() && u.IsRowDeleted == false);
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IEnumerable<UserAccount>> GetAllActiveUsers()
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