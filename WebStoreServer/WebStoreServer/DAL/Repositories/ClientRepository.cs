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

        public IEnumerable<Client> GetAllSupplies()
        {
            var clients = _context.ClientsTable;

            return clients;
        }
    }
}
