using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Infrastructure.Contracts;
using ExpenseTracker.Utilities.Constants;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Api.Controllers
{
   [Route(RouteConstants.BaseRoute)]
   [ApiController]
   public class UserAccountsController : ControllerBase
   {
      private readonly IUnitOfWork context;

      public UserAccountsController(IUnitOfWork context)
      {
         this.context = context;
      }

      /// <summary>
      /// URL: expense-tracker-api/user-account
      /// </summary>
      [HttpPost]
      [Route(RouteConstants.CreateUserAccount)]
      public async Task<IActionResult> CreateUserAccount(UserAccount userAccount)
      {
         try
         {
            if (await IsAccountDuplicate(userAccount) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

            context.UserAccountRepository.Add(userAccount);
            await context.SaveChangesAsync();

            return CreatedAtAction("ReadUserAccountByKey", new { id = userAccount.UserAccountID }, userAccount);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/user-accounts
      /// </summary>
      [HttpGet]
      [Route(RouteConstants.ReadUserAccounts)]
      public async Task<IActionResult> ReadUserAccounts()
      {
         try
         {
            var userAccounts = await context.UserAccountRepository.GetAllActiveUsers();
            return Ok(userAccounts);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/user-account/key/{key}
      /// </summary>
      [HttpGet]
      [Route(RouteConstants.ReadUserAccountByKey)]
      public async Task<IActionResult> ReadUserAccountByKey(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var userAccount = await context.UserAccountRepository
                .GetActiveUserAccountByKey(key);

            if (userAccount == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(userAccount);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/user-account/email/{email}
      /// </summary>
      [HttpGet]
      [Route(RouteConstants.ReadUserAccountByEmail)]
      public async Task<IActionResult> ReadUserAccountByEmail(string email)
      {
         try
         {
            if (string.IsNullOrWhiteSpace(email))
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var userAccount = await context.UserAccountRepository.GetActiveUserAccountByEmail(email);

            if (userAccount == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            return Ok(userAccount);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// URL: expense-tracker-api/user-account/{key}
      /// </summary>
      /// <returns></returns>
      [HttpPut]
      [Route(RouteConstants.UpdateUserAccount)]
      public async Task<IActionResult> UpdateUserAccount(int key, UserAccount userAccount)
      {
         try
         {
            if (key != userAccount.UserAccountID)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.UnauthorizedAttemptOfRecordUpdateError);

            if (await IsAccountDuplicate(userAccount) == true)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.DuplicateUserAccountError);

            var userInDb = await context.UserAccountRepository.GetActiveUserAccountByKey(userAccount.UserAccountID);

            if (userInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            userInDb.Name = userAccount.Name;
            userInDb.DOB = userAccount.DOB;
            userInDb.Gender = userAccount.Gender;
            userInDb.Password = userAccount.Password;
            userInDb.ConfirmPassword = userAccount.ConfirmPassword;
            userInDb.Email = userAccount.Email;
            userInDb.DateModified = userAccount.DateModified;
            userInDb.ModifiedBy = userAccount.ModifiedBy;
            userInDb.IsSynced = userAccount.IsSynced;

            context.UserAccountRepository.Update(userInDb);
            await context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status204NoContent, MessageConstants.RecordSavedSuccessfully);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError);
         }
      }

      [HttpDelete]
      [Route(RouteConstants.DeleteUserAccount)]
      public async Task<IActionResult> DeleteUserAccount(int key)
      {
         try
         {
            if (key <= 0)
               return StatusCode(StatusCodes.Status400BadRequest, MessageConstants.InvalidParameterError);

            var userAccountInDb = await context.UserAccountRepository.GetActiveUserAccountByKey(key);

            if (userAccountInDb == null)
               return StatusCode(StatusCodes.Status404NotFound, MessageConstants.NoMatchFoundError);

            userAccountInDb.IsRowDeleted = true;

            context.UserAccountRepository.Update(userAccountInDb);
            await context.SaveChangesAsync();

            return Ok(userAccountInDb);
         }
         catch (Exception)
         {
            return StatusCode(StatusCodes.Status500InternalServerError, MessageConstants.GenericError);
         }
      }

      /// <summary>
      /// Checks whether the user account is duplicate? 
      /// </summary>
      /// <param name="userAccount">UserAccount object.</param>
      /// <returns>Boolean</returns>
      private async Task<bool> IsAccountDuplicate(UserAccount userAccount)
      {
         try
         {
            var userAccountInDb = await context.UserAccountRepository.GetActiveUserAccountByEmail(userAccount.Email);

            if (userAccountInDb != null)
            {
               if (userAccountInDb.UserAccountID != userAccount.UserAccountID)
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