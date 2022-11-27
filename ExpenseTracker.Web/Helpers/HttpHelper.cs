using ExpenseTracker.Domain.Models.Entities;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace ExpenseTracker.Web.Helpers
{
   public class HttpHelper<T> where T : class
   {
      private readonly HttpClient httpClient;
      private readonly ActionOutcome<T> outcome;

      public HttpHelper()
      {
         var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json").Build();

         var baseUri = config.GetValue<string>("ExpenseTrackerSettings:BaseUri");

         httpClient = new HttpClient();

         httpClient.BaseAddress = new Uri(baseUri);
         httpClient.DefaultRequestHeaders.Clear();
         httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

         outcome = new ActionOutcome<T>();
      }

      /// <summary>
      ///Consumes SmartCare API endpoint that will create a new resource.
      /// </summary>
      /// <param name="endpoint">SmartCare API endpoint.</param>
      /// <param name="entity">Resource to be created.</param>
      /// <returns>Instance of the ActionOutcome object.</returns>
      public async Task<ActionOutcome<T>> CreateResource(string endpoint, T entity)
      {
         try
         {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(endpoint, entity);

            if (response.IsSuccessStatusCode)
            {
               outcome.ActionStatus = Status.Success;
               outcome.Entity = await response.Content.ReadAsAsync<T>();
            }
            else
            {
               outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            }
         }
         catch (Exception ex)
         {
            outcome.Message = ex.Message;
         }
         finally
         {
            httpClient.Dispose();
         }

         return outcome;
      }

      /// <summary>
      /// Consumes SmartCare API endpoint that returns a single resource.
      /// </summary>
      /// <param name="endpoint"> SmartCare API endpoint.</param>
      /// <returns>Instance of the ActionOutcome object.</returns>
      public async Task<ActionOutcome<T>> ReadSingleResource(string endpoint)
      {
         try
         {
            HttpResponseMessage response = await httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
               outcome.ActionStatus = Status.Success;
               outcome.Entity = await response.Content.ReadAsAsync<T>();
            }
            else
            {
               outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            }
         }
         catch (Exception ex)
         {
            outcome.Message = ex.Message;
         }
         finally
         {
            httpClient.Dispose();
         }

         return outcome;
      }

      /// <summary>
      /// Consumes SmartCare API endpoint that returns a list of resources.
      /// </summary>
      /// <param name="endpoint">SmartCare API endpoint.</param>
      /// <returns>Instance of the ActionOutcome object.</returns>
      public async Task<ActionOutcome<T>> ReadResourceList(string endpoint)
      {
         try
         {
            HttpResponseMessage response = await httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
               string result = await response.Content.ReadAsStringAsync();
               outcome.ActionStatus = Status.Success;
               outcome.EntityList = JsonConvert.DeserializeObject<List<T>>(result) ?? new List<T>();
            }
            else
            {
               outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            }
         }
         catch (Exception ex)
         {
            outcome.Message = ex.Message;
         }
         finally
         {
            httpClient.Dispose();
         }

         return outcome;
      }

      /// <summary>
      /// Consumes SmartCare API endpoint that will update an existing resource.
      /// </summary>
      /// <param name="endpoint">SmartCare API endpoint.</param>
      /// <param name="key">Primary key of the resource.</param>
      /// <param name="entity">Resource to be updated.</param>
      /// <returns>Instance of the ActionOutcome object.</returns>
      public async Task<ActionOutcome<T>> UpdateResource(string endpoint, int key, T entity)
      {
         try
         {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(endpoint + key.ToString(), entity);

            if (response.IsSuccessStatusCode)
            {
               outcome.ActionStatus = Status.Success;
               outcome.Entity = entity;
            }
            else
            {
               outcome.Message = JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
            }
         }
         catch (Exception ex)
         {
            outcome.Message = ex.Message;
         }
         finally
         {
            httpClient.Dispose();
         }

         return outcome;
      }
   }

   public class ActionOutcome<T> where T : class
   {
      public Status ActionStatus { get; set; }

      public string Message { get; set; }

      public int StatusCode { get; set; }

      public T Entity { get; set; }

      public List<T> EntityList { get; set; }

      public ActionOutcome()
      {
         ActionStatus = Status.Failed;
         Message = string.Empty;

         Entity = null;
         EntityList = new List<T>();
      }
   }

   public enum Status : byte
   {
      Failed = 0,
      Success = 1
   }
}