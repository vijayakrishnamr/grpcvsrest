using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VehicleTracker.Entities;

namespace VehicleTracker.Profiles
{
    public class VehicleMovementProfile: Profile
    {
        public VehicleMovementProfile()
        {
            CreateMap<VehicleMovementMessage, VehicleMovement>().ForMember(
                dest => dest.Id,
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.Id) ? Guid.Empty : Guid.Parse(src.Id))); ;
        }
    }
}
