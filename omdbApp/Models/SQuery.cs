using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace omdbApp.Models
{
    public class SQuery
    {
        public List<SQueryInner> Search { get; set; }
        public string totalResults { get; set; }
        public bool Response { get; set; }
        public string Error { get; set; }
    }
}
