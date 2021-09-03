using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.Extensions.Logging;
using VehicleTracker.Entities;
using VehicleTracker.Models;
using VehicleTracker.Repository;

namespace VehicleTracker.Controllers
{
    [ApiController]
    [Route("api/vehiclemovements")]
    public class VehicleMovementController : ControllerBase
    {
        private readonly ILogger<VehicleMovementController> _logger;
        private readonly IVehicleTrackerRepository _repository;
        private readonly IMapper _mapper;

        public VehicleMovementController(ILogger<VehicleMovementController> logger, IVehicleTrackerRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("{vehicleMovementId}", Name = "GetVehicleMovement")]
        public ActionResult<VehicleMovementDto> GetVehicleMovement(Guid vehicleMovementId)
        {
            var vehicleMovementFromRepo = _repository.GetVehicleMovement(vehicleMovementId);
            return Ok(_mapper.Map<VehicleMovementDto>(vehicleMovementFromRepo));
        }

        [HttpPost]
        public ActionResult<VehicleDto> CreateVehicleMovement(VehicleMovementDto vehicleMovement)
        {
            var vehicleMovementForCreation = _mapper.Map<VehicleMovement>(vehicleMovement);
            _repository.AddMovement(vehicleMovementForCreation);
            _repository.Save();
            var vehicleMovementToReturn = _mapper.Map<VehicleMovementDto>(vehicleMovementForCreation);
            return CreatedAtRoute("GetVehicleMovement", new { vehicleMovementId = vehicleMovementToReturn.Id }, vehicleMovementToReturn);
        }
    }
}
