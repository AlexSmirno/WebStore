using Microsoft.EntityFrameworkCore;
using WebStoreServer.Models;
using WebStoreServer.Models.Clients;
using WebStoreServer.Models.Clients;

namespace WebStoreServer.DAL.Repositories
{
    public class ClientRepository
    {
        private StoreContext _context;
        public ClientRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Client>>> GetAllSuppliesAsync()
        {
            var clients = _context.ClientsTable;

            return await Task.FromResult(new Result<IEnumerable<Client>>(clients));
        }

        public async Task<Result<Client>> GetClientByIdAsync(Guid id)
        {
            var client = await _context.ClientsTable.FirstOrDefaultAsync(p => p.Id == id);

            if (client == null)
            {
                return await Task.FromResult(new Result<Client>() { IsSucceeded = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<Client>(client));
        }

        public async Task<Result<IEnumerable<Client>>> GetClientsByNameAsync(string name)
        {
            var client = _context.ClientsTable.Where(p => p.CompamyName == name);

            if (client == null || client.Count() == 0)
            {
                return await Task.FromResult(new Result<IEnumerable<Client>>() { IsSucceeded = false, ErrorMessage = "Таких компаний нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<IEnumerable<Client>>(client));
        }

        public async Task<Result<bool>> AddClientAsync(Client newClient)
        {
            try
            {
                var currentClient = await _context.ClientsTable.AddAsync(newClient);

                _context.SaveChanges();

                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //TODO: Think about error message for user
                return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 400 });
            }
        }


        public async Task<Result<bool>> UpdateClientAsync(Client newClient)
        {
            try
            {
                var currentClient = await _context.ClientsTable.FindAsync(newClient.Id);

                if (currentClient == null)
                {
                    return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
                }

                currentClient.CompamyName = newClient.CompamyName;
                currentClient.Adress = newClient.Adress;
                currentClient.PhoneNumber = newClient.PhoneNumber;
                currentClient.Negoriator = newClient.Negoriator;

                _context.SaveChanges();
                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 503 });
            }
        }

        public async Task<Result<bool>> DeleteClientAsync(Client client)
        {
            int count = await _context.ClientsTable.Where(p => p.Id == client.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            _context.SaveChanges();
            return await Task.FromResult(new Result<bool>(true));
        }
    }
}
