using ExpenseTracker.Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Infrastructure
{
   public class DataContext : DbContext
   {
      public DbSet<UserAccount> UserAccounts { get; set; }

      public DbSet<ExpenseCategory> ExpenseCategories { get; set; }

      public DbSet<Expense> Expenses { get; set; }

      protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
      {
         string connectionString = "Data Source=URANUS; Initial Catalog=ExpenseTracker; User Id=sa; Password=abcd123!";
         optionsBuilder.UseSqlServer(connectionString);
      }
   }
}