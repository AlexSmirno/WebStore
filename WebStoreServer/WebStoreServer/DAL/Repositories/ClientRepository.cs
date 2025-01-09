using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using WebStore.Domain;
using WebStore.Domain.Clients;
using WebStoreServer.DAL;

namespace WebStoreServer.DAL.Repositories
{
    public class ClientRepository
    {
        private StoreContext _context;
        public ClientRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Client>>> GetAllClientsAsync()
        {
            var clients = _context.Clients;

            return await Task.FromResult(new Result<IEnumerable<Client>>(clients));
        }

        public async Task<Result<Client>> GetClientsByDTO(ClientDTO client)
        {
            var clients = _context.Clients.Include(cl => cl.Orders);
            Client? foundClient = null;

            if (client.Id > 0)
                foundClient = await clients.FirstOrDefaultAsync(p => p.Id == client.Id);

            if (foundClient == null && client.Mail != null)
                foundClient = await clients.FirstOrDefaultAsync(p => p.Mail == client.Mail);

            if (foundClient == null && client.FullName != null)
                foundClient = await clients.FirstOrDefaultAsync(p => p.FullName == client.FullName);

            if (foundClient == null)
            {
                return await Task.FromResult(new Result<Client>()
                { IsSucceeded = false, ErrorMessage = "There is no this client", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<Client>(foundClient));
        }

        public async Task<Result<bool>> AddClientAsync(Client newClient)
        {
            try
            {
                var currentClient = await _context.Clients.AddAsync(newClient);

                _context.SaveChanges();

                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //TODO: Think about error message for user
                return await Task.FromResult(new Result<bool>()
                { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 400 });
            }
        }


        public async Task<Result<bool>> UpdateClientAsync(Client newClient)
        {
            try
            {
                string name = _context.Database.GetDbConnection().Database;
                var currentClient = await _context.Clients.FindAsync(newClient.Id);

                if (currentClient == null)
                {
                    return await Task.FromResult(new Result<bool>()
                    { IsSucceeded = false, Data = false, ErrorMessage = "There is no this client", ErrorCode = 404 });
                }
                if (newClient.Phone != null)
                    currentClient.Phone = newClient.Phone;

                if (newClient.FullName != null)
                    currentClient.FullName = newClient.FullName;

                if (newClient.Password != null)
                    currentClient.Password = newClient.Password;

                if (newClient.Mail != null)
                    currentClient.Mail = newClient.Mail;

                _context.Clients.Update(currentClient);

                _context.SaveChanges();
                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<bool>()
                { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 503 });
            }
        }

        public async Task<Result<bool>> DeleteClientAsync(Client client)
        {
            int count = await _context.Clients.Where(p => p.Id == client.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(new Result<bool>()
                { IsSucceeded = false, Data = false, ErrorMessage = "There is no this client", ErrorCode = 404 });
            }

            _context.SaveChanges();
            return await Task.FromResult(new Result<bool>(true));
        }
    }
}
