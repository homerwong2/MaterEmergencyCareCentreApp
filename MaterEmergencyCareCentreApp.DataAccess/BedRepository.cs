using MaterEmergencyCareCentreApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace MaterEmergencyCareCentreApp.DataAccess
{
    public class BedRepository : IBedRepository
    {
        private readonly BedApiContext _context;
        private int _nextPatientId;
        private int _nextCommentId;
        public BedRepository(BedApiContext bedApiContext)
        {
            _context = bedApiContext;
            AddTestData();
        }

        public List<BedDto> GetBeds()
        {
            var bedDtos = _context.Beds
                .Select(b => BedDtoMapper.MapToDto(b, GetPatient(b.PatientId)))
                .ToList();
            return bedDtos;
        }

        public int CountBedsInUse()
        {
            var count = _context.Beds.Where(b => b.Status == "In use").Count();
            return count;
        }
        public int CountBedsFree()
        {
            var count = _context.Beds.Where(b => b.Status == "Free").Count();
            return count;
        }

        public int CountTotalPatientsAdmittedToday()
        {
            var count = _context.Patients.Where(p => p.DateAdmitted.Date == DateTime.Today).Count();
            return count;
        }

        public Bed? GetBed(int bedId)
        {
            var bed = _context.Beds.Find(bedId);

            return bed;
        }

        public Patient? GetPatient(int? patientId)
        {
            var patient = _context.Patients
                .Include(p => p.Comments)
                .Where(p => p.Id == patientId)
                .FirstOrDefault();

            return patient;
        }

        public List<Patient> GetPatients()
        {
            var patients = _context.Patients
                .Include(p => p.Comments)
                .ToList();

            return patients;
        }


        public List<Patient> GetAdmittedPatientsUsingABed()
        {
            var patientIds = _context.Beds
                .Where(b => b.Status == "In use")
                .Select(b => b.PatientId)
                .ToList();

            var patients = _context.Patients
                .Where(p => patientIds.Contains(p.Id))
                .ToList();

            return patients;
        }

        public List<Comment> GetComments(int patientId)
        {
            var patient = _context.Patients
                .Include(p => p.Comments)
                .Where(p => p.Id == patientId)
                .FirstOrDefault();

            return patient == null ? new List<Comment>() : patient.Comments;
        }


        public bool AddComment(CommentDto commentDto)
        {
            var comment = CommentDtoMapper.MapFromDto(commentDto);
            comment.Id = GetNextCommentId();

            var patient = GetPatient(commentDto.PatientId);
            patient?.Comments.Add(comment);

            _context.Comments.Add(comment);
            _context.SaveChanges();

            return true;
        }

        public bool AdmitPatient(PatientDto patientDto)
        {
            var patient = PatientDtoMapper.MapFromDto(patientDto);
            var bed = GetBed((int)patientDto.BedId);

            if (bed is null)
                return false;   // bed not found

            if (bed.Status == "In use")
                return false;   // need to discharge patient first

            var patientId = GetNextPatientId();
            bed.PatientId = patientId;
            bed.Status = "In use";
            patient.Id = patientId;

            _context.Patients.Add(patient);
            _context.SaveChanges();

            var result = AddComment(new CommentDto()
            {
                PatientId = patient.Id,
                Patient = patient.Name,
                CommentTime = DateTime.Now,
                Text = "Admitted",
                Nurse = patientDto.Nurse
            });

            return true;
        }

        public bool DischargePatient(DischargeDto dischargeDto)
        {
            var patient = GetPatient(dischargeDto.PatientId);
            if (patient is null)
                return false;   // no patient to discharge
            if (patient.BedId is null)
                return false;   // no patient to discharge

            var bed = GetBed((int)patient.BedId);
            if (bed is null)
                return false;   // no bed to discharge patient from
            bed.Status = "Free";
            bed.PatientId = null;
            patient.BedId = null;

            _context.SaveChanges();

            var result = AddComment(new CommentDto()
            {
                PatientId = dischargeDto.PatientId,
                Patient = dischargeDto.Patient,
                CommentTime = DateTime.Now,
                Text = "Discharged",
                Nurse = dischargeDto.Nurse
            });

            return true;
        }

        public int GetNextPatientId()
        {
            var patientIds = _context.Patients
                .Select(p => p.Id)
                .OrderBy(p => p)
                .ToList();

            _nextPatientId = patientIds.LastOrDefault();

            return ++_nextPatientId;
        }

        public int GetNextCommentId()
        {
            var commentIds = _context.Comments
                .Select(c => c.Id)
                .OrderBy(c => c)
                .ToList();

            _nextCommentId = commentIds.LastOrDefault();

            return ++_nextCommentId;
        }


        public void AddTestData()
        {
            if (_context.Beds.Count() == 0)
            {
                var beds = new List<Bed>
                    {
                    new Bed{Id=1,Status="In use",PatientId=1},
                    new Bed{Id=2,Status="Free",PatientId=null},
                    new Bed{Id=3,Status="Free",PatientId=null},
                    new Bed{Id=4,Status="Free",PatientId=null},
                    new Bed{Id=5,Status="In use",PatientId=2},
                    new Bed{Id=6,Status="In use",PatientId=3},
                    new Bed{Id=7,Status="Free",PatientId=null},
                    new Bed{Id=8,Status="Free",PatientId=null}
                    };
                _context.Beds.AddRange(beds);
                _context.SaveChanges();
            }

            if (_context.Patients.Count() == 0)
            {
                var patients = new List<Patient>
                {
                    new Patient{Id=1,Name="John Doe",URN="0083524",DOB=DateTime.Parse("01/01/1980"),BedId=1,PresentingIssue="Nausea, dizziness",
                        Comments = new List<Comment>()
                        {
                            new Comment{Id=1,CommentTime=DateTime.Parse("02/02/2020 09:50:00"),Text="Admitted",Nurse="Kelly A."},
                            new Comment{Id=2,CommentTime=DateTime.Parse("02/02/2020 09:55:00"),Text="Temp checked",Nurse="Mary P."},
                            new Comment{Id=3,CommentTime=DateTime.Parse("02/02/2020 10:25:00"),Text="Blood pressure checked",Nurse="Mary P."},
                            new Comment{Id=4,CommentTime=DateTime.Parse("02/02/2020 10:35:00"),Text="Discharged",Nurse="Kelly A."}
                        }
                    },
                    new Patient
                    {
                        Id = 2,
                        Name = "Lorna Smith",
                        URN = "001154",
                        DOB = DateTime.Parse("15/03/1995"),
                        BedId = 5,
                        PresentingIssue = "Broken leg",
                        Comments = new List<Comment>()
                        {
                            new Comment{Id=5,CommentTime=DateTime.Parse("02/02/2020 09:50:00"),Text="Admitted",Nurse="Kelly A."},
                            new Comment{Id=6,CommentTime=DateTime.Parse("02/02/2020 09:55:00"),Text="Temp checked",Nurse="Mary P."},
                            new Comment{Id=7,CommentTime=DateTime.Parse("02/02/2020 10:25:00"),Text="Blood pressure checked",Nurse="Mary P."},
                            new Comment{Id=8,CommentTime=DateTime.Parse("02/02/2020 10:35:00"),Text="X-Ray waiting results",Nurse="Kelly A."}
                        }
                    },
                    new Patient
                    {
                        Id = 3,
                        Name = "Diana May",
                        URN = "0877341",
                        DOB = DateTime.Parse("23/11/1972"),
                        BedId = 6,
                        PresentingIssue = "High fever",
                        Comments = new List<Comment>()
                        {
                            new Comment{Id=9,CommentTime=DateTime.Parse("02/02/2020 09:50:00"),Text="Admitted",Nurse="Kelly A."},
                            new Comment{Id=10,CommentTime=DateTime.Parse("02/02/2020 09:55:00"),Text="Temp checked",Nurse="Mary P."},
                            new Comment{Id=11,CommentTime=DateTime.Parse("02/02/2020 10:25:00"),Text="Blood pressure checked",Nurse="Mary P."},
                            new Comment{Id=12,CommentTime=DateTime.Parse("02/02/2020 10:35:00"),Text="Medication supplied",Nurse="Kelly A."}
                        }
                    },
                };
                _context.Patients.AddRange(patients);
                //if (_context.Comments.Count() == 0)
                //{
                _context.Comments.AddRange(
                    _context.Patients
                        .Select(p => p.Comments)
                        .SelectMany(c => c)
                );
                //}
                _context.SaveChanges();
                _nextCommentId = 13;
                _nextPatientId = 4;
            }



            //if (_context.Employees.Count() == 0)
            //{
            //    var employees = new List<Employee>
            //    {
            //        new Employee { Id = 1, Name = "Kelly A.", Job = "Nurse" },
            //        new Employee { Id = 2, Name = "Mary P.", Job = "Nurse" },
            //    };
            //    _context.Employees.AddRange(employees);
            //    _context.SaveChanges();
            //}
        }
    }
}

