using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace omdbApp.Models
{
    public class Results
    {
        public List<MediaItem> MediaItems { get; set; }
        public string message { get; set; }
    }
}
