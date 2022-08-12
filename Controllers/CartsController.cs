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
        public async Task<IActionResult> AddtoCart(Cart C)
        {

            string UserId = HttpContext.Session.GetString("UserId");
            int b = int.Parse(UserId);
            C.UserId = b;
            C.Food = null;
            C.User = null ;
            Cart cart = new Cart();

            using (var httpClient = new HttpClient())
            {

                StringContent content1 = new StringContent(JsonConvert.SerializeObject(C), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Carts/AddtoCart", content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cart = JsonConvert.DeserializeObject<Cart>(apiResponse);
                }
            }
            return RedirectToAction("ViewCart");
        }
        public async Task<IActionResult> ViewCart()
        {

            string UserId = HttpContext.Session.GetString("UserId");
            int b = int.Parse(UserId);
            List<Cart> U=new List<Cart>();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:7172/api/Carts/ViewCart" + b))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    U = JsonConvert.DeserializeObject<List<Cart>>(apiResponse);
                }
            }
            return View(U);
        }
        [HttpPost]
        public async Task<IActionResult> ViewCart(Cart C)
        {

            string UserId = HttpContext.Session.GetString("UserId");
            int b = int.Parse(UserId);
            C.UserId = b;
            C.Food = null;
            C.User = null;
            Cart cart = new Cart();

            using (var httpClient = new HttpClient())
            {

                StringContent content1 = new StringContent(JsonConvert.SerializeObject(C), Encoding.UTF8, "application/json");
                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Carts/AddtoCart", content1))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    cart = JsonConvert.DeserializeObject<Cart>(apiResponse);
                }
            }
            return RedirectToAction("ViewCart");
        }
    }
    }

