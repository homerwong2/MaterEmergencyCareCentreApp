using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public DateTime CommentTime { get; set; }
        public string Text { get; set; }
        public string Nurse { get; set; }

    }
}
