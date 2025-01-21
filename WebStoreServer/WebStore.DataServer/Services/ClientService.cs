using Grpc.Core;
using WebStore.DataServer.Extention;
using WebStore.Domain.DAL.EF.Repositories;

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

            var reply = new BoolReply() { IsSuccess = result };
            return await Task.FromResult(reply);
        }
    }
}
