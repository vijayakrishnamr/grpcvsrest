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
        private readonly VehicleTrackerContext _context;

        public VehicleTrackerRepository(VehicleTrackerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public void CreateVehicle(Vehicle vehicle)
        {
            vehicle .Id = Guid.NewGuid();
            _context.Vehicles.Add(vehicle);
        }

        public IList<Vehicle> GetVehicles()
        {
            return _context.Vehicles.ToList();
        }

        public bool DeleteVehicle(Vehicle vehicle)
        {
            var vehicleToDelete = _context.Vehicles.FirstOrDefault(v => v.Id == vehicle.Id);
            if (vehicleToDelete == null)
            {
                return false;
            }

            var status = _context.Vehicles.Remove(vehicleToDelete);
            return true;
        }

        public void AddMovement(VehicleMovement vehicleMovement)
        {
            vehicleMovement.Id = Guid.NewGuid();
            vehicleMovement.Vehicle = _context.Vehicles.FirstOrDefault(v => v.Id == vehicleMovement.Vehicle.Id);
            _context.VehicleMovements.Add(vehicleMovement);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
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
