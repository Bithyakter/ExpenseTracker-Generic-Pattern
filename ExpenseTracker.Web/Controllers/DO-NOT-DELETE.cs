using ExpenseTracker.Domain.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using ExpenseTracker.Web.Helpers;

namespace ExpenseTracker.Web.Controllers
{
    public class UserAccountsController : Controller
    {
        public async Task<IActionResult> Index()
        {
            try
            {
                /*
                List<UserAccount> userAccounts = new List<UserAccount>();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7000/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("expense-tracker-api/user-accounts");

                    if (response.IsSuccessStatusCode)
                    {
                        string result = await response.Content.ReadAsStringAsync();

                        userAccounts = JsonConvert.DeserializeObject<List<UserAccount>>(result) ?? new List<UserAccount>();
                    }
                }

                return View(userAccounts);
                */

                HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();
                var outcome = await httpHelper.ReadResources("expense-tracker-api/user-accounts");

                return View(outcome.EntityList);
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
                /*
                UserAccount userAccount = new UserAccount();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7000/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("expense-tracker-api/user-account/key/" + id.ToString());

                    if (response.IsSuccessStatusCode)
                    {
                        userAccount = await response.Content.ReadAsAsync<UserAccount>();
                    }
                }
                
                return View(userAccount);
                */

                /*
                HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();
                var userAccount = await httpHelper.ReadResource("expense-tracker-api/user-account/key/" + id.ToString());

                if (userAccount == null)
                    return NotFound();
                */

                HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();
                var outcome = await httpHelper.ReadResource("expense-tracker-api/user-account/key/" + id.ToString());

                if (outcome.ActionStatus == Status.Failed)
                    return NotFound();

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
        public async Task<IActionResult> Create(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                /*
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7000/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PostAsJsonAsync("expense-tracker-api/user-account", userAccount);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details", userAccount.UserAccountID);
                    }
                }
                */

                HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();

                userAccount = await httpHelper.CreateResource("expense-tracker-api/user-account", userAccount);

                RedirectToAction("Index");
            }

            return View(userAccount);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                UserAccount userAccount = new UserAccount();

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7000/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("expense-tracker-api/user-account/key/" + id.ToString());

                    if (response.IsSuccessStatusCode)
                    {
                        userAccount = await response.Content.ReadAsAsync<UserAccount>();
                    }
                }

                return View(userAccount);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserAccount userAccount)
        {
            if (ModelState.IsValid)
            {
                /*
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7000/");
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.PutAsJsonAsync("expense-tracker-api/user-account/" + userAccount.UserAccountID.ToString(), userAccount);

                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Details", userAccount.UserAccountID);
                    }
                }
                */

                HttpHelper<UserAccount> httpHelper = new HttpHelper<UserAccount>();
                await httpHelper.UpdateResource("expense-tracker-api/user-account/", userAccount.UserAccountID, userAccount);
            }

            return View(userAccount);
        }
    }
}