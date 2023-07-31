namespace Rent_a_car_main_page.Models
{
    public class UsableCars
    {
        public int UsableId { get; set; }
        public int CarId { get; set; }
        public DateTime UsableTimeStart { get; set; }
        public DateTime UsableTimeFinish { get; set; }
    }
}
