

using WebStore.Contracts.Orders;

namespace WebStore.Contracts.Clients
{
    public class ClientAuthDTO
    {
        public string? Mail { get; set; }
        public string? Password { get; set; }

        public ClientAuthDTO() { }
        public ClientAuthDTO(Client client)
        {
            Mail = client.Mail;
            Password = client.Password;
        }

        public Client ToClient()
        {
            var cl = new Client();

            cl.Mail = Mail;
            cl.Password = Password;

            return cl;
        }
    }
}
