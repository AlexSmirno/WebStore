

using WebStore.Domain.Orders;

namespace WebStore.Domain.Clients
{
    public class ClientAuthDTO
    {
        public int Id { get; set; }
        public string? Mail { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }

        public ClientAuthDTO() { }
        public ClientAuthDTO(Client client)
        {
            Id = client.Id;
            Mail = client.Mail;
            Phone = client.Phone;
            Password = client.Password;
        }

        public Client ToClient()
        {
            var cl = new Client();

            cl.Id = Id;
            cl.Mail = Mail;
            cl.Phone = Phone;
            cl.Password = Password;

            return cl;
        }
    }
}
