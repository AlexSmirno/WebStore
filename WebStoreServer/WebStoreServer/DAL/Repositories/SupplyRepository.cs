
using Microsoft.EntityFrameworkCore;
using WebStoreServer.Models.Supplies;
using WebStoreServer.Models;

namespace WebStoreServer.DAL.Repositories
{
    public class SupplyRepository
    {

        private StoreContext _context;
        public SupplyRepository(StoreContext context)
        {
            _context = context;
        }

        public async Task<Result<IEnumerable<Supply>>> GetAllSuppliesAsync()
        {
            var supplies = _context.SuppliesTable;

            return await Task.FromResult(new Result<IEnumerable<Supply>>(supplies));
        }

        public async Task<Result<Supply>> GetSupplyByIdAsync(Guid id)
        {
            var supply = await _context.SuppliesTable.FirstOrDefaultAsync(p => p.Id == id);

            if (supply == null)
            {
                return await Task.FromResult(new Result<Supply>() { IsSucceeded = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            return await Task.FromResult(new Result<Supply>(supply));
        }

        public async Task<Result<bool>> AddSupplyAsync(Supply newSupply)
        {
            try
            {
                var currentSupply = await _context.SuppliesTable.AddAsync(newSupply);

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


        public async Task<Result<bool>> UpdateSupplyAsync(Supply newSupply)
        {
            try
            {
                var currentSupply = await _context.SuppliesTable.FindAsync(newSupply.Id);

                if (currentSupply == null)
                {
                    return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
                }

                currentSupply.Description = newSupply.Description;
                currentSupply.SupplierId = newSupply.SupplierId;
                currentSupply.ProductId = newSupply.ProductId;
                currentSupply.ClientId = newSupply.ClientId;
                currentSupply.Count = newSupply.Count;
                currentSupply.Date = newSupply.Date;
                currentSupply.Time = newSupply.Time;
                currentSupply.SupplyType = newSupply.SupplyType;

                _context.SaveChanges();

                return await Task.FromResult(new Result<bool>(true));
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = ex.Message, ErrorCode = 503 });
            }
        }

        public async Task<Result<bool>> DeleteSupplyAsync(Supply Supply)
        {
            int count = await _context.SuppliesTable.Where(p => p.Id == Supply.Id).ExecuteDeleteAsync();

            if (count == 0)
            {
                return await Task.FromResult(new Result<bool>() { IsSucceeded = false, Data = false, ErrorMessage = "Такого элемента нет", ErrorCode = 404 });
            }

            _context.SaveChanges();
            return await Task.FromResult(new Result<bool>(true));
        }
    }
}
