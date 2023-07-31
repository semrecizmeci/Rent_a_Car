using Microsoft.AspNetCore.Mvc;
using Rent_a_car_main_page.Models;

namespace Rent_a_car_main_page.Controllers
{
    public class UploadImagesController : Controller
    {
        public IActionResult UploadPhoto()
        {
            return View();
        }
  

            [HttpPost]
        public async Task<IActionResult> UploadPhoto(ImagesViewModel model)
        {
            if (model.Photo != null && model.Photo.Length > 0)
            {
                // Fotoğrafı kaydetmek için gerekli işlemleri yapın
                // Örneğin, fotoğrafı sunucuda bir klasöre kaydedebilir veya veritabanına base64 formatında kaydedebilirsiniz.

                // Örnek: Fotoğrafı wwwroot/img klasörüne kaydetmek
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);


                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.Photo.CopyToAsync(stream);
                }

                // Fotoğraf başarıyla kaydedildiğinde kullanıcıyı başka bir sayfaya yönlendirin veya istenen işlemi gerçekleştirin
                ViewBag.message = "başarılı gönderme";
                return View();
            }

            // Eğer kullanıcı bir fotoğraf seçmediyse veya seçilen dosya geçerli değilse hata mesajı gösterin
            ModelState.AddModelError("Photo", "Please select a valid photo.");

            // Aynı sayfayı tekrar gösterin ve kullanıcıya hata mesajını gösterin
            ViewBag.message = "gönderme başarısız";
            return View();
        }



    }
}
