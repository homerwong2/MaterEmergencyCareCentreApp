using Microsoft.AspNetCore.Mvc;
using MaterEmergencyCareCentreApp.Domain.Models;
using MaterEmergencyCareCentreApp.DataAccess;

namespace MaterEmergencyCareCentreApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BedController : ControllerBase
    {
        private readonly IBedRepository _bedRepository;

        public BedController(IBedRepository bedRepository)
        {
            _bedRepository = bedRepository;
        }

        [HttpGet("GetBeds")]
        public ActionResult<IEnumerable<Bed>> Get()
        {
            return Ok(_bedRepository.GetBeds());
        }

        [HttpGet("CountBedsInUse")]
        public ActionResult<int> CountBedsInUse()
        {
            return Ok(_bedRepository.CountBedsInUse());
        }

        [HttpGet("CountBedsFree")]
        public ActionResult<int> CountBedsFree()
        {
            return Ok(_bedRepository.CountBedsFree());
        }

        [HttpGet("CountTotalPatientsAdmittedToday")]
        public ActionResult<int> CountTotalPatientsAdmittedToday()
        {
            return Ok(_bedRepository.CountTotalPatientsAdmittedToday());
        }

        [HttpGet("GetPatients")]
        public ActionResult<IEnumerable<Patient>> GetPatients()
        {
            return Ok(_bedRepository.GetPatients());
        }

        [HttpGet("GetAdmittedPatientsUsingABed")]
        public ActionResult<int> GetAdmittedPatientsUsingABed()
        {
            return Ok(_bedRepository.GetAdmittedPatientsUsingABed());
        }

        [HttpGet("GetComments")]
        public ActionResult<IEnumerable<Comment>> GetComments(int patientId)
        {
            return Ok(_bedRepository.GetComments(patientId));
        }

        [HttpPost("AddComment")]
        public ActionResult<int> AddComment(int patientId, DateTime commentTime, string text, string nurse)
        {
            return Ok(_bedRepository.AddComment(patientId, commentTime, text, nurse));
        }

        [HttpPut("AdmitPatient")]
        public ActionResult<int> AdmitPatient(Patient patient, int bedId)
        {
            return Ok(_bedRepository.AdmitPatient(patient, bedId));
        }

        [HttpPut("DischargePatient")]
        public ActionResult<int> DischargePatient(int patientId)
        {
            return Ok(_bedRepository.DischargePatient(patientId));
        }
    }
}