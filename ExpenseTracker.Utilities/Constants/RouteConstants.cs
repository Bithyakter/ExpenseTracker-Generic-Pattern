
namespace ExpenseTracker.Utilities.Constants
{
   public static class RouteConstants
   {
      public const string BaseRoute = "expense-tracker-api";

      #region User-Account
      public const string CreateUserAccount = "user-account";

      public const string ReadUserAccounts = "user-accounts";

      public const string ReadUserAccountByKey = "user-account/key/{key}";

      public const string ReadUserAccountByEmail = "user-account/email/{email}";

      public const string UpdateUserAccount = "user-account/{key}";

      public const string DeleteUserAccount = "user-account/{key}";
      #endregion

      #region Expense
      public const string CreateExpense = "expense";

      public const string ReadExpenses = "expenses";

      public const string ReadExpenseByKey = "expense/key/{key}";

      public const string UpdateExpense = "expense/{key}";

      public const string ExpenseByCategory = "expense/expenseByCategory/{categoryId}";

      public const string DeleteExpense = "expense/{key}";
      #endregion

      #region Category
      public const string CreateCategory = "category";

      public const string ReadCategories = "categories";

      public const string ReadCategoryByKey = "category/key/{key}";

      public const string UpdateCategory = "category/{key}";

      public const string DeleteCategory = "category/{key}";
      #endregion
   }
}