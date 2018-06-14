using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using omdbApp.Models;

namespace omdbApp.Models
{
    public class Movie : MediaItem
    {
        public string FullPlot { get; set; }
    }
}
