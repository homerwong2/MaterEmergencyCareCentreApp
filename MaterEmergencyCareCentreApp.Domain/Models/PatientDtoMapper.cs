using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class PatientDtoMapper
    {
        public static Patient MapFromDto(PatientDto commentDto)
        {
            return new Patient()
            {
                Id = 0,
                Name = commentDto.Name,
                URN = commentDto.URN,
                DOB = commentDto.DOB,
                BedId = commentDto.BedId,
                PresentingIssue = commentDto.PresentingIssue,
                Comments = new List<Comment>()
            };
        }
    }
}
