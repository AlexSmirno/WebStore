

using WebStore.Domain.Clients;

namespace WebStore.Domain.DAL.Interfaces
{
    public interface IClientRepository
    {
        public Task<Client> GetClientByMailAsync(string mail);
        public Task<bool> AddClientAsync(Client newClient);
        public Task<bool> DeleteClientAsync(Client client);
    }
}
