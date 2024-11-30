using System.ComponentModel.DataAnnotations;

namespace WebStoreServer.Models.Clients
{
    public class Client
    {
        [Key]
        public Guid Id { get; set; }
        public string? CompamyName { get; set; }
        public string? Adress { get; set; }
        public string? Negoriator { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
