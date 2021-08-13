using KafeinPortal.Data.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KafeinPortal.Core.Requests
{
    public class CustomerRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Sector { get; set; }
    }
}
