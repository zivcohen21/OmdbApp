using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace omdbApp.Models
{
    public class ResponseData : MediaItem
    {
        public List<MediaItem> Search { get; set; }
        public int totalResults { get; set; }
        public bool Response { get; set; }

    }
}
