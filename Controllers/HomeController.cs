using Microsoft.AspNetCore.Mvc;
using Rent_a_car_main_page.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace Rent_a_car_main_page.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        private readonly string _conncetionString = "Server=104.247.162.242\\MSSQLSERVER2017;Database=akadem58_sc1;" + "User Id=akadem58_sc1;Password=Ez7t46d3%;";

        public IActionResult Index()
        {


            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            ViewBag.Isim = null;
            return RedirectToAction("Login","Access");
        }









        ////nav bar da "car" sekmesine tıklayınca arabaları getiriyor.
        //public IActionResult CarsInfo()
        //{
        
        //        var addto = new List<Selected_Car_Info>();
        //    using (SqlConnection connection = new SqlConnection(_conncetionString))
        //    {
        //        connection.Open();
        //        var command = new SqlCommand("SELECT carId,car_model,doors_number,seats,lugage,transmission,price,created_on FROM Selected_Car", connection);
        //        var reader = command.ExecuteReader();
        //        while (reader.Read())
        //        {
        //            //burada araba ve müsaitlik tabloları çağırılacak ve aşağıda sorgular
        //            //ile if koşuluna sokulacak.
        //            //var commandd = new SqlCommand("SELECT carId,car_model,doors_number,seats,lugage,transmission,price,created_on FROM Selected_Car", connection);


        //            var item = new Selected_Car_Info();
        //            item.CarId = reader.GetInt32(0);
        //            if (item.CarId == 1) 
        //            {
        //                item.Car_Model = reader.GetString(1);
        //                item.Doors_Number = reader.GetInt32(2);

        //                item.Seats = reader.GetInt32(3);
        //                item.Lugage = reader.GetString(4);
        //                item.Transmission = reader.GetString(5);
        //                item.Price = reader.GetInt32(6);
        //                item.Created_On = reader.GetDateTime(7);
        //                addto.Add(item);
        //                break;
        //            };
                    
        //        }
        //    }
        //    return Json(addto);
        //}



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}