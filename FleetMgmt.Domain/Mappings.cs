using System;
using AutoMapper;
using FleetMgmt.Data.Entities;
using FleetMgmt.Dto;

namespace FleetMgmt.Domain
{
    public class Mappings : Profile
    {
        public Mappings()
        {
            CreateMap<VehicleDto, Vehicle>().ReverseMap();
        }
    }
}
