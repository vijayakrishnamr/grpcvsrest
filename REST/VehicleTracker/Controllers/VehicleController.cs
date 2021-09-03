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
    [Route("api/vehicles")]
    public class VehicleController : ControllerBase
    {
        private readonly ILogger<VehicleController> _logger;
        private readonly IVehicleTrackerRepository _repository;
        private readonly IMapper _mapper;

        public VehicleController(ILogger<VehicleController> logger, IVehicleTrackerRepository repository,
            IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<VehicleDto>> GetVehicles()
        {
            var vehiclesFromRepo = _repository.GetVehicles();
            return Ok(_mapper.Map<IEnumerable<VehicleDto>>(vehiclesFromRepo));
        }

        [HttpGet("{vehicleId}", Name = "GetVehicle")]
        public ActionResult<VehicleDto> GetVehicle(Guid vehicleId)
        {
            var vehiclesFromRepo = _repository.GetVehicle(vehicleId);
            return Ok(_mapper.Map<VehicleDto>(vehiclesFromRepo));
        }

        [HttpPost]
        public ActionResult<VehicleDto> CreateVehicle(VehicleDto vehicle)
        {
            var vehicleForCreation = _mapper.Map<Vehicle>(vehicle);
            _repository.CreateVehicle(vehicleForCreation);
            _repository.Save();
            var vehicleToReturn = _mapper.Map<VehicleDto>(vehicleForCreation);
            return CreatedAtRoute("GetVehicle", new { vehicleId = vehicleForCreation.Id }, vehicleToReturn);
        }

        [HttpDelete]
        public ActionResult DeleteVehicle(VehicleDto vehicle)
        {
            _repository.DeleteVehicle(_mapper.Map<Vehicle>(vehicle));
            _repository.Save();
            return Ok();
        }
    }
}
