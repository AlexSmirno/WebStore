using Microsoft.AspNetCore.Mvc;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Models;
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

        public async Task<Result<IEnumerable<Client>>> GetClientsAsync()
        {
            var clients = await _repository.GetAllSuppliesAsync();

            return await Task.FromResult(clients);
        }

        public async Task<Result<Client>> GetClientByIdAsync(int id)
        {
            var clients = await _repository.GetClientByIdAsync(id);

            return await Task.FromResult(clients);
        }

        public async Task<Result<IEnumerable<Client>>> GetClientByNameAsync(string name)
        {
            var clients = await _repository.GetClientsByNameAsync(name);

            return await Task.FromResult(clients);
        }

        public async Task<Result<bool>> CreateClientAsync(Client newClient)
        {
            var res = await _repository.AddClientAsync(newClient);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> UpdateClientAsync(Client newClient)
        {
            var res = await _repository.UpdateClientAsync(newClient);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> DeleteClientAsync(Client newClient)
        {
            var res = await _repository.DeleteClientAsync(newClient);

            return await Task.FromResult(res);
        }
    }
}
