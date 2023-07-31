using Microsoft.AspNetCore.Mvc;

namespace Rent_a_car_main_page.Controllers
{
    public class _carsController : Controller
    {//ViewBag.Isim  zannediyorum null geliyor ve layout dak nav barı etkilemiyor.
        public IActionResult _cars(string viewBagIsim)
        {  
            ViewBag.Isim = viewBagIsim;
            ViewBag.Control = "positive";
            ViewBag.message = "Hoşgeldiniz !!";
            return View();
        }
    
    }
}
