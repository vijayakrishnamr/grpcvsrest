using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleTracker.Entities;

namespace VehicleTracker.DbContexts
{
    public class VehicleTrackerContext : DbContext
    {

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleMovement> VehicleMovements { get; set; }

        public VehicleTrackerContext(DbContextOptions options) : base(options)
        {}

    }
}
