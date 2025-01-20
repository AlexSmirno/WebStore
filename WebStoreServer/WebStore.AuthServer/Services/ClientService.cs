using Grpc.Core;
using WebStore.Domain;
using WebStore.Domain.Clients;
using WebStore.Domain.DAL.Repositories;

namespace WebStore.AuthServer.Services
{
    public class ClientService : ClientServiceGRPS.ClientServiceGRPSBase
    {
        private ClientRepository _clientRepository;
        private JWTProvider _jwtProvider;

        public ClientService(ClientRepository clientRepository, JWTProvider jwtProvider)
        {
            _clientRepository = clientRepository;
            _jwtProvider = jwtProvider;
        }

        public async override Task<BoolReply> Registration(ClientRequest request, ServerCallContext context)
        {
            var client = new Client()
            {
                Id = request.Id,
                FullName = request.FullName,
                Mail = request.Mail,
                Phone = request.Phone,
                Password = request.Password
            };

            bool result = await _clientRepository.AddClientAsync(client);

            var reply = new BoolReply() { IsSuccess = result };
            return await Task.FromResult(reply);
        }

        public async override Task<JWTMessage> Authentication(ClientDTORequest request, ServerCallContext context)
        {
            Client client = await _clientRepository.GetClientByMailAsync(request.Mail);

            var reply = new JWTMessage();
            if (client != null && client.Password == request.Password)
            {
                reply.JWT = _jwtProvider.GenerateJWT();
                return await Task.FromResult(reply);
            }

            reply.JWT = "";
            return await Task.FromResult(reply);
        }

        public async override Task<BoolReply> Authorization(JWTMessage request, ServerCallContext context)
        {
            var result = _jwtProvider.VerifyJWT(request.JWT);

            var reply = new BoolReply() { IsSuccess = result };
            return await Task.FromResult(reply);
        }
    }
}
