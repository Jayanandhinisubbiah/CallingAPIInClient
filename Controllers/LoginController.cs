using CallingAPIInClient.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
namespace CallingAPIInClient.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Registration(UserList U)
        {
            //HttpContext.Session.SetString("UserId", U.UserId.ToString());

            UserList prodobj = new UserList();
            
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(U), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Login/Registration", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    prodobj = JsonConvert.DeserializeObject<UserList>(apiResponse);
                }

                if (prodobj.Role == "Admin")
                    return RedirectToAction("Index", "User");
                else
                    return RedirectToAction("Index", "Foods");
            }
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserList U)
        {
            U.Lname = "";
            U.Address = "";
            U.Email = "";
            U.Gender = "";
            U.City = "";

            UserList prodobj = new UserList();

            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(U), Encoding.UTF8, "application/json");

                using (var response = await httpClient.PostAsync("https://localhost:7172/api/Login/Login", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    prodobj = JsonConvert.DeserializeObject<UserList>(apiResponse);
                    HttpContext.Session.SetString("UserId", prodobj.UserId.ToString());

                }

                if (prodobj.Role == "Admin")
                    return RedirectToAction("Index", "User");
                else
                    return RedirectToAction("GetAllFoods", "Foods");
            }

        }
    }
}

