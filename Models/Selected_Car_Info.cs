namespace Rent_a_car_main_page.Models
{
    public class Selected_Car_Info
    {
        public int CarId { get; set; }
        public string? Car_Model { get; set; }
        public int Doors_Number { get; set; }
        public int Seats { get; set; }
        public string? Lugage { get; set; }
        public string? Transmission { get; set; }
        public int Price { get; set; }
        public DateTime Created_On { get; set; }
        public string? İmages { get; set; }
        public string? Areas { get; set; }
    }
}
