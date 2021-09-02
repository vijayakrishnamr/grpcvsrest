using System;
using System.ComponentModel.DataAnnotations;

namespace VehicleTracker.Entities
{
    public class VehicleMovement
    {
        [Key]
        public Guid Id { get; set; }
        public Vehicle Vehicle { get; set; }
        [MaxLength(50)]
        public string ActionType { get; set; }
        public DateTime ActionTime { get; set; }
    }
}
