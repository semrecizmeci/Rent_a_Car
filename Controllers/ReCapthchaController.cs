using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Rent_a_car_main_page.Controllers
{
    public class ReCaptchaController : Controller
    {
        public IActionResult Captcha()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            //if (string.IsNullOrEmpty(captchaImage))
            //{
            //    return Content("Doldurulmadı");
            //}
            var verified = await CheckCaptcha();
            //if (!verified)
            //{
            //    return Content("Doğrulanmadı");
            //}
            //if (verified)
            //{
            //    return Content("Başarıyla doğrulandı.");
            //}
            return RedirectToAction("_mainPage", "_mainPage");
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
    //public class ReCaptchaController : Controller
    //{
    //    public IActionResult Captcha()
    //    {
    //        return View();
    //    }

    //    [HttpPost]
    //    public async Task<IActionResult> Post()
    //    {
    //        var captchaResponse = HttpContext.Request.Form["g-recaptcha-response"];
    //        if (string.IsNullOrEmpty(captchaResponse))
    //        {
    //            return Content("Doldurulmadı");
    //        }

    //        var verified = await CheckCaptcha(captchaResponse);
    //        if (!verified)
    //        {
    //            return Content("Doğrulanmadı");
    //        }

    //        return Content("Başarıyla doğrulandı.");
    //    }

    //    public async Task<bool> CheckCaptcha(string captchaResponse)
    //    {
    //        var postData = new List<KeyValuePair<string, string>>
    //    {
    //        new KeyValuePair<string, string>("secret", "6LcXc70mAAAAAPDe1ApSEVSuLaXjht1cpDFr-KZc"),
    //        new KeyValuePair<string, string>("response", captchaResponse)
    //    };

    //        using (var client = new HttpClient())
    //        {
    //            var response = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));
    //            dynamic jsonResponse = JsonConvert.DeserializeObject(await response.Content.ReadAsStringAsync());
    //            return jsonResponse.success;
    //        }
    //    }
    //}

}
