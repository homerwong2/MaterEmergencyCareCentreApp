namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class CommentDto
    {
        public CommentDto()
        {
        }

        public int PatientId { get; set; }
        public string? Patient { get; set; }
        public DateTime CommentTime { get; set; }
        public string Text { get; set; }
        public string Nurse { get; set; }
    }
}
