using Microsoft.AspNetCore.Mvc;
using Logistic.Core;
using Logistic.Models;
using Logistic.Core.Services;
using Logistic.DAL.DataBase;
using Logistic.WebAPI.Models;
using Logistic.WebAPI;
using AutoMapper;

namespace Logistic.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class VehicleController : ControllerBase
    {
        public readonly VehicleService _vehicleService;
        private readonly IMapper _mapper;
        public VehicleController(IMapper mapper, VehicleService vehicleservice)
        {
            _vehicleService = vehicleservice;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetAllVehicles")]
        public IEnumerable<Vehicle> Get()
        {
            return _vehicleService.GetAll();
        }

        [HttpPost]
        public IActionResult Create (VehicleModel vehicleModel)
        {
            var vehicle = _mapper.Map<Vehicle>(vehicleModel);
            _vehicleService.Create(vehicle);
            return Accepted();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete (int Id)
        {
            var deletedVehicle = _vehicleService.GetById(Id);
            if (deletedVehicle != null)
            {
                _vehicleService.Delete(deletedVehicle.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{Id},Update")]
        public IActionResult Update (int Id, VehicleModel vehicleModel)
        {
            var updatedVehicle = _vehicleService.GetById(Id);
            if (updatedVehicle != null)
            {
                updatedVehicle.Name = vehicleModel.Name;
                updatedVehicle.MaxCargoWeightKg = vehicleModel.MaxCargoWeightKg;
                updatedVehicle.MaxCargoVolume = vehicleModel.MaxCargoVolume;
                _vehicleService.Update(updatedVehicle);
                return Accepted();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{Id}, /LoadCargo")]
        public IActionResult LoadCargo(Cargo cargo, int Id)
        {
            try
            {
                _vehicleService.LoadCargo(cargo, Id);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut("{vehicleId}/UnloadCargo/{cargoId}")]
        public IActionResult UnLoadCargo(Guid cargoId, int vehicleId)
        {
            try
            { 
                _vehicleService.UnloadCargo(cargoId, vehicleId);
                return Accepted();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}