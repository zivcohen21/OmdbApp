using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace omdbApp.Models
{
    public class Season
    {
        public string Title { get; set; }
        public int SeasonNumber { get; set; }
        public List<Episode> Episodes { get; set; }
    }
}
