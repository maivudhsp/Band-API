using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Models
{
    public class BandForCreatingDto
    {
        public string Name { get; set; }
        public string FoundYearsAgo { get; set; }
        public string MainGenre { get; set; } 
    }
}