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

            Stopwatch sw2 = new Stopwatch();
            for (int i = 0; i < 100; i++)
            {
                sw2.Restart();
                var vehicleReg = Guid.NewGuid();
                Console.WriteLine($"Create new Vehicle {vehicleReg}");
                var vehicleMessage = new VehicleDto() { Registration = vehicleReg.ToString(), VehicleType = "Car" };

                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var response = client.UploadString("https://localhost:5001/api/vehicles", "POST",
                        JsonConvert.SerializeObject(vehicleMessage));
                    var createdVehicle = JsonConvert.DeserializeObject<VehicleDto>(response);
                    vehicleMessage.Id = createdVehicle.Id;
                    sw2.Stop();
                    Console.WriteLine($"Created Vehicle {createdVehicle.Id} - {DateTime.Now}, {sw2.ElapsedMilliseconds}");
                }

                for (int j = 0; j < 10; j++)
                {
                    sw2.Restart();
                    var vehicleMovement = new VehicleMovementDto() { Vehicle = vehicleMessage, ActionTime = DateTime.UtcNow.ToString(), ActionType = "Test" };
                    using (var client = new WebClient())
                    {
                        client.Headers[HttpRequestHeader.ContentType] = "application/json";
                        var response = client.UploadString("https://localhost:5001/api/vehiclemovements", "POST",
                            JsonConvert.SerializeObject(vehicleMovement));
                        sw2.Stop();
                        Console.WriteLine($"Created Vehicle Movement. {DateTime.Now}, {sw2.ElapsedMilliseconds}");
                    }
                }
            }

            IList<VehicleDto> vehicles;
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                var response = client.DownloadString("https://localhost:5001/api/vehicles");
                vehicles = JsonConvert.DeserializeObject<IList<VehicleDto>>(response);
                Console.WriteLine($"Total {vehicles.Count} vehicles fetched");
            }

            foreach (var vehicleDto in vehicles)
            {
                sw2.Restart();
                using (var client = new WebClient())
                {
                    client.Headers[HttpRequestHeader.ContentType] = "application/json";
                    var response = client.UploadString("https://localhost:5001/api/vehicles", "DELETE", JsonConvert.SerializeObject(vehicleDto));
                    sw2.Stop();
                    Console.WriteLine($"Deleted Vehicle {vehicleDto.Id} -  {DateTime.Now}, {sw2.ElapsedMilliseconds}");
                }
            }

            sw.Stop();
            Console.WriteLine($"Time taken = {sw.ElapsedMilliseconds}");
            Console.WriteLine("END");
        }
    }
}
