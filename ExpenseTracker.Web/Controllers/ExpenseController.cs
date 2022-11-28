using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Utilities.Constants;
using ExpenseTracker.Web.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ExpenseTracker.Web.Controllers
{
   public class ExpenseController : Controller
   {
      public async Task<IActionResult> Index()
      {
         try
         {
            List<Expense> expenses = new List<Expense>();

            HttpHelper<Expense> httpHelper = new HttpHelper<Expense>();
            var outcome = await httpHelper.ReadResourceList("expense-tracker-api/expenses");

            if (outcome.ActionStatus == Status.Failed)
            {
               TempData[MessageConstants.MessageKey] = outcome.Message;
            }
            else
            {
               expenses = outcome.EntityList;
            }
            return View(expenses.OrderBy(o => o.ExpenseDate));
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
            HttpHelper<Expense> httpHelper = new HttpHelper<Expense>();
            var outcome = await httpHelper.ReadSingleResource("expense-tracker-api/expense/key/" + id.ToString());

            if (outcome.ActionStatus == Status.Failed)
               TempData[MessageConstants.MessageKey] = outcome.Message;

            return View(outcome.Entity);
         }
         catch (Exception)
         {
            throw;
         }
      }

      [HttpGet]
      public async Task<IActionResult> Create()
      {
         try
         {
            HttpHelper<ExpenseCategory> httpHelper = new HttpHelper<ExpenseCategory>();
            var categories = await httpHelper.ReadResourceList("expense-tracker-api/categories");

            ViewData["category"] = new SelectList(categories.EntityList, "ExpenseCategoryID", "CategoryName");
         }
         catch (Exception)
         {
            throw;
         }
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind(BindingConstants.ExpenseCreate)] Expense expense)
      {
         try
         {
            if (ModelState.IsValid)
            {
               expense.DateCreated = DateTime.Now;
               expense.CreatedBy = -1;
               expense.IsRowDeleted = false;
               expense.IsSynced = false;

               HttpHelper<Expense> httpHelper = new HttpHelper<Expense>();
               var outcome = await httpHelper.CreateResource("expense-tracker-api/expense", expense);

               if (outcome.ActionStatus == Status.Success)
               {
                  TempData[MessageConstants.MessageKey] = MessageConstants.RecordSavedSuccessfully;
                  return RedirectToAction("Details", new { id = outcome.Entity.ExpenseID });
               }

               /*Requires improvement*/
               ModelState.AddModelError("", outcome.Message);
            }
         }
         catch (Exception)
         {
            throw;
         }
         return View(expense);
      }

      [HttpGet]
      public async Task<IActionResult> Edit(int id)
      {
         try
         {
            if (id <= 0)
               return BadRequest();

            HttpHelper<Expense> httpHelper = new HttpHelper<Expense>();
            var outcome = await httpHelper.ReadSingleResource("expense-tracker-api/expense/key/" + id.ToString());

            HttpHelper<ExpenseCategory> httpHelper1 = new HttpHelper<ExpenseCategory>();
            var categories = await httpHelper1.ReadResourceList("expense-tracker-api/categories");

            if (outcome.ActionStatus == Status.Failed)
               return NotFound();

            ViewData["category"] = new SelectList(categories.EntityList, "ExpenseCategoryID", "CategoryName");

            return View(outcome.Entity);
         }
         catch (Exception)
         {
            throw;
         }
      }

      [HttpPost]
      public async Task<IActionResult> Edit([Bind(BindingConstants.ExpenseEdit)] Expense expense)
      {
         try
         {
            if (ModelState.IsValid)
            {
               expense.DateModified = DateTime.Now;
               expense.ModifiedBy = -1;
               expense.IsSynced = false;

               HttpHelper<Expense> httpHelper = new HttpHelper<Expense>();
               var outcome = await httpHelper.UpdateResource("expense-tracker-api/expense/", expense.ExpenseID, expense);

               if (outcome.ActionStatus == Status.Success)
               {
                  TempData[MessageConstants.MessageKey] = MessageConstants.RecordSavedSuccessfully;
                  return RedirectToAction("Details", new { id = expense.ExpenseID });
               }

               /*Requires improvement*/
               ModelState.AddModelError("", outcome.Message);
            }
         }
         catch (Exception ex)
         {
            throw;
         }

         return View(expense);
      }
   }
}