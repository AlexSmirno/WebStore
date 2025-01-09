using Microsoft.Extensions.DependencyInjection;

using System.Net;
using System.Net.Http.Json;

using WebStoreServer;
using WebStoreServer.DAL;
using WebStore.Domain.Products;

namespace Tests
{
    [Collection("Integration Tests")]
    public class ProductTests : TestBase
    {
        public ProductTests(TestServer server) : base(server)
        {

        }

        [Fact]
        public async Task GetClients()
        {
            var res = await _client.GetFromJsonAsync<List<Product>>($"/api/Product");

            Assert.True(res != null, "null");

            var db = _services.GetRequiredService<StoreContext>();
            Assert.True(res.Count == 2, res.Count.ToString());
        }

    }
}
