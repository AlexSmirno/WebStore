using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Clients;

namespace WebStore.API.Features.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientRPCSender _clientRPCSender;
        public ClientController(ClientRPCSender clientRPCSender)
        {
            _clientRPCSender = clientRPCSender;
        }

        [HttpPost("/Registration", Name = "Registration")]
        public async Task<bool> Registration([FromBody] Client client)
        {
            bool result = await _clientRPCSender.ClientRegistration(client);

            return await Task.FromResult(result);
        }

    }
}