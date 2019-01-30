using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FleetMgmt.Data.Entities;

namespace FleetMgmt.Dto
{
    public class AccidentDto
    {
        public Guid Id { get; set; }

        [Required]
        public Guid TripId { get; set; }

        [Required]
        public DateTime AccidentTime { get; set; }

        [Required]
        public bool IsOwnFault { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Fine { get; set; }

        [Required]
        public bool CoveredByInsurance { get; set; }

        public Trip Trip { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
