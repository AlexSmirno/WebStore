using Grpc.Core;
using WebStore.DataServer.Extention;
using WebStore.Domain.DAL.Repositories;

namespace WebStore.DataServer.Services
{
    public class ClientService : ClientServiceGRPS.ClientServiceGRPSBase
    {

        private ClientRepository _clientRepository;
        public ClientService(ClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }


        public override async Task<BoolReply> Registration(ClientRequest request, ServerCallContext context)
        {
            bool result = await _clientRepository.AddClientAsync(request.ToOrderDTO());

            return await Task.FromResult(new BoolReply() { IsSuccess = result });
        }
    }
}
