using WebStore.Domain.Clients;

namespace WebStore.DataServer.Extention
{
    public static class ClientRequestExtention
    {
        public static Client ToOrderDTO(this ClientRequest clientRequest)
        {
            return new Client
            {
                Id = clientRequest.Id,
                FullName = clientRequest.FullName,
                Mail = clientRequest.Mail,
                Phone = clientRequest.Phone,
                Password = clientRequest.Password
            };

        }
    }
}
