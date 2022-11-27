using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class ExpenseController : ControllerBase
   {
      private readonly IUnitOfWork context;

      public ExpenseController(IUnitOfWork context)
      {
         this.context = context;
      }

      /// <summary>
      /// URL: expense-tracker-api/expense
      /// </summary>
      [HttpPost]
      [Route(RouteConstants.CreateExpense)]
      public async Task<IActionResult> CreateExpense(Expense expense)
      {
         try
         {
            if (await IsExpenseDuplicate(expense) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

            context.ExpenseRepository.Add(expense);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadExpenseByKey", new { id = expense.ExpenseID }, expense);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/expenses
      /// </summary>
      [HttpGet]
      [Route(RouteConstants.ReadExpenses)]
      public async Task<IActionResult> ReadExpenses()
      {
         try
         {
            var expenses = await context.ExpenseRepository.GetAllActiveExpenses();
            return Ok(expenses);
         }
         catch (Exception ex)
         {
            string message = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/expense/key/{key}
      /// </summary>
      [HttpGet]
      [Route(RouteConstants.ReadExpenseByKey)]
      public async Task<IActionResult> ReadExpenseByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var expense = await context.ExpenseRepository.GetActiveExpenseByKey(key);

            if (expense == null)
            {
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);
            }

            return Ok(expense);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/expense/{key}
      /// </summary>
      /// <returns></returns>
      [HttpPut]
      [Route(RouteConstants.UpdateExpense)]
      public async Task<IActionResult> UpdateExpense(int key, Expense expense)
      {
         try
         {
            if (key != expense.ExpenseID)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsExpenseDuplicate(expense) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

            var expenseInDb = await context.ExpenseRepository.GetActiveExpenseByKey(expense.ExpenseID);

            if (expenseInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            expenseInDb.ExpenseDate = expense.ExpenseDate;
            expenseInDb.Amount = expense.Amount;
            expenseInDb.ExpenseCategory = expense.ExpenseCategory;
            expenseInDb.DateModified = expense.DateModified;
            expenseInDb.ModifiedBy = expense.ModifiedBy;
            expenseInDb.IsSynced = expense.IsSynced;

            context.ExpenseRepository.Update(expenseInDb);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      [HttpDelete]
      [Route(RouteConstants.DeleteExpense)]
      public async Task<IActionResult> DeleteExpense(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var expenseInDb = await context.ExpenseRepository.GetActiveExpenseByKey(key);

            if (expenseInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            expenseInDb.IsRowDeleted = true;

            context.ExpenseRepository.Update(expenseInDb);
            await context.SaveChangesAsync();

            return Ok(expenseInDb);
         }
         catch (Exception)
         {
            ///WriteToLog(ex.Message);
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/expenseByCategory/{categoryId}
      /// </summary>
      /// <param name="categoryId">Foreign key of the table Expenses.</param>
      /// <returns>Http status code: Ok.</returns>
      [HttpGet]
      [Route(RouteConstants.ExpenseByCategory)]
      public async Task<IActionResult> GetAllExpenseByCategory(int categoryId)
      {
         try
         {
            var expenseInDb = await context.ExpenseRepository.GetExpenseByCategory(categoryId);
            return Ok(expenseInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// Checks whether the expense is duplicate? 
      /// </summary>
      /// <param name="expense">Expense object.</param>
      /// <returns>Boolean</returns>
      private async Task<bool> IsExpenseDuplicate(Expense expense)
      {
         try
         {
            var expenseInDb = await context.ExpenseRepository.GetActiveExpenseByKey(expense.ExpenseID);

            if (expenseInDb != null)
            {
               if (expenseInDb.ExpenseID != expense.ExpenseID)
                  return true;
            }

            return false;
         }
         catch
         {
            throw;
         }
      }
   }
}