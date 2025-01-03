using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

using System.Net;
using System.Net.Http.Json;

using WebStore.Domain.Clients;
using WebStoreServer;
using WebStoreServer.DAL;

namespace Tests
{
    [Collection("Integration Tests")]
    public class ClientTests : TestBase
    {
        public ClientTests(TestServer server) : base(server)
        {

        }

        [Fact]
        public async Task ChangeClientInfo()
        {
            var client = new Client();
            client.Id = 1;
            client.FullName = "Семен 2";

            var res = await _client.PutAsJsonAsync<Client>($"/api/Client", client);

            Assert.True(res.StatusCode == HttpStatusCode.OK,
                res.StatusCode.ToString());
            var result = await res.Content.ReadAsStringAsync();
            Assert.True(result == "true", result);

            var db = _services.GetRequiredService<StoreContext>();

            string name = db.Database.GetDbConnection().Database;

            var dbClient = await db.Clients.FirstOrDefaultAsync(cl => cl.Id == client.Id);
            Assert.True(dbClient.FullName == client.FullName, dbClient.FullName);
        }
    }
}
