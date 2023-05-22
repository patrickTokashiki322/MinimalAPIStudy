using Microsoft.AspNetCore.Mvc;

namespace LicaoDeCasa
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();
            var configuration = app.Configuration;
            ListCar.Init(configuration);

            app.MapPost("/cars", (Car car) =>
            {
                var findCar = ListCar.Get(car.Code);

                if (findCar == null)
                {
                    ListCar.Add(car);

                    return Results.Created($"/cars/{car.Code}", car.Code);
                } else
                {
                    return Results.Conflict();
                }

            });

            app.MapGet("/cars", ([FromQuery] string code) =>
            {
                var carFind = ListCar.Get(code);
                
                if (carFind != null)
                {
                    return Results.Ok(carFind);
                } else
                {
                    return Results.NotFound();
                }
            });

            app.MapDelete("/cars", ([FromQuery] string code) =>
            {
                var findCar = ListCar.Get(code);

                if (findCar != null)
                {
                    ListCar.Delete(code);
                    return Results.Ok();
                } else
                {
                    return Results.NotFound();
                }
            });

            app.MapPut("/cars", (Car car) =>
            {
                var savedCar = ListCar.Get(car.Code);

                if (savedCar != null)
                {
                    savedCar.Manufacturer = car.Manufacturer;
                    savedCar.Model = car.Model;
                    savedCar.Age = car.Age;

                    return Results.Ok(savedCar);
                } else
                {
                    return Results.NotFound();
                }

            });
            
            if (app.Environment.IsStaging())
            {
                app.MapGet("/environment/test", () =>
                {
                    
                });
            }

            app.Run();
        }
    }
}