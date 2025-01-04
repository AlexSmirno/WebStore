using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net;

using WebStore.Domain.Orders;
using WebStore.Domain.Products;

using WebStoreServer;
using WebStoreServer.DAL;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Tests
{
    [Collection("Integration Tests")]
    public class OrderTests : TestBase
    {
        public OrderTests(TestServer server) : base(server)
        {
        }


        [Fact]
        public async Task AddExportOrder()
        {
            var order = new OrderDTO()
            {
                ClientId = 1,
                Date = DateTime.Now.ToString("DD.MM.YYYY"),
                Time = DateTime.Now.ToString("hh:mm"),
                OrderType = "Export",
                Products = new List<Product>()
            };

            order.Products.Add(new Product()
            {
                Id = 1,
                Count = 20
            });

            order.Products.Add(new Product()
            {
                Id = 2,
                Count = 23
            });

            var db = _services.GetRequiredService<StoreContext>();

            var productCounts = db.Products.ToList();

            var res = await _client.PostAsJsonAsync<OrderDTO>($"/api/Order", order);

            Assert.True(res.StatusCode == HttpStatusCode.OK, res.StatusCode.ToString());


            var products = db.Products;

            for (int i = 0; i < productCounts.Count; i++)
            {
                int id = productCounts[i].Id;
                int productCount = productCounts[0].Count;

                int real = products.FirstOrDefault(p => p.Id == id).Id;
                int removeCount = order.Products.FirstOrDefault(p => p.Id == id).Id;

                Assert.True(productCount == real + removeCount, "wrong sum");
            }
        }

        [Fact]
        public async Task AddImportOrder()
        {
            var order = new OrderDTO()
            {
                ClientId = 1,
                Date = DateTime.Now.ToString("dd.mm.yyyy"),
                Time = DateTime.Now.ToString("hh:mm"),
                OrderType = "Import",
                Products = new List<Product>()
            };

            order.Products.Add(new Product()
            {
                Id = 1,
                Count = 100
            });

            order.Products.Add(new Product()
            {
                Id = 2,
                Count = 43
            });

            var res = await _client.PostAsJsonAsync<OrderDTO>($"/api/Order", order);

            Assert.True(res.StatusCode == HttpStatusCode.OK, res.StatusCode.ToString());

            var db = _services.GetRequiredService<StoreContext>();

            var products = db.Products;

            foreach (var product in order.Products)
            {
                int firstCount = products.FirstOrDefault(p => p.Id == product.Id).Count;
                Assert.True(firstCount == product.Count, firstCount.ToString());
            }
        }

    }
}
