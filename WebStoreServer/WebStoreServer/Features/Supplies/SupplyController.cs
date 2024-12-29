using Microsoft.AspNetCore.Mvc;
using WebStoreServer.Features.Supplies;
using WebStoreServer.Models.Supplies;
using WebStoreServer.Models.Supplies;

namespace WebStoreServer.Features.Supplies
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyController : ControllerBase
    {
        private SupplyService _supplyService;

        public SupplyController(SupplyService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Supply>>> GetSupplies()
        {
            var result = await _supplyService.GetSuppliesAsync();

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            if (result.ErrorCode == 404) return NotFound();

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }


        [HttpGet("{SupplyName}", Name = "/get_Supply")]
        public async Task<ActionResult<Supply>> GetSuppliesByName(int id)
        {
            var result = await _supplyService.GetSupplyByIdAsync(id);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            if (result.ErrorCode == 404) return NotFound();

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateSupply([FromBody] SupplyDTO Supply)
        {
            var result = await _supplyService.CreateSupply(Supply);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateSupply([FromBody] Supply Supply)
        {
            var result = await _supplyService.UpdateSupply(Supply);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            if (result.ErrorCode / 100 == 4) return BadRequest();

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteSupply([FromBody] Supply Supply)
        {
            var result = await _supplyService.DeleteSupply(Supply);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            if (result.ErrorCode / 100 == 4) return BadRequest();

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }
    }
}
