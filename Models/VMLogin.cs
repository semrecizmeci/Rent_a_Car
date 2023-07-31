using System.ComponentModel.DataAnnotations;

namespace Rent_a_car_main_page.Models
{
    public class VMLogin
    {
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? PassWord { get; set; }
        public string? Name { get; set; }
        public bool KeppLoggedIn { get; set; }
    }
}
