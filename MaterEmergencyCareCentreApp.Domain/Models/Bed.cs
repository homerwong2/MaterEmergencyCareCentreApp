using MaterEmergencyCareCentreApp.Domain.Enums;

namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class Bed
    {
        public int Id { get; set; }
        public StatusType Status { get; set; }
        public int? PatientId { get; set; }
    }
}

