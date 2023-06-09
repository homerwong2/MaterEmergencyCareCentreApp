﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MaterEmergencyCareCentreApp.Domain.Models
{
    public class CommentDtoMapper
    {
        public static Comment MapFromDto(CommentDto commentDto)
        {
            return new Comment()
            {
                Id = 0,
                CommentTime = commentDto.CommentTime,
                Text = commentDto.Text,
                Nurse = commentDto.Nurse
            };
        }
    }
}
