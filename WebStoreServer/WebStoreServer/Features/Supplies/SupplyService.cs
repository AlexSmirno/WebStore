using Microsoft.AspNetCore.Mvc;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Models;
using WebStoreServer.Models.Supplies;
using WebStoreServer.Models.Supplies;

namespace WebStoreServer.Features.Supplies
{
    public class SupplyService
    {
        private SupplyRepository _repository;
        public SupplyService(SupplyRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<IEnumerable<Supply>>> GetSuppliesAsync()
        {
            var supplies = await _repository.GetAllSuppliesAsync();

            return await Task.FromResult(supplies);
        }

        public async Task<Result<Supply>> GetSupplyByIdAsync(Guid id)
        {
            var supplies = await _repository.GetSupplyByIdAsync(id);

            return await Task.FromResult(supplies);
        }

        public async Task<Result<bool>> CreateSupply(Supply newSupply)
        {
            newSupply.Id = Guid.NewGuid();

            var res = await _repository.AddSupplyAsync(newSupply);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> UpdateSupply(Supply newSupply)
        {
            var res = await _repository.UpdateSupplyAsync(newSupply);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> DeleteSupply(Supply newSupply)
        {
            var res = await _repository.DeleteSupplyAsync(newSupply);

            return await Task.FromResult(res);
        }
    }
}
