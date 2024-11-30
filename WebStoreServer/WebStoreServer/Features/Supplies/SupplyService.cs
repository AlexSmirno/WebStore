using Microsoft.AspNetCore.Mvc;
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Models.Products;
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

        public async Task<ActionResult<List<Supply>>> GetProductsAsync()
        {
            var products = _repository.GetAllSupplies().ToList();

            return await Task.FromResult(products);
        }
    }
}
