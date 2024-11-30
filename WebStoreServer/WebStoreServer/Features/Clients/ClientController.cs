using Microsoft.AspNetCore.Mvc;
using WebStoreServer.Models.Clients;

namespace WebStoreServer.Features.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientService _supplyService;

        public ClientController(ClientService supplyService)
        {
            _supplyService = supplyService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetProducts()
        {
            var result = await _supplyService.GetClientsAsync();

            if (result != null)
            {
                return await Task.FromResult(result);
            }

            return NotFound();
        }
    }
}
