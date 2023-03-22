namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class Bed
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int? PatientId { get; set; }
    }
}

