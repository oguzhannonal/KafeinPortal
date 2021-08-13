using KafeinPortal.Core.Requests;
using KafeinPortal.Data.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafeinPortal.Core.Responses
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string Name { get; set; }
        

    }
}
