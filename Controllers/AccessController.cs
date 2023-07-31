using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Rent_a_car_main_page.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Rent_a_car_main_page.Controllers
{
    public class AccessController : Controller
    {
        private readonly string _conncetionString = "Server=104.247.162.242\\MSSQLSERVER2017;Database=akadem58_sc1;" + "User Id=akadem58_sc1;Password=Ez7t46d3%;";
        public IActionResult Login()
        {
           
            ClaimsPrincipal claimUser = HttpContext.User;
            if(claimUser.Identity.IsAuthenticated)
            return RedirectToAction("_mainPage", "_mainPage");
            return View();
        }



        [HttpPost]
        public async Task<IActionResult> Login(VMLogin modelLogin)
        {
         

            if (!ModelState.IsValid)
            {
                ViewBag.message = "Kullanıcı bilgilerini tam giriniz !";
            }

            using (SqlConnection connection = new SqlConnection(_conncetionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT name, email, password FROM LoginActive", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    VMLogin vMLogin = new VMLogin();
                    vMLogin.Name = reader.GetString(0);
                    vMLogin.Email = reader.GetString(1);
                    vMLogin.PassWord = reader.GetString(2);
                    if (vMLogin.Email == modelLogin.Email && vMLogin.PassWord == modelLogin.PassWord)
                    {
                        
                        List<Claim> claims = new List<Claim>()
                        {
                          new Claim(ClaimTypes.NameIdentifier,modelLogin.Email),
                          new Claim("OtherProperties","Example Role")
                         };


                        var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
                        if (string.IsNullOrEmpty(captchaImage))
                        {
                            ViewBag.message = "Recaptcha doldurulmadı";
                            return View("Login");
                        }
                        var verified = await CheckCaptcha();
                        if (!verified)
                        {
                            ViewBag.message = "Kimlik Doğrulanamadı";
                            return View("Login");
                        }
                        if (verified)
                        {
                            ViewBag.isim = vMLogin.Name;
                            //return RedirectToAction("_mainPage", "_mainPage");
                            return RedirectToAction("_mainPage", "_mainPage", new { isim = vMLogin.Name });

                        }

                        return View();
                    }

                }



                //bu yorum satırlı kısım cookie ile alakalı kısım

                //ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims,
                //    CookieAuthenticationDefaults.AuthenticationScheme);


                //AuthenticationProperties properties = new AuthenticationProperties()
                //{
                //    AllowRefresh=true,
                //    IsPersistent=modelLogin.KeppLoggedIn
                //};


                //await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                //    new ClaimsPrincipal(claimsIdentity), properties);


                //var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
                //if (string.IsNullOrEmpty(captchaImage))
                //{
                //    ViewBag.message = "Recaptcha doldurulmadı";
                //    return View("Login");
                //}
                //var verified = await CheckCaptcha();
                //if (!verified)
                //{
                //    ViewBag.message = "Kimlik Doğrulanamadı";
                //    return View("Login");
                //}
                //if (verified)
                //{

                //    return RedirectToAction("_mainPage", "_mainPage");
                //}
                //return View();
                //}


                return View();
            }
        }
        public async Task<bool> CheckCaptcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", "6LcXc70mAAAAAPDe1ApSEVSuLaXjht1cpDFr-KZc"),
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])


            };
            var client = new HttpClient();
            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));
            var o = (JObject)JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
            return (bool)o["success"];

        }
    }
}
