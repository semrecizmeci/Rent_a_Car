using Microsoft.AspNetCore.Mvc;
using Rent_a_car_main_page.Models;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Rent_a_car_main_page.Controllers
{
    public class _mainPageController : Controller
    {
        private readonly string _conncetionString = "Server=104.247.162.242\\MSSQLSERVER2017;Database=akadem58_sc1;" + "User Id=akadem58_sc1;Password=Ez7t46d3%;";
        public IActionResult _mainPage(string? isim)
        {
            ViewBag.Isim = isim;
            //ViewBag.Control = "positive";
            ViewBag.message = "Hoşgeldiniz ";
            return View();
        }
        public IActionResult _filterCars()
        {
            return View();
        }
       [HttpPost]
        public IActionResult _filterCars(DateTime a, DateTime b, string c, string d,string? viewBagIsim)
        {
            ViewBag.Isim = viewBagIsim;
            if (viewBagIsim==null)
            {
                return RedirectToAction("Login","Access");
            }
            try
            {
                var addto = new List<object>();
                //CitiesSelectedcarUsablecar citiesSelectedcarUsablecar = new CitiesSelectedcarUsablecar();
                var Viewmodel = new CitiesSelectedcarUsablecar();


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

                    List<UsableCars> usableCarsList = new List<UsableCars>();
                    List<Selected_Car_Info> selectedCarInfoList = new List<Selected_Car_Info>();
                    List<Cities> citiesList = new List<Cities>();

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
                     

                            if (itemmm.City == BaslangicYer )
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
                                itemm.İmages = reader.GetString(12);
                                itemm.Areas = reader.GetString(13);

                                usableCarsList.Add(item);
                                selectedCarInfoList.Add(itemm);
                                citiesList.Add(itemmm);

                            }
                           
                        }
                    }
                    Viewmodel.Cities = citiesList;
                    Viewmodel.SelectedCarInfo = selectedCarInfoList;
                    Viewmodel.UsableCars = usableCarsList;
                }
                return View(Viewmodel);

            }
            catch (Exception ex)
            {
                return Content(ex.Message);
                //throw;
            }
        }
        public IActionResult _SelectedCars()
        {
            return View("~/Views/UsableCar/_SelectedCars.cshtml");
        }
        public IActionResult _filterCars2()
        {
            return View();
        }

    }
}
