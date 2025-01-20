using WebStore.Domain.Clients;

namespace WebStore.API.Extention
{
    public static class ClientRequestExtention
    {

        public static ClientRequest ToClientRequest(this Client client)
        {
            return new ClientRequest()
            {
                Id = client.Id,
                FullName = client.FullName,
                Mail = client.Mail,
                Password = client.Password,
                Phone = client.Phone
            };
        }
    }
}
