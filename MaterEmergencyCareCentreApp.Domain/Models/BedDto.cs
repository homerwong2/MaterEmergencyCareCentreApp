﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class BedDto
    {
        public int Bed{ get; set; }
        public string Status { get; set; }
        public int? PatientId { get; set; }
        public string? Patient { get; set; }
        public string? DOB { get; set; }
        public string? PresentingIssue { get; set; }
        public string? LastComment{ get; set; }
        public string? LastUpdate { get; set; }
        public string? URN { get; set; }
        public string? Nurse { get; set; }
        public string? Action { get; set; }
    }
}
