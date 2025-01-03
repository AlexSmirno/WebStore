using System.Net.Http.Json;
using WebStore.Domain.Products;

namespace WebStore.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7151");

            var cl = new Domain.Clients.Client();
            cl.Id = 1;
            cl.FullName = "Семен";

            var res = client.PutAsJsonAsync<Domain.Clients.Client>($"/api/Client", cl);

            Console.WriteLine(res.Result);

            Console.ReadKey();
        }
    }
}
