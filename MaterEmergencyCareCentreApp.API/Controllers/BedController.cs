using Microsoft.AspNetCore.Mvc;
using MaterEmergencyCareCentreApp.Domain.Models;
using MaterEmergencyCareCentreApp.DataAccess;
using MaterEmergencyCareCentreApp.Domain.DTOs;

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
        public ActionResult<bool> AddComment(CommentDto commentDto)
        {
            return Ok(_bedRepository.AddComment(commentDto));
        }

        [HttpPost("AdmitPatient")]
        public ActionResult<bool> AdmitPatient(PatientDto patientDto)
        {
            return Ok(_bedRepository.AdmitPatient(patientDto));
        }

        [HttpPost("DischargePatient")]
        public ActionResult<bool> DischargePatient(DischargeDto dischargeDto)
        {
            return Ok(_bedRepository.DischargePatient(dischargeDto));
        }
    }
}