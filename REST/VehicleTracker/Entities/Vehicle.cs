using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleTracker.Entities
{
    public class Vehicle
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Registration { get; set; }
        [Required]
        [MaxLength(50)]
        public string VehicleType { get; set; }
    }
}
