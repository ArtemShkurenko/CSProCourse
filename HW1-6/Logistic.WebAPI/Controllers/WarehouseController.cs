using AutoMapper;
using Logistic.Core.Services;
using Logistic.Models;
using Logistic.WebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WarehouseController : ControllerBase
    {
        private readonly WarehouseService _warehouseService;
        private readonly IMapper _mapperWh;
        private readonly ILogger<WarehouseService> _logger;
        
        public WarehouseController(ILogger<WarehouseController> logger, WarehouseService warehouseService, IMapper mapperWh)
        {
            _logger = (ILogger<WarehouseService>?)logger;
            _warehouseService = warehouseService;
            _mapperWh = mapperWh;
        }

        [HttpGet(Name = "GetAllWarehouses")]
        public IEnumerable<Warehouse> Get()
        {
            return _warehouseService.GetAll();
        }

        [HttpPost]
        public IActionResult Create(WarehouseModel warehouseModel)
        {
            var warehouse = _mapperWh.Map<Warehouse>(warehouseModel);
            _warehouseService.Create(warehouse);
            return Accepted();
        }
        [HttpDelete("{Id}")]
        public IActionResult Delete(int Id)
        {
            var deletedWarehouse = _warehouseService.GetById(Id);
            if (deletedWarehouse != null)
            {
                _warehouseService.Delete(deletedWarehouse.Id);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPut("{Id},Update")]
        public IActionResult Update(int Id, Warehouse warehouseModel)
        {
            try
            {
                var updatedWarehouse = _warehouseService.GetById(Id);
                updatedWarehouse.Name = warehouseModel.Name;

                _warehouseService.Update(updatedWarehouse);
                return Accepted();
            }
            catch (Exception ex)
            {
                return NotFound();
            }
        }
    }
}
