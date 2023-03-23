namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class PatientDto
    {
        public string Name { get; set; }
        public string URN{ get; set; }
        public DateTime DOB { get; set; }
        public int? BedId { get; set; }
        public string PresentingIssue { get; set; }
        public DateTime DateAdmitted  { get; set; }
        public string Nurse { get; set; }

    }
}

