﻿using System;
using System.ComponentModel.DataAnnotations;

namespace FleetMgmt.Data.Entities
{
    public class Vehicle : BaseEntity
    {
        [Required]
        [MaxLength(255)]
        public string Make { get; set; }

        [Required]
        [MaxLength(255)]
        public string Model { get; set; }

        [Required]
        [MaxLength(100)]
        public string Color { get; set; }

        [Required]
        [MaxLength(100)]
        public string PlateNumber { get; set; }

        public string EngineNumber { get; set; }

        public string ChassisNumber { get; set; }

        public bool IsActive { get; set; } = true;

        public long? KilometersDriven { get; set; }
    }
}
