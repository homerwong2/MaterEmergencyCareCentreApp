using MaterEmergencyCareCentreApp.Domain.Models;

namespace MaterEmergencyCareCentreApp.DataAccess
{
    public interface IBedRepository
    {
        public List<BedDto> GetBeds();
        public int CountBedsInUse();
        public int CountBedsFree();
        public int CountTotalPatientsAdmittedToday();
        public List<Patient> GetPatients();
        public List<Patient> GetAdmittedPatientsUsingABed();
        public List<Comment> GetComments(int patientId);
        public bool AddComment(int patientId, DateTime commentTime, string text, string nurse);
        public bool AdmitPatient(Patient patient, int bedId);
        public bool DischargePatient(int patientId);
    }
}
