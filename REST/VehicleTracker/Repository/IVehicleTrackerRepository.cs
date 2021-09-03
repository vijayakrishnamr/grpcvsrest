using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleTracker.Entities;

namespace VehicleTracker.Repository
{
    public interface IVehicleTrackerRepository
    {
        void CreateVehicle(Vehicle vehicle);
        IList<Vehicle> GetVehicles();
        Vehicle GetVehicle(Guid vehicleId);
        bool DeleteVehicle(Vehicle vehicle);
        void AddMovement(VehicleMovement vehicleMovement);
        VehicleMovement GetVehicleMovement(Guid vehicleMovementId);
        bool Save();
    }
}
