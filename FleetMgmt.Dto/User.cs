using System;
using System.Collections.Generic;
using System.Text;

namespace FleetMgmt.Dto
{
    public class User
    {
        public string Name { get; set; }

        public string[] UserGroups { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
