using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Clients;
using WebStore.Domain.DAL.Interfaces;

namespace WebStore.Domain.DAL.EF.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private StoreContext _context;
        public ClientRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Client> GetClientByMailAsync(string mail)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(p => p.Mail == mail);

            return await Task.FromResult(client);
        }

        public async Task<bool> AddClientAsync(Client newClient)
        {
            try
            {
                var currentClient = await _context.Clients.AddAsync(newClient);

                _context.SaveChanges();

                return await Task.FromResult(true);
            }
            catch (Exception ex)
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteClientAsync(Client client)
        {
            int count = await _context.Clients.Where(p => p.Id == client.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(false);
            }

            _context.SaveChanges();
            return await Task.FromResult(true);
        }
    }
}
