using Microsoft.AspNetCore.Mvc;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Models.Clients;

namespace WebStoreServer.Features.Clients
{
    public class ClientService
    {
        private ClientRepository _repository;
        public ClientService(ClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<ActionResult<List<Client>>> GetClientsAsync()
        {
            var products = _repository.GetAllSupplies().ToList();

            return await Task.FromResult(products);
        }
    }
}
