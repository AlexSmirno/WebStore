using WebStore.Domain.Products;

namespace WebStoreServer.Extention
{
    public static class ProductExtension
    {
        public static void FromProductRequest(this Product product, ProductRequest request)
        {
            product.Id = request.Id;
            product.Description = request.Description;
            product.Count = request.Count;
            product.ProductName = request.ProductName;
            product.Size = request.Size;
        }

        public static ProductRequest ToProductRequest(this Product product)
        {
            return new ProductRequest()
            {
                Id = product.Id,
                Description = product.Description,
                Count = product.Count,
                ProductName = product.ProductName,
                Size = product.Size,
            };
        }
    }
}
