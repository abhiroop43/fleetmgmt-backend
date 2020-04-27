using System;
using System.Collections.Generic;
using System.Text;

namespace FleetMgmt.Dto
{
    public class SearchInputDto
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 15;
        public List<KeyValuePair<string, dynamic>> Filters { get; set; }
    }
}
