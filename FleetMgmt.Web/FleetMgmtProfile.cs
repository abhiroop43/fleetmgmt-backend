using AutoMapper;
using FleetMgmt.Data.Entities;
using FleetMgmt.Dto;

namespace FleetMgmt.Web
{
    public class FleetMgmtProfile : Profile
    {
        public FleetMgmtProfile()
        {
            CreateMap<Vehicle, VehicleDto>();
            CreateMap<VehicleDto, Vehicle>();

            CreateMap<Driver, DriverDto>();
            CreateMap<DriverDto, Driver>();
        }
    }
}
