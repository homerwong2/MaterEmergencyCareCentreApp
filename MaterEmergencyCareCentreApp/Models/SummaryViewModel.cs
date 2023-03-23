using MaterEmergencyCareCentreApp.Domain.Models;

namespace MaterEmergencyCareCentreApp.Models
{
    public class SummaryViewModel
    {
        public List<BedDtoViewModel> Beds { get; set; }
        public int CountOfBedsInUse { get; set; }
        public int CountOfBedsFree { get; set; }
        public int CountTotalPatientsAdmittedToday { get; set; }
        public List<Patient> AdmittedPatientsUsingABed { get; set; }
    }
}
