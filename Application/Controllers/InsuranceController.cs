using Domain.DTO;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceController : ControllerBase
    {
        private readonly IInsuranceService _insuranceService;

        public InsuranceController(IInsuranceService insuranceService)
        {
            _insuranceService = insuranceService;
        }

        [HttpPost("Add")]
        public IActionResult AddInsurance([FromBody] InsuranceDTO insurance)
        {
            try
            {
                _insuranceService.AddInsurance(insurance);
                return Ok("Insurance created");
            }
            catch (CustomValidateException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllInsurances()
        {
            var isnurances = _insuranceService.GetAllInsurances();
            return Ok(isnurances);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetInsuranceById(int id)
        {
            try
            {
                var insurance = _insuranceService.GetInsuranceById(id);
                return Ok(insurance);
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("getByUserId")]
        public IActionResult GetInsurancesByUserId([FromQuery] string userId)
        {
            try
            {
                var insurances = _insuranceService.GetInsurancesByUserId(userId);
                return Ok(insurances);
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotIntegerException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateInsurance([FromBody] InsuranceUpdateDTO insurance)
        {
            try
            {
                _insuranceService.UpdateInsurance(insurance);
                return Ok("Success");
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Detele/{id}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _insuranceService.DeleteInsurance(id);
                return Ok("Deleted");
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
