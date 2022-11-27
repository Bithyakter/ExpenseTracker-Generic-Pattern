using Microsoft.AspNetCore.Mvc;

namespace ExpenseTracker.Web.Controllers
{
   public class UsersController : Controller
   {
      private readonly IHttpClientFactory clientFactory;

      public UsersController(IHttpClientFactory clientFactory)
      {
         this.clientFactory = clientFactory;
      }

      public IActionResult Index()
      {
         var client = clientFactory.CreateClient();

         return View();
      }
   }
}
