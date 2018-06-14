using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace omdbApp.Models
{
    public class TVShow: MediaItem
    {
        public List<Episode> Episodes { get; set; }
    }
}
