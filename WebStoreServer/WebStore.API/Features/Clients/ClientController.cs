using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using WebStore.Domain.Clients;

namespace WebStore.API.Features.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        public ClientController()
        {

        }

        [HttpPost("/Registration", Name = "Registration")]
        public async Task<bool> Registration([FromBody] Client client)
        {


            return false;
        }

        [HttpPost("/Authentication", Name = "Authentication")]
        public async Task<bool> Authentication([FromBody] Client client)
        {


            return false;
        }
    }
}