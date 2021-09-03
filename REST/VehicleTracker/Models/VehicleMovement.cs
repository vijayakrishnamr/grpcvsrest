using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTracker.Models
{
    public class VehicleMovementDto
    {
        public Guid Id { get; set; }
        public VehicleDto Vehicle { get; set; }
        public string ActionType { get; set; }
        public string ActionTime { get; set; }
    }
}
