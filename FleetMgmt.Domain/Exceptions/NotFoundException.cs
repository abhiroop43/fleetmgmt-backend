using System;

namespace FleetMgmt.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message)
        {
            Message = message;
        }

        public override string Message { get; }
    }
}
