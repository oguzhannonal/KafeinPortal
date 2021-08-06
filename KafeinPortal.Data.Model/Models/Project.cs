using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Model.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        
        
        public virtual ProjectDetail ProjectDetails{ get; set; }
        public virtual Customer Customer { get; set; }
    }
}
