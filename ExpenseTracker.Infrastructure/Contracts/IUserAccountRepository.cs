using ExpenseTracker.Domain.Models.Entities;

namespace ExpenseTracker.Infrastructure.Contracts
{
   public interface IUserAccountRepository : IRepository<UserAccount>
   {
      /// <summary>
      /// Returns a user account if key matched.
      /// </summary>
      /// <param name="key">Primary key of the table UserAccounts</param>
      /// <returns>Instance of a UserAccount object.</returns>
      public Task<UserAccount> GetActiveUserAccountByKey(int key);

      /// <summary>
      /// Returns a user account if the email matched.
      /// </summary>
      /// <param name="email">Login email of the user.</param>
      /// <returns>Instance of a UserAccount object.</returns>
      public Task<UserAccount> GetActiveUserAccountByEmail(string email);

      /// <summary>
      /// Returns all active user accounts.
      /// </summary>
      /// <returns>List of UserAccount object.</returns>
      public Task<IEnumerable<UserAccount>> GetAllActiveUsers();
   }
}