using System;
using System.Collections.Generic;
using System.Text;

namespace FleetMgmt.Dto
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public dynamic Data { get; set; }
        public List<Error> Errors { get; set; }
    }

    public class Error
    {
        public string ErrorCode { get; set; }
        public string ErrorDescription { get; set; }
    }
}
