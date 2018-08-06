using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FleetMgmt.Data.Entities
{
    public class Trip
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public int Length { get; set; }

        [Required]
        public Guid VehicleId { get; set; }

        [Required]
        public Guid DiverId { get; set; }

        [Required]
        public DateTime TripDate { get; set; }

        public Vehicle Vehicle { get; set; }
        public Driver Driver { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
