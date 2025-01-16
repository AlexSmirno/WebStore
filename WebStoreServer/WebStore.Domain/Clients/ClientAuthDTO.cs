

using WebStore.Domain.Orders;

namespace WebStore.Domain.Clients
{
    public class ClientAuthDTO
    {
        public string? Mail { get; set; }
        public string? Password { get; set; }

        public ClientAuthDTO() { }
        public ClientAuthDTO(Client client)
        {
            Id = client.Id;
            Mail = client.Mail;
            Password = client.Password;
        }

        public Client ToClient()
        {
            var cl = new Client();

            cl.Id = Id;
            cl.Mail = Mail;
            cl.Password = Password;

            return cl;
        }
    }
}
