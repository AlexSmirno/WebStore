using Microsoft.Extensions.DependencyInjection;

using System.Net.Http.Json;

using WebStoreServer;
using WebStore.Domain.Products;
using WebStore.Domain.DAL;

namespace Tests
{
    [Collection("Integration Tests")]
    public class ProductTests : TestBase
    {
        public ProductTests(TestServer server) : base(server)
        {

        }

        [Fact]
        public async Task GetProduct()
        {
            var res = await _client.GetFromJsonAsync<List<Product>>($"/api/Product");

            Assert.True(res != null, "null");

            var db = _services.GetRequiredService<StoreContext>().Products.ToList();
            Assert.True(res.Count == db.Count(), res.Count.ToString());
        }
    }
}
