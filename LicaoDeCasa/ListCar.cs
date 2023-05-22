using Microsoft.AspNetCore.Mvc;

namespace LicaoDeCasa
{
    public static class ListCar
    {
        public static List<Car> Cars { get; set; } = Cars = new List<Car>();

        public static void Add(Car car) {        
            Cars.Add(car);
        }

        public static Car Get(string code) {
            return Cars.Find(p => p.Code == code);
        }

        public static void Delete(string code)
        {
            var findCar = Cars.Find(p => p.Code == code);

            Cars.Remove(findCar);
        }

        public static void Init(IConfiguration configuration)
        {
            var cars = configuration.GetSection("Cars").Get<List<Car>>();
            Cars = cars;
        }
    }
}
