using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafeinPortal.Data.Model.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
        
       public virtual IList<Project> Projects { get; set; }
        

    }
}
