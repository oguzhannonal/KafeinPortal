using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Model.Dtos
{
    public class ProjectDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public  ProjectDetailsDto ProjectDetails { get; set; }

    }
}
