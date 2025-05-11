using labor_fee_calculator.Models;
using labor_fee_calculator.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace labor_fee_calculator.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaborController : ControllerBase
    {
        public readonly ILaborFeeService laborFeeService;

        public LaborController(ILaborFeeService _laborFeeService)
        {
            laborFeeService = _laborFeeService;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] LaborInput input)
        {
            var result = laborFeeService.GetLaborFeeCalculation(input);
            if (result.Data == null && string.IsNullOrEmpty(result.ErrorMessage))
            {
                return BadRequest(result.ErrorMessage);
            }
            else
            {
                return Ok(result);
            }
        }
    }
}
