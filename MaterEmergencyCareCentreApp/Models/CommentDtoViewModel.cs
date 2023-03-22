namespace MaterEmergencyCareCentreApp.Models
{
    public class CommentDtoViewModel
    {
        public CommentDtoViewModel(int patientId, string? patient)
        {
            PatientId = patientId;
            Patient = patient;
        }

        public int PatientId { get; set; }
        public string? Patient { get; set; }
        public DateTime CommentTime { get; set; }
        public string Text { get; set; }
        public string Nurse { get; set; }
    }
}
