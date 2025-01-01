using Grpc.Net.Client;
using WebStore.Domain;
using WebStore.Domain.Supplies;

namespace WebStoreServer.Features.Senders
{
    public class RPCSender : ISender
    {
        public RPCSender()
        {

        }


        public async Task<Result<string>> Send(string message)
        {
            using var channel = GrpcChannel.ForAddress("http://localhost:5113");

            var client = new Greeter.GreeterClient(channel);

            var reply = await client.SayHelloAsync(new HelloRequest { Name = message });
            Console.WriteLine($"Ответ сервера: {reply.Message}");

            return new Result<string>(reply.Message);
        }

        public async Task<Result<bool>> Send(Order Order)
        {
            try
            {
                using var channel = GrpcChannel.ForAddress("http://localhost:5113");

                var client = new Greeter.GreeterClient(channel);

                var reply = await client.SayHelloAsync(new HelloRequest { Name = message });
            }
            catch (Exception)
            {

                throw;
            }

            return new Result<bool>(true);
        }
    }
}
