using System;

namespace FleetMgmt.Domain.Exceptions
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}
