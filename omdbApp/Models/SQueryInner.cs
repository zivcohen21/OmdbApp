using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace omdbApp.Models
{
    public class SQueryInner
    {
        public string imdbID { get; set; }
        public string Title { get; set; }
        public string Released { get; set; }
        public string Type { get; set; }
        public string Poster { get; set; }
    }
}
