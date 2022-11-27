using ExpenseTracker.Domain.Models.Entities;
using ExpenseTracker.Utilities.Constants;
using ExpenseTracker.Web.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
   public class CategoryController : Controller
   {
      public async Task<IActionResult> Index()
      {
         try
         {
            List<ExpenseCategory> categories = new List<ExpenseCategory>();

            HttpHelper<ExpenseCategory> httpHelper = new HttpHelper<ExpenseCategory>();

            var outcome = await httpHelper.ReadResourceList("expense-tracker-api/categories");

            if (outcome.ActionStatus == Status.Failed)
            {
               TempData[MessageConstants.MessageKey] = outcome.Message;
            }
            else
            {
               categories = outcome.EntityList;
            }

            return View(categories.OrderBy(c => c.CategoryName));
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
            if (id <= 0)
               return BadRequest();

            HttpHelper<ExpenseCategory> httpHelper = new HttpHelper<ExpenseCategory>();
            var outcome = await httpHelper.ReadSingleResource("expense-tracker-api/category/key/" + id.ToString());

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
      public IActionResult Create()
      {
         return View();
      }

      [HttpPost]
      [ValidateAntiForgeryToken]
      public async Task<IActionResult> Create([Bind(BindingConstants.CategoryCreate)] ExpenseCategory category)
      {
         try
         {
            if (ModelState.IsValid)
            {
               category.DateCreated = DateTime.Now;
               category.CreatedBy = -1;
               category.IsRowDeleted = false;
               category.IsSynced = false;

               HttpHelper<ExpenseCategory> httpHelper = new HttpHelper<ExpenseCategory>();
               var outcome = await httpHelper.CreateResource("expense-tracker-api/category", category);

               if (outcome.ActionStatus == Status.Success)
               {
                  TempData[MessageConstants.MessageKey] = MessageConstants.RecordSavedSuccessfully;
                  return RedirectToAction("Details", new { id = outcome.Entity.ExpenseCategoryID });
               }

               ModelState.AddModelError("", outcome.Message);
            }
         }
         catch (Exception)
         {
            throw;
         }

         return View(category);
      }

      [HttpGet]
      public async Task<IActionResult> Edit(int id)
      {
         try
         {
            if (id <= 0)
               return BadRequest();

            HttpHelper<ExpenseCategory> httpHelper = new HttpHelper<ExpenseCategory>();
            var outcome = await httpHelper.ReadSingleResource("expense-tracker-api/category/key/" + id.ToString());

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
      public async Task<IActionResult> Edit([Bind(BindingConstants.CategoryEdit)] ExpenseCategory category)
      {
         try
         {
            if (ModelState.IsValid)
            {
               category.DateModified = DateTime.Now;
               category.ModifiedBy = -1;
               category.IsSynced = false;

               HttpHelper<ExpenseCategory> httpHelper = new HttpHelper<ExpenseCategory>();
               var outcome = await httpHelper.UpdateResource("expense-tracker-api/category/", category.ExpenseCategoryID, category);

               if (outcome.ActionStatus == Status.Success)
               {
                  TempData[MessageConstants.MessageKey] = MessageConstants.RecordSavedSuccessfully;
                  return RedirectToAction("Details", new { id = category.ExpenseCategoryID });
               }

               /*Requires improvement*/
               ModelState.AddModelError("", outcome.Message);
            }
         }
         catch (Exception)
         {
            throw;
         }

         return View();
      }
   }
}