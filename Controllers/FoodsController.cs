using CallingAPIInClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

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
        public async Task<IActionResult> AddFood(Food food)
        {
            //string UserId = HttpContext.Session.GetString("UserId");
            //int b = int.Parse(UserId);
            //C.UserId = b;
            //C.Food = null;
            //C.User = null;
            Food f = new Food();

            using (var httpClient = new HttpClient())
            {

                StringContent content1 = new StringContent(JsonConvert.SerializeObject(food), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Foods", content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    f = JsonConvert.DeserializeObject<Food>(apiResponse);
                }
            }
            return RedirectToAction("GetAllFoods");

        }
        public async Task<IActionResult> Index()
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

        public async Task<IActionResult> Edit(int? FoodId)
        {

            Food C = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Foods/" + FoodId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    C = JsonConvert.DeserializeObject<Food>(apiResponse);
                }
               


            }
            return View(C);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int foodId, Food C)
        {
           

            
            Food cart = new Food();

            using (var httpClient = new HttpClient())
            {

                StringContent content1 = new StringContent(JsonConvert.SerializeObject(C), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PutAsync("https://localhost:7172/api/Foods/" + foodId, content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cart = JsonConvert.DeserializeObject<Food>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int? FoodId)
        {

            Food C = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Foods/" + FoodId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    C = JsonConvert.DeserializeObject<Food>(apiResponse);
                }
            }
            return View(C);
        }
        [HttpPost]
        public async Task<IActionResult> DeleteFood(int FoodId)
        {
           
            Food food = new Food();

            using (var httpClient = new HttpClient())
            {

                using (var response = await httpClient.DeleteAsync("https://localhost:7172/api/Foods/" + FoodId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    food = JsonConvert.DeserializeObject<Food>(apiResponse);
                }
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Details(int? FoodId)
        {

            Food food = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Foods/" + FoodId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    food = JsonConvert.DeserializeObject<Food>(apiResponse);
                }
            }
            return View(food);
        }
    }
}
