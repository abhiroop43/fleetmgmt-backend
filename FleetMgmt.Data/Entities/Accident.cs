using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FleetMgmt.Data.Entities
{
    public class Accident
    {
        [Key]
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
