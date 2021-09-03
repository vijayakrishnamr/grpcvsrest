using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using VehicleTracker.Models;

namespace VehicleTrackerConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("START");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            for (int i = 0; i < 100; i++)
            {
                var vehicleReg = Guid.NewGuid();
                Console.WriteLine($"Create new Vehicle {vehicleReg}");
                var vehicleMessage = new VehicleDto() { Registration = vehicleReg.ToString(), VehicleType = "Car" };

                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var response = client.UploadString("https://localhost:44321/api/vehicles", "POST",
                        JsonConvert.SerializeObject(vehicleMessage));
                    var createdVehicle = JsonConvert.DeserializeObject<VehicleDto>(response);
                    vehicleMessage.Id = createdVehicle.Id;
                    Console.WriteLine($"Created Vehicle {createdVehicle.Id} - {DateTime.Now}");
                }

                for (int j = 0; j < 10; j++)
                {
                    var vehicleMovement = new VehicleMovementDto() { Vehicle = vehicleMessage, ActionTime = DateTime.UtcNow.ToString(), ActionType = "Test" };
                    using (var client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        var response = client.UploadString("https://localhost:44321/api/vehiclemovements", "POST",
                            JsonConvert.SerializeObject(vehicleMovement));
                        Console.WriteLine($"Created Vehicle Movement. {DateTime.Now}");
                    }
                }
            }

            IList<VehicleDto> vehicles;
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var response = client.DownloadString("https://localhost:44321/api/vehicles");
                vehicles = JsonConvert.DeserializeObject<IList<VehicleDto>>(response);
            }

            foreach (var vehicleDto in vehicles)
            {
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var response = client.UploadString("https://localhost:44321/api/vehicles", "DELETE", JsonConvert.SerializeObject(vehicleDto));
                    Console.WriteLine($"Deleted Vehicle {vehicleDto.Id} -  {DateTime.Now}");
                }
            }

            sw.Stop();
            Console.WriteLine($"Time taken = {sw.ElapsedMilliseconds}");
            Console.WriteLine("END");
        }
    }
}
