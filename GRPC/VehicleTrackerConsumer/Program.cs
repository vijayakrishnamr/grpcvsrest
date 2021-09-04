using System;
using System.Collections.Generic;
using System.Diagnostics;
using VehicleTracker;

namespace VehicleTrackerConsumer
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("START");
            Stopwatch sw = new Stopwatch();
            sw.Start();

            using (var channel = Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:5001/"))
            {
                var client = new VehicleTracker.TrackerService.TrackerServiceClient(channel);
                var sw2 = new Stopwatch();
                for (int i = 0; i < 100; i++)
                {
                    sw2.Restart();
                    var vehicleReg = Guid.NewGuid();
                    Console.WriteLine($"Create new Vehicle {vehicleReg}");
                    var vehicleMessage = new VehicleMessage() { Registration = vehicleReg.ToString(), VehicleType = "Car" };
                    var response = client.CreateVehicle(vehicleMessage);
                    sw2.Stop();
                    Console.WriteLine($"Created Vehicle with Id {response.Vehicle.Id} - {DateTime.Now}, {sw2.ElapsedMilliseconds}");

                    for (int j = 0; j < 10; j++)
                    {
                        sw2.Restart();
                        var vehicleMovement = new VehicleMovementMessage() { Vehicle = response.Vehicle, ActionTime = DateTime.UtcNow.ToString(), ActionType = "Test" };
                        client.AddMovement(vehicleMovement);
                        sw2.Stop();
                        Console.WriteLine($"Created Vehicle Movement. {DateTime.Now}, {sw2.ElapsedMilliseconds}");
                    }
                }

                var addedVehicles = client.GetVehicles(new QueryRequest());
                Console.WriteLine($"Total {addedVehicles.Vehicles.Count} vehicles fetched");
                foreach (var addedVehicle in addedVehicles.Vehicles)
                {
                    sw2.Restart();
                    client.DeleteVehicle(addedVehicle);
                    sw2.Stop();
                    Console.WriteLine($"Deleted Vehicle {addedVehicle.Id} - {DateTime.Now}, {sw2.ElapsedMilliseconds}");
                }
            }

            sw.Stop();
            Console.WriteLine($"Time taken = {sw.ElapsedMilliseconds}");
            Console.WriteLine("END");
        }
    }
}
