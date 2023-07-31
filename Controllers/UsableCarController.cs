using Microsoft.AspNetCore.Mvc;
using Rent_a_car_main_page.Models;
using System.Data.SqlClient;

namespace Rent_a_car_main_page.Controllers
{
    public class UsableCarController : Controller
    {
        private readonly string _conncetionString = "Server=104.247.162.242\\MSSQLSERVER2017;Database=akadem58_sc1;" + "User Id=akadem58_sc1;Password=Ez7t46d3%;";
        public IActionResult UsableCar()
        {
            
            return View();
        }

        [HttpPost]
        public IActionResult SelectedCars(DateTime a,DateTime b,string c,string d)
        {
            try
            {
                var addto = new List<object>();
                var general = new List<object>();
                var otherCities = new List<string>();
                using (SqlConnection connection = new SqlConnection(_conncetionString))
                {
                    connection.Open();
                    var command = new SqlCommand("SELECT  u.usableId, u.carId, u.usableTimestart, u.usableTimefinish, cts.city, c.car_model, c.doors_number, c.seats, c.lugage, c.transmission, c.price, c.created_on, c.images, c.areas " +
                                                  "FROM Selected_Car c " +
                                                  "INNER JOIN usableTime u ON u.carId = c.carId " +
                                                  "INNER JOIN cities cts ON cts.carId=c.carId ", connection);

                    var reader = command.ExecuteReader();
                    DateTime startDate = a;
                    DateTime endDate = b;
                    string BaslangicYer = c;
                    string BitisYer = d;
                    int toplam = 0;
                    while (reader.Read())
                    {
                        var item = new UsableCars();
                        var itemm = new Selected_Car_Info();
                        var itemmm = new Cities();

                        item.UsableTimeStart = reader.GetDateTime(2);
                        item.UsableTimeFinish = reader.GetDateTime(3);
                        itemmm.City = reader.GetString(4);

                        if (item.UsableTimeStart <= startDate || item.UsableTimeFinish >= endDate)
                        {
                          

                            if (itemmm.City == BaslangicYer)
                            {

                                item.UsableId = reader.GetInt32(0);
                                item.CarId = reader.GetInt32(1);
                                itemm.CarId = reader.GetInt32(1);
                                itemmm.CarId = reader.GetInt32(1);
                                item.UsableTimeStart = reader.GetDateTime(2);
                                item.UsableTimeFinish = reader.GetDateTime(3);
                                itemmm.City = reader.GetString(4);
                                itemm.Car_Model = reader.GetString(5);
                                itemm.Doors_Number = reader.GetInt32(6);
                                itemm.Seats = reader.GetInt32(7);
                                itemm.Lugage = reader.GetString(8);
                                itemm.Transmission = reader.GetString(9);
                                itemm.Price = reader.GetInt32(10);
                                itemm.Created_On = reader.GetDateTime(11);
                                itemm.İmages=reader.GetString(12);
                                itemm.Areas = reader.GetString(13);
                                toplam++;
                          

                                var items = new { UsableCars = item, Selected_Car_Info = itemm, Cities = itemmm };
                                addto.Add(items);
                        


                            }

                        }
                    }
                }

                return View(addto);
            }
            catch (Exception ex)
            {
                return Content(ex.Message);
                //throw;
            }
           
            //try
            //{
            //    var addto = new List<object>();
            //    using (SqlConnection connection = new SqlConnection(_conncetionString))
            //    {
            //        connection.Open();
            //        var command = new SqlCommand("SELECT  u.usableId, u.carId, u.usableTimestart, u.usableTimefinish, cts.city, c.car_model, c.doors_number, c.seats, c.lugage, c.transmission, c.price, c.created_on " +
            //                                      "FROM Selected_Car c " +
            //                                      "INNER JOIN usableTime u ON u.carId = c.carId " +
            //                                      "INNER JOIN cities cts ON cts.carId=c.carId ", connection);

            //        var reader = command.ExecuteReader();

            //        DateTime startDate = a;
            //        DateTime endDate = b;
            //        string BaslangicYer = c;
            //        string BitisYer = d;

            //        while (reader.Read())
            //        {
            //            var item = new UsableCars();
            //            var itemm = new Selected_Car_Info();
            //            var itemmm = new Cities();

            //            item.UsableTimeStart = reader.GetDateTime(2);
            //            item.UsableTimeFinish = reader.GetDateTime(3);
            //            itemmm.City = reader.GetString(4);
            //            int toplam = 0;

            //            if (item.UsableTimeStart <= startDate || item.UsableTimeFinish >= endDate)
            //            {
            //                if (itemmm.City == BaslangicYer)
            //                {
            //                    item.UsableId = reader.GetInt32(0);
            //                    item.CarId = reader.GetInt32(1);
            //                    itemm.CarId = reader.GetInt32(1);
            //                    itemmm.CarId = reader.GetInt32(1);
            //                    item.UsableTimeStart = reader.GetDateTime(2);
            //                    item.UsableTimeFinish = reader.GetDateTime(3);
            //                    itemmm.City = reader.GetString(4);
            //                    itemm.Car_Model = reader.GetString(5);
            //                    itemm.Doors_Number = reader.GetInt32(6);
            //                    itemm.Seats = reader.GetInt32(7);
            //                    itemm.Lugage = reader.GetString(8);
            //                    itemm.Transmission = reader.GetString(9);
            //                    itemm.Price = reader.GetInt32(10);
            //                    itemm.Created_On = reader.GetDateTime(11);
            //                    toplam++;

            //                    // Kullanıcının girdiği şehir dışındaki illerin bilgilerini almak için
            //                    var otherCities = new List<string>();
            //                    while (reader.Read())
            //                    {
            //                        var city = reader.GetString(4);
            //                        if (city != BaslangicYer && !otherCities.Contains(city))
            //                        {
            //                            otherCities.Add(city);
            //                        }
            //                    }

            //                    itemmm.OtherCities = otherCities;

            //                    var items = new { UsableCars = item, SelectedCarInfo = itemm, Cities = itemmm };
            //                    addto.Add(items);
            //                }
            //            }
            //        }
            //    }
            //    return Json(addto);
            //}
            //catch (Exception ex)
            //{
            //    return Content(ex.Message);
            //    //throw;
            //}

        }

    }
 

}


//[
//    {
//        "usableCars": {
//            "usableId": 2,
//            "carId": 2,
//            "usableTimeStart": "2023-05-02T09:30:00",
//            "usableTimeFinish": "2023-05-05T09:30:00"
//        },
//        "selectedCarInfo": {
//    "carId": 2,
//            "car_Model": "ferrari",
//            "doors_Number": 2,
//            "seats": 2,
//            "lugage": "3_lugagge",
//            "transmission": "auto",
//            "price": 500,
//            "created_On": "2022-06-06T00:00:00"
//        },
//        "cities": {
//    "cityId": 0,
//            "carId": 2,
//            "city": "İzmir"
//        }
//    },
//    {
//    "usableCars": {
//        "usableId": 2,
//            "carId": 2,
//            "usableTimeStart": "2023-05-02T09:30:00",
//            "usableTimeFinish": "2023-05-05T09:30:00"
//        },
//        "selectedCarInfo": {
//        "carId": 2,
//            "car_Model": "ferrari",
//            "doors_Number": 2,
//            "seats": 2,
//            "lugage": "3_lugagge",
//            "transmission": "auto",
//            "price": 500,
//            "created_On": "2022-06-06T00:00:00"
//        },
//        "cities": {
//        "cityId": 0,
//            "carId": 2,
//            "city": "Aydın"
//        }
//}
//]