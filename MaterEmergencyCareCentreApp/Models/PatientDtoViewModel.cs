namespace MaterEmergencyCareCentreApp.Models
{
    public class PatientDtoViewModel
    {
        public PatientDtoViewModel(int patientId, string? patient)
        {
            PatientId = patientId;
            Patient = patient;
        }

        public int PatientId { get; set; }
        public string? Patient { get; set; }
    }
}
