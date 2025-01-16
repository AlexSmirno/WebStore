using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Net;
using System.Net.Http.Json;

using WebStore.Domain.Clients;
using WebStore.Domain.DAL;
using WebStore.API;

namespace Tests
{
    [Collection("Integration Tests")]
    public class ClientTests : TestBase
    {
        public ClientTests(TestServer server) : base(server)
        {

        }

        [Fact]
        public async Task GetClients()
        {
            var res = await _client.GetFromJsonAsync<List<Client>>($"/api/Client");

            Assert.True(res != null, "null");

            var db = _services.GetRequiredService<StoreContext>();
            Assert.True(res.Count == db.Clients.Count(), res.Count.ToString());
        }

        [Fact]
        public async Task ChangeClientInfo()
        {
            var client = new Client();
            client.Id = 1;
            client.FullName = "Семен Семеныч";

            var res = await _client.PutAsJsonAsync<Client>($"/api/Client", client);

            Assert.True(res.StatusCode == HttpStatusCode.OK,
                res.StatusCode.ToString());
            var result = await res.Content.ReadAsStringAsync();
            Assert.True(result == "true", result);

            var db = _services.GetRequiredService<StoreContext>();

            var dbClient = await db.Clients.FirstOrDefaultAsync(cl => cl.Id == client.Id);
            Assert.True(dbClient.FullName == client.FullName, dbClient.FullName);
        }

        [Fact]
        public async Task ChangeWrongClientInfo()
        {
            var client = new Client();
            client.Id = 5;
            client.FullName = "Семен Семеныч";

            var res = await _client.PutAsJsonAsync<Client>($"/api/Client", client);

            Assert.True(res.StatusCode == HttpStatusCode.NotFound,
                res.StatusCode.ToString());
        }
    }
}
