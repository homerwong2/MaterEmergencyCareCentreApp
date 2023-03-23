using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class BedDtoMapper
    {
        public static BedDto MapToDto(Bed bed, Patient? patient)
        {
            return new BedDto()
            {
                Bed = bed.Id,
                Status = bed.Status,
                PatientId = patient?.Id,
                Patient = patient?.Name,
                DOB = patient?.DOB.ToString("dd-MMM-yyyy"),
                PresentingIssue = patient?.PresentingIssue,
                LastComment = patient?.Comments.OrderBy(c => c.CommentTime).Last<Comment>().Text,
                LastUpdate = patient?.Comments.OrderBy(c => c.CommentTime).Last<Comment>().CommentTime.ToString("dd-MMM-yyyy HH:mm:ss "),
                URN = patient?.URN,
                Nurse = patient?.Comments.OrderBy(c => c.CommentTime).Last<Comment>().Nurse,
                Action = ""
            };
        }
    }
}
