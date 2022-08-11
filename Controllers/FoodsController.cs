using CallingAPIInClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace CallingAPIInClient.Controllers
{
    public class FoodsController : Controller
    {
        public async Task<IActionResult> GetAllFoods()
        {
            List<Food> ProductInfo = new List<Food>();
            // HttpClient client = new HttpClient();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("https://localhost:7172/api/Foods");

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var ProdResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the product list  
                    ProductInfo = JsonConvert.DeserializeObject<List<Food>>(ProdResponse);

                }
                //returning the product list to view  
                return View(ProductInfo);
            }
        }
    }
}
