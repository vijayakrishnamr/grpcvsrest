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
                for (int i = 0; i < 100; i++)
                {


                    var vehicleReg = Guid.NewGuid();
                    Console.WriteLine($"Create new Vehicle {vehicleReg}");
                    var vehicleMessage = new VehicleMessage() { Registration = vehicleReg.ToString(), VehicleType = "Car" };
                    var response = client.CreateVehicle(vehicleMessage);
                    vehicleMessage.Id = response.Vehicle.Id;
                    Console.WriteLine($"Created Vehicle with Id {response.Vehicle.Id} - {DateTime.Now}");

                    for (int j = 0; j < 10; j++)
                    {
                        var vehicleMovement = new VehicleMovementMessage() { Vehicle = vehicleMessage, ActionTime = DateTime.UtcNow.ToString(), ActionType = "Test" };
                        client.AddMovement(vehicleMovement);
                        Console.WriteLine($"Created Vehicle Movement. {DateTime.Now}");
                    }
                }

                var addedVehicles = client.GetVehicles(new QueryRequest());
                foreach (var addedVehicle in addedVehicles.Vehicles)
                {
                    client.DeleteVehicle(addedVehicle);
                    Console.WriteLine($"Deleted Vehicle {addedVehicle.Id} - {DateTime.Now}");
                }
            }

            sw.Stop();
            Console.WriteLine($"Time taken = {sw.ElapsedMilliseconds}");
            Console.WriteLine("END");
        }
    }
}
