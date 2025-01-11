using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Clients;

namespace WebStoreServer.Features.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientService _clientService;

        public ClientController()
        {

        }

        [HttpGet]
        public async Task<ActionResult<List<Client>>> GetClients()
        {
            var result = await _clientService.GetClientsAsync();

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data.ToList());
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }


        [HttpPost("/api/Clients", Name = "Clients")]
        public async Task<ActionResult<ClientDTO>> GetClientsByDTO([FromBody] ClientDTO client)
        {
            var result = await _clientService.GetClientByDTO(client);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> CreateClient([FromBody] ClientAuthDTO client)
        {
            var result = await _clientService.CreateClientAsync(client);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpPut]
        public async Task<ActionResult<bool>> UpdateClient([FromBody] Client client)
        {
            var result = await _clientService.UpdateClientAsync(client);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteClient([FromBody] ClientDTO client)
        {
            var result = await _clientService.DeleteClientAsync(client);

            if (result.IsSucceeded)
            {
                return await Task.FromResult(result.Data);
            }

            return StatusCode(result.ErrorCode, result.ErrorMessage);
        }
    }
}