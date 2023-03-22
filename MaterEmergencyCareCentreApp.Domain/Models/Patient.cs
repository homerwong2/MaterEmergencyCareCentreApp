namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URN{ get; set; }
        public DateTime DOB { get; set; }
        public int? BedId { get; set; }
        public string PresentingIssue { get; set; }
        public List<Comment> Comments { get; set; }
        public DateTime DateAdmitted  { get; set; }
    }
}

