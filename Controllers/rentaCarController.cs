using Microsoft.AspNetCore.Mvc;
using Rent_a_car_main_page.Models;
using System.Data.SqlClient;

namespace Rent_a_car_main_page.Controllers
{
    public class rentaCarController : Controller
    {
       
        private readonly string _conncetionString = "Server=104.247.162.242\\MSSQLSERVER2017;Database=akadem58_sc1;" + "User Id=akadem58_sc1;Password=Ez7t46d3%;";
        //public IActionResult rentaCar() { return View(); }


        //js tarafında bir şeyler yapmaya çalışıyorduk yarım kaldı.
        ///Verileri fotoğrafların altına çekebiliyoruz.
        public IActionResult rentaCar()
        {
            var addto = new List<Selected_Car_Info>();
            using (SqlConnection connection = new SqlConnection(_conncetionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT carId,car_model,doors_number,seats,lugage,transmission,price FROM Selected_Car", connection);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    //var item = new UsableCars();
                    var itemm = new Selected_Car_Info();
                    //item.UsableTimeStart = reader.GetDateTime(2);
                    //item.UsableTimeFinish = reader.GetDateTime(3);

                    //DateTime startDate = a;
                    //DateTime endDate = b;
                    //if (item.UsableTimeStart >= startDate && item.UsableTimeFinish <= endDate)
                    //{
                    //item.UsableId = reader.GetInt32(0);
                    //item.CarId = reader.GetInt32(1);
                    //itemm.CarId = reader.GetInt32(1);
                    //item.UsableTimeStart = reader.GetDateTime(2);
                    //item.UsableTimeFinish = reader.GetDateTime(3);
                    itemm.CarId = reader.GetInt32(0);
                    if(itemm.CarId == 1) {
                        itemm.Car_Model = reader.GetString(1);
                        itemm.Doors_Number = reader.GetInt32(2);
                        itemm.Seats = reader.GetInt32(3);
                        itemm.Lugage = reader.GetString(4);
                        itemm.Transmission = reader.GetString(5);
                        itemm.Price = reader.GetInt32(6);

                    };
                   
                    //itemm.Created_On = reader.GetDateTime(10);

                    //var items = new { UsableCars = item, SelectedCarInfo = itemm };
                    addto.Add(itemm);
                    //}
                }
            }
            return View(addto);
        }
    }
}
