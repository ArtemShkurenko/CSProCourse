using Logistic.Core.Services;
using Logistic.Models;
using Microsoft.AspNetCore.Mvc;

namespace Logistic.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly ReportService<Vehicle> _reportService;
        private readonly VehicleService _vehicleService;
        
        public ReportController(ReportService<Vehicle> reportService, VehicleService vehicleService)
        {
            _reportService = reportService;
            _vehicleService = vehicleService;
        }
       
        [HttpPost("{reportType}/CreateReport")]
        public IActionResult CreateReport(ReportType reportType)
        {
            try
            {
                IEnumerable<Vehicle> entities = _vehicleService.GetAll();
                _reportService.CreateReport(reportType, entities);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{reportFilePath},LoadReport")]
        public IActionResult LoadReport(string reportFilePath)
        {
            try
            {
                var reportData = _reportService.LoadReport(reportFilePath);
                return Ok(reportData);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
