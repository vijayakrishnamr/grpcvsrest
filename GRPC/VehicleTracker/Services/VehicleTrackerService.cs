using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VehicleTracker.DbContexts;
using VehicleTracker.Entities;
using VehicleTracker.Repository;

namespace VehicleTracker
{
    public class VehicleTrackerService : TrackerService.TrackerServiceBase
    {
        private readonly ILogger<VehicleTrackerService> _logger;
        private readonly IVehicleTrackerRepository _repository;
        private readonly IMapper _mapper;
        public VehicleTrackerService(ILogger<VehicleTrackerService> logger, IVehicleTrackerRepository repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        public override Task<CreateVehicleReply> CreateVehicle(VehicleMessage request, ServerCallContext context)
        {
            var vehicle = _mapper.Map<Vehicle>(request);
            _repository.CreateVehicle(vehicle);
            _repository.Save();
            request.Id = vehicle.Id.ToString();
            return Task.FromResult(new CreateVehicleReply {CreationSuccess = true, Vehicle = request});
        }

        public override Task<AllVehiclesReply> GetVehicles(QueryRequest request, ServerCallContext context)
        {
            var vehicles = _repository.GetVehicles();
            var allVehicles = new AllVehiclesReply();
            allVehicles.Vehicles.AddRange(_mapper.Map<IList<VehicleMessage>>(vehicles));
            return Task.FromResult(allVehicles);
        }

        public override Task<DeleteVehicleReply> DeleteVehicle(VehicleMessage request, ServerCallContext context)
        {
            _repository.DeleteVehicle(_mapper.Map<Vehicle>(request));
            _repository.Save();
            return Task.FromResult(new DeleteVehicleReply { DeletionSuccess = true });
        }

        public override Task<CreateVehicleMovementReply> AddMovement(VehicleMovementMessage request, ServerCallContext context)
        {
            _repository.AddMovement(_mapper.Map<VehicleMovement>(request));
            _repository.Save();
            return Task.FromResult(new CreateVehicleMovementReply { CreationSuccess = true });
        }
    }
}
