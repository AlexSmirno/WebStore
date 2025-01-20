using Grpc.Net.Client;

using WebStore.Domain.Clients;
using WebStore.Domain;
using WebStore.API.Extention;

namespace WebStore.API.Features.Clients
{
    public class ClientRPCSender
    {

        public async Task<bool> ClientRegistration(Client client)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5005");

            var rpcClient = new ClientServiceGRPS.ClientServiceGRPSClient(channel);
            var reply = await rpcClient.RegistrationAsync(client.ToClientRequest());

            return await Task.FromResult(reply.IsSuccess);
        }
    }
}
