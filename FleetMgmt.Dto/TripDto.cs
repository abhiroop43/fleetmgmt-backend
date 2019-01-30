using FleetMgmt.Data.Entities;
using System;
using System.ComponentModel.DataAnnotations;

namespace FleetMgmt.Dto
{
    public class TripDto
    {
        public Guid Id { get; set; }

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
