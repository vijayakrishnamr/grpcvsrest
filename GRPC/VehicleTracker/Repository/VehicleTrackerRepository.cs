using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.DbContexts;
using VehicleTracker.Entities;

namespace VehicleTracker.Repository
{
    public class VehicleTrackerRepository: IVehicleTrackerRepository, IDisposable
    {
        public void CreateVehicle(Vehicle vehicle)
        {
            vehicle .Id = Guid.NewGuid();
        }

        public IList<Vehicle> GetVehicles()
        {
            IList<Vehicle> vehicles = new List<Vehicle>(100);
            for (int i = 0; i < 100; i++)
            {
                vehicles.Add(new Vehicle(){Id = Guid.NewGuid(), Registration = Guid.NewGuid().ToString(), VehicleType = "Car"});
            }
            return vehicles;
        }

        public bool DeleteVehicle(Vehicle vehicle)
        {
            return true;
        }

        public void AddMovement(VehicleMovement vehicleMovement)
        {
            vehicleMovement.Id = Guid.NewGuid();
        }

        public bool Save()
        {
            return true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // dispose resources when needed
            }
        }
    }
}
