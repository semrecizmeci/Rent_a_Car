namespace Rent_a_car_main_page.Models
{
    public class Cities
    {
        public int CityId { get; set; }
        public int CarId { get; set; }
        public string? City { get; set; }

        public static implicit operator List<object>(Cities v)
        {
            throw new NotImplementedException();
        }
    }
}
