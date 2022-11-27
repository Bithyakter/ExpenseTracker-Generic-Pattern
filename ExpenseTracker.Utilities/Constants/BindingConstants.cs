namespace ExpenseTracker.Utilities.Constants
{
   public static class BindingConstants
   {
      public const string UserAccountCreate = "UserAccountID, Name, DOB, Gender, Email, Password, ConfirmPassword";
      public const string UserAccountEdit = "UserAccountID, Name, DOB, Gender, Email, Password, ConfirmPassword";
      public const string ExpenseCreate = "ExpenseID, ExpenseDate, Amount, ExpenseCatagoryID";
      public const string ExpenseEdit = "ExpenseID, ExpenseDate, Amount, ExpenseCatagoryID";
      public const string CategoryCreate = "ExpenseCategoryID, CategoryName";
      public const string CategoryEdit = "ExpenseCategoryID, CategoryName";
   }
}