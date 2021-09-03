using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleTracker.Models
{
    public class VehicleDto
    {
        public Guid Id { get; set; }
        public string Registration { get; set; }
        public string VehicleType { get; set; }
    }
}
