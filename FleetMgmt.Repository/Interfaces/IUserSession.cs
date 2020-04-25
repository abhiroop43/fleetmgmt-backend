using System;
using System.Collections.Generic;
using System.Text;
using FleetMgmt.Dto;

namespace FleetMgmt.Repository.Interfaces
{
    public interface IUserSession
    {
        User GetUser();
    }
}
