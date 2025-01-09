using WebStore.Domain;
using WebStore.Domain.Products;

namespace WebStoreServer.Features.Senders
{
    public interface ISender
    {
        public Task<Result<List<Product>>> GetProductAsync();
    }
}
