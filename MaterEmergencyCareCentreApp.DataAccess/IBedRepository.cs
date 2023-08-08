using MaterEmergencyCareCentreApp.Domain.DTOs;
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
        public bool AddComment(CommentDto commentDto);
        public bool AdmitPatient(PatientDto patientDto);
        public bool DischargePatient(DischargeDto dischargeDto);
    }
}
