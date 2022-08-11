using CallingAPIInClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace CallingAPIInClient.Controllers
{
    public class CartsController : Controller
    {
        public async Task<IActionResult> AddtoCart(int? FoodId)
        {
            
            string UserId = HttpContext.Session.GetString("UserId");
            Food food = new();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Carts/AddtoCart" + FoodId))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    food = JsonConvert.DeserializeObject<Food>(apiResponse);
                }
            }
            return View(food);
        }
        //[HttpPost]
        //public async Task<IActionResult> AddtoCart(int Qnt, Food food)
        //{


        //    string UserId = HttpContext.Session.GetString("UserId");
        //    int b = int.Parse(UserId);
        //    using (var httpClient = new HttpClient())
        //    {


        //        StringContent content1 = new StringContent(JsonConvert.SerializeObject(Qnt, food.FoodId), Encoding.UTF8, "application/json");

        //        using (var response = await httpClient.PostAsync("https://localhost:7172/api/Carts", content1))
        //        {
        //            string apiResponse = await response.Content.ReadAsStringAsync();
        //            C = JsonConvert.DeserializeObject<Cart>(apiResponse);
        //        }
        //    }
        //    return RedirectToAction("ViewCart");
        //}
        [HttpPost]
        public async Task<IActionResult> AddtoCart(int qnt,int foodId)
        {

            string UserId = HttpContext.Session.GetString("UserId");
            int b = int.Parse(UserId);
            Content C = new Content { Qnt = qnt, FoodId = foodId, UserId = b };

            using (var httpClient = new HttpClient())
            {
                //var data = JsonConvert.SerializeObject(objectDataToPost);
                //StringContent content = new StringContent(data, Encoding.UTF8, "application/json");

                StringContent content1 = new StringContent(JsonConvert.SerializeObject(C), Encoding.UTF8, "application/json");
                //await httpClient.PostAsync("https://localhost:7172/api/Carts/AddtoCart/", content1);
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Carts/AddtoCart/", content1, CancellationToken.None))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    C = JsonConvert.DeserializeObject<Content>(apiResponse);
                }
            }
            return RedirectToAction("ViewCart");
        }

    }
    }

