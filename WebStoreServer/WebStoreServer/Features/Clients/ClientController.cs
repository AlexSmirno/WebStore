using Microsoft.AspNetCore.Mvc;
using WebStoreServer.Features.Clients;
using WebStoreServer.Models.Clients;
using WebStoreServer.Models.Clients;

namespace WebStoreServer.Features.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientService _clientService;

        public ClientController(ClientService clientService)
        {
            _clientService = clientService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            var result = await _clientService.GetClientsAsync();

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            if (result.ErrorCode == 404) return NotFound(result.ErrorMessage);

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }


        [HttpGet("{ClientName}", Name = "/get_client")]
        public async Task<ActionResult<List<Client>>> GetClientsByCompanyName(string name)
        {
            var result = await _clientService.GetClientByNameAsync(name);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            if (result.ErrorCode == 404) return NotFound(result.ErrorMessage);

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateClient([FromBody] Client Client)
        {
            var result = await _clientService.CreateClientAsync(Client);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            if (result.ErrorCode / 100 == 4) return BadRequest(result.ErrorMessage);

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateClient([FromBody] Client Client)
        {
            var result = await _clientService.UpdateClientAsync(Client);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            if (result.ErrorCode / 100 == 4) return BadRequest(result.ErrorMessage);

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteClient([FromBody] Client Client)
        {
            var result = await _clientService.DeleteClientAsync(Client);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            if (result.ErrorCode / 100 == 4) return BadRequest(result.ErrorMessage);

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }
    }
}