using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Model.Models
{
    public class ProjectDetail
    {
        public int Id { get; set; } //isimler id degisecek
        public int ProjectId { get; set; }
        public string ShortDescription { get; set; }
        public string BackendTech { get; set; }
        public string FrontendTech { get; set; }
        public string DatabaseTech { get; set; }
        public int Year { get; set; }
        public Enums.ProductionTime ProductionTime { get; set; } //enum olabilir 
        public Enums.ProjectSize ProjectSize { get; set; } // enum olabilir 

        [NotMapped]
        public virtual Project Project { get; set; }
    }
}
