using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Net;

using WebStore.Domain.Orders;
using WebStore.Domain.Products;

using WebStore.API;
using Microsoft.EntityFrameworkCore;
using WebStore.Domain.DAL;

namespace Tests
{
    [Collection("Integration Tests")]
    public class OrderTests : TestBase
    {
        public OrderTests(TestServer server) : base(server)
        {
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

            var db = _services.GetRequiredService<StoreContext>();
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var productsBefore = db.Products.ToList();

            var res = await _client.PostAsJsonAsync<OrderDTO>($"/api/Order", order);
            Assert.True(res.StatusCode == HttpStatusCode.OK, res.StatusCode.ToString());

            var productsAfter = db.Products.ToList();

            for (int i = 0; i < order.Products.Count; i++)
            {
                int id = order.Products[i].Id;
                int removeCount = order.Products[i].Count;

                int after = productsAfter.FirstOrDefault(p => p.Id == id).Count;
                int before = productsBefore.FirstOrDefault(p => p.Id == id).Count;

                Assert.True(after - before == removeCount, "wrong sum");
            }
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
                Count = 10
            });

            order.Products.Add(new Product()
            {
                Id = 2,
                Count = 7
            });

            var db = _services.GetRequiredService<StoreContext>();
            db.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var productsBefore = db.Products.ToList();

            var res = await _client.PostAsJsonAsync<OrderDTO>($"/api/Order", order);
            Assert.True(res.StatusCode == HttpStatusCode.OK, res.StatusCode.ToString());

            var productsAfter = db.Products.ToList();

            for (int i = 0; i < order.Products.Count; i++)
            {
                int id = order.Products[i].Id;
                int removeCount  = order.Products[i].Count;

                int after = productsAfter.FirstOrDefault(p => p.Id == id).Count;
                int before = productsBefore.FirstOrDefault(p => p.Id == id).Count;

                Assert.True(before - after == removeCount, "wrong sum");
            }
        }
    }
}
