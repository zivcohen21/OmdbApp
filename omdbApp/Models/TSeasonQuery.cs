using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace omdbApp.Models
{
    public class TSeasonQuery
    {
        public string Title { get; set; }
        public List<Episode> Episodes { get; set; }
    }
}
