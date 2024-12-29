using System.Net.Http.Json;
using WebStoreServer.Models.Products;

namespace WebStore.Client
{
    internal class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:7151");

            string name = "string";
            var product = client.GetFromJsonAsync<Product>($"/api/Product/{name}").Result;


            Console.WriteLine(product.Id + " " + product.Count);

            Console.ReadKey();
        }
    }
}
