using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VehicleTracker.Entities;
using VehicleTracker.Models;

namespace VehicleTracker.Profiles
{
    public class VehicleMovementProfile : Profile
    {
        public VehicleMovementProfile()
        {
            CreateMap<VehicleMovementDto, VehicleMovement>();
            CreateMap<VehicleMovement, VehicleMovementDto>();
        }
    }
}
