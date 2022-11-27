using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Utilities.Constants;
using ExpenseTracker.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
   public class UserAccountsController : Controller
   {
      public UserAccountsController()
      {

      }

      public async Task<IActionResult> Index()
      {
         try
         {
            List<UserAccount> userAccounts = new List<UserAccount>();

            HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();

            var outcome = await httpHelper.ReadResourceList("expense-tracker-api/user-accounts");

            if (outcome.ActionStatus == Status.Failed)
            {
               TempData[MessageConstants.MessageKey] = outcome.Message;
            }
            else
            {
               userAccounts = outcome.EntityList;
            }

            return View(userAccounts.OrderBy(o => o.Name));
         }
         catch (Exception)
         {
            throw;
         }
      }

      public async Task<IActionResult> Details(int id)
      {
         try
         {
            HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();
            var outcome = await httpHelper.ReadSingleResource("expense-tracker-api/user-account/key/" + id.ToString());

            if (outcome.ActionStatus == Status.Failed)
               TempData[MessageConstants.MessageKey] = outcome.Message;

            return View(outcome.Entity);
         }
         catch (Exception ex)
         {
            throw;
         }
      }

      [HttpGet]
      public IActionResult Create()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind(BindingConstants.UserAccountCreate)] UserAccount userAccount)
      {
         try
         {
            if (ModelState.IsValid)
            {
               userAccount.DateCreated = DateTime.Now;
               userAccount.CreatedBy = -1;
               userAccount.IsRowDeleted = false;
               userAccount.IsSynced = false;

               HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();
               var outcome = await httpHelper.CreateResource("expense-tracker-api/user-account", userAccount);

               if (outcome.ActionStatus == Status.Success)
               {
                  TempData[MessageConstants.MessageKey] = MessageConstants.RecordSavedSuccessfully;
                  return RedirectToAction("Details", new { id = outcome.Entity.UserAccountID });
               }

               /*Requires improvement*/
               ModelState.AddModelError("", outcome.Message);
            }
         }
         catch (Exception)
         {
            throw;
         }

         return View(userAccount);
      }

      [HttpGet]
      public async Task<IActionResult> Edit(int id)
      {
         try
         {
            if (id <= 0)
               return BadRequest();

            HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();
            var outcome = await httpHelper.ReadSingleResource("expense-tracker-api/user-account/key/" + id.ToString());

            if (outcome.ActionStatus == Status.Failed)
               return NotFound();

            return View(outcome.Entity);
         }
         catch (Exception)
         {
            throw;
         }
      }

      [HttpPost]
      public async Task<IActionResult> Edit([Bind(BindingConstants.UserAccountEdit)] UserAccount userAccount)
      {
         try
         {
            if (ModelState.IsValid)
            {
               userAccount.DateModified = DateTime.Now;
               userAccount.ModifiedBy = -1;
               userAccount.IsSynced = false;

               HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();
               var outcome = await httpHelper.UpdateResource("expense-tracker-api/user-account/", userAccount.UserAccountID, userAccount);

               if (outcome.ActionStatus == Status.Success)
               {
                  TempData[MessageConstants.MessageKey] = MessageConstants.RecordSavedSuccessfully;
                  return RedirectToAction("Details", new { id = userAccount.UserAccountID });
               }

               /*Requires improvement*/
               ModelState.AddModelError("", outcome.Message);
            }
         }
         catch (Exception ex)
         {
            throw;
         }

         return View(userAccount);
      }
   }
}