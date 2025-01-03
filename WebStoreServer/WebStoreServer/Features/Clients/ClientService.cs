using WebStore.Domain;
using WebStore.Domain.Clients;
using WebStore.Domain.Orders;
using WebStoreServer.DAL.Repositories;

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
            var clients = await _repository.GetAllClientsAsync();

            return await Task.FromResult(clients);
        }

        public async Task<Result<ClientDTO>> GetClientByDTO(ClientDTO client)
        {
            var result = await _repository.GetClientsByDTO(client);

            if (result.IsSucceeded == false)
            {
                return await Task.FromResult(new Result<ClientDTO>() 
                { IsSucceeded = false, ErrorCode = result.ErrorCode, ErrorMessage = result.ErrorMessage});
            }

            var foundClient = result.Data;
            var cl = new ClientDTO(foundClient);

            return await Task.FromResult(new Result<ClientDTO>()
            { IsSucceeded = true, Data = cl});
        }

        public async Task<Result<bool>> CreateClientAsync(ClientAuthDTO newClient)
        {
            var res = await _repository.AddClientAsync(newClient.ToClient());

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> UpdateClientAsync(Client newClient)
        {
            var res = await _repository.UpdateClientAsync(newClient);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> DeleteClientAsync(ClientDTO newClient)
        {
            var res = await _repository.DeleteClientAsync(newClient.ToClient());

            return await Task.FromResult(res);
        }
    }
}
