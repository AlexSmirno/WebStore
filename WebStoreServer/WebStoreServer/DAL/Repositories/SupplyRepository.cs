
using WebStoreServer.Models.Supplies;

namespace WebStoreServer.DAL.Repositories
{
    public class SupplyRepository
    {

        private StoreContext _context;
        public SupplyRepository(StoreContext context)
        {
            _context = context;
        }

        public IEnumerable<Supply> GetAllSupplies()
        {
            var supplies = _context.SuppliesTable;

            return supplies;
        }
    }
}
