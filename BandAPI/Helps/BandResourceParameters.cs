using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BandAPI.Helps
{
    public class BandResourceParameters
    {
        public string MainGenre { get; set; }
        public string SearchQuery { get; set; }

        const int maxPageSize = 3;
        public int PageNumber { get; set; } = 1;

        private int _pageSize = 2;

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
        public string OrderBy { get; set;} = "Name";

    }
}