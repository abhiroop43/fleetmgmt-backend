using System;
using System.ComponentModel.DataAnnotations;

namespace FleetMgmt.Data.Entities
{
    public class Trip : BaseEntity
    {
        [Required]
        public long Length { get; set; }

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
