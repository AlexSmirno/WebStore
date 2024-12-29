
using WebStoreServer.DAL.Repositories;
using WebStoreServer.Features.Products;
using WebStoreServer.Models;
using WebStoreServer.Models.Supplies;

namespace WebStoreServer.Features.Supplies
{
    public class SupplyService
    {
        private SupplyRepository _supplyRepository;
        private ProductRepository _productRepository;
        private ClientRepository _clientRepository;
        public SupplyService(
            SupplyRepository supplyRepository, 
            ProductRepository productRepository,
            ClientRepository clientRepository)
        {
            _supplyRepository = supplyRepository;
            _productRepository = productRepository;
            _clientRepository = clientRepository;
        }

        public async Task<Result<IEnumerable<Supply>>> GetSuppliesAsync()
        {
            var supplies = await _supplyRepository.GetAllSuppliesAsync();

            return await Task.FromResult(supplies);
        }

        public async Task<Result<Supply>> GetSupplyByIdAsync(int id)
        {
            var supplies = await _supplyRepository.GetSupplyByIdAsync(id);

            return await Task.FromResult(supplies);
        }

        public async Task<Result<bool>> CreateSupply(SupplyDTO newSupply)
        {
            if (newSupply.SupplyType == "in")
            {
                return await CreateInSupply(newSupply);
            }

            if (newSupply.SupplyType == "out")
            {
                return await CreateOutSupply(newSupply);
            }

            return new Result<bool> { Data = false, ErrorCode = 404, ErrorMessage = "Несущесвтующий тип поставки", IsSucceeded = false };
        }

        //Добавление продуктов
        private async Task<Result<bool>> CreateInSupply(SupplyDTO newSupply)
        {
            var productResult = await _productRepository.GetProductByIdAsync(newSupply.Product);
            if (productResult.IsSucceeded == false)
            {
                return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = productResult.ErrorCode,
                        ErrorMessage = productResult.ErrorMessage,
                        Data = false
                    }
                );
            }

            var supply = newSupply.ToSupply();

            var product = productResult.Data;
            if (product == null)
            {
                return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = 404,
                        ErrorMessage = "Неизвестный продукт",
                        Data = false
                    }
                );
            }

            supply.Product = product;
            supply.Product.Count += supply.Count;

            var res = await _supplyRepository.AddInSupplyAsync(supply);

            return await Task.FromResult(res);
        }

        //Удаление продуктов
        private async Task<Result<bool>> CreateOutSupply(SupplyDTO newSupply)
        {
            var productResult = await _productRepository.GetProductByIdAsync(newSupply.Product);
            if (productResult.IsSucceeded == false)
            {
                return await Task.FromResult(new Result<bool>()
                {
                    IsSucceeded = false,
                    ErrorCode = productResult.ErrorCode,
                    ErrorMessage = productResult.ErrorMessage,
                    Data = false
                }
                );
            }

            var supply = newSupply.ToSupply();

            var product = productResult.Data;
            if (product == null)
            {
                return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = 400,
                        ErrorMessage = "Неизвестный продукт",
                        Data = false
                    }
                );
            }

            if (product.Count < supply.Count)
            {
                return await Task.FromResult(new Result<bool>()
                    {
                        IsSucceeded = false,
                        ErrorCode = 400,
                        ErrorMessage = "Недостаток продкта",
                        Data = false
                    }
                );
            }

            supply.Product = product;
            supply.Product.Count -= supply.Count;

            var res = await _supplyRepository.AddInSupplyAsync(supply);

            return await Task.FromResult(res);
        }


        public async Task<Result<bool>> UpdateSupply(Supply newSupply)
        {
            var res = await _supplyRepository.UpdateSupplyAsync(newSupply);

            return await Task.FromResult(res);
        }

        public async Task<Result<bool>> DeleteSupply(Supply newSupply)
        {
            var res = await _supplyRepository.DeleteSupplyAsync(newSupply);

            return await Task.FromResult(res);
        }
    }
}
