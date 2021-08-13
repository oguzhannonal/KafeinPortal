using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafeinPortal.Core.Requests
{
    public class ProjectRequest
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
    }
}
