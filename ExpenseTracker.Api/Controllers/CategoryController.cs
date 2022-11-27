using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class CategoryController : ControllerBase
   {
      private readonly IUnitOfWork context;

      public CategoryController(IUnitOfWork context)
      {
         this.context = context;
      }

      /// <summary>
      /// URL: expense-tracker-api/category
      /// </summary>
      [HttpPost]
      [Route(RouteConstants.CreateCategory)]
      public async Task<IActionResult> CreateCategory(ExpenseCategory category)
      {
         try
         {
            if (await IsCategoryDuplicate(category) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

            context.ExpenseCategoryRepository.Add(category);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadExpenseByKey", new { id = category.ExpenseCategoryID }, category);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/categories
      /// </summary>
      [HttpGet]
      [Route(RouteConstants.ReadCategories)]
      public async Task<IActionResult> ReadCategories()
      {
         try
         {
            var expenses = await context.ExpenseCategoryRepository.GetAllActiveExpenseCategory();
            return Ok(expenses);
         }
         catch (Exception ex)
         {
            string message = ex.Message;
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/category/key/{key}
      /// </summary>
      [HttpGet]
      [Route(RouteConstants.ReadCategoryByKey)]
      public async Task<IActionResult> ReadCategoryByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var expense = await context.ExpenseCategoryRepository.GetActiveExpenseCategoryByKey(key);

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
      /// URL: expense-tracker-api/category/{key}
      /// </summary>
      /// <returns></returns>
      [HttpPut]
      [Route(RouteConstants.UpdateCategory)]
      public async Task<IActionResult> UpdateCategory(int key, ExpenseCategory expense)
      {
         try
         {
            if (key != expense.ExpenseCategoryID)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsCategoryDuplicate(expense) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

            var expenseInDb = await context.ExpenseCategoryRepository.GetActiveExpenseCategoryByKey(expense.ExpenseCategoryID);

            if (expenseInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            expenseInDb.CategoryName = expense.CategoryName;
            expenseInDb.DateModified = expense.DateModified;
            expenseInDb.ModifiedBy = expense.ModifiedBy;
            expenseInDb.IsSynced = expense.IsSynced;

            context.ExpenseCategoryRepository.Update(expenseInDb);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/category/{key}
      /// </summary>
      /// <returns></returns>
      [HttpDelete]
      [Route(RouteConstants.DeleteCategory)]
      public async Task<IActionResult> DeleteCategory(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var expenseInDb = await context.ExpenseCategoryRepository.GetActiveExpenseCategoryByKey(key);

            if (expenseInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            expenseInDb.IsRowDeleted = true;

            context.ExpenseCategoryRepository.Update(expenseInDb);
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
      /// Checks whether the category is duplicate? 
      /// </summary>
      /// <param name="expense">Expense category object.</param>
      /// <returns>Boolean</returns>
      private async Task<bool> IsCategoryDuplicate(ExpenseCategory expense)
      {
         try
         {
            var expenseInDb = await context.ExpenseCategoryRepository.GetActiveExpenseCategoryByKey(expense.ExpenseCategoryID);

            if (expenseInDb != null)
            {
               if (expenseInDb.ExpenseCategoryID != expense.ExpenseCategoryID)
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