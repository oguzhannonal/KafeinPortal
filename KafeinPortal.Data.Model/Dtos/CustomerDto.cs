﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Model.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        public  IEnumerable<ProjectDto> Projects { get; set; }

    }
}
