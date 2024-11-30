using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<List<Supply>>> GetProducts()
        {
            var result = await _supplyService.GetProductsAsync();

            if (result != null)
            {
                return await Task.FromResult(result);
            }

            return NotFound();
        }
    }
}
