using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStoreServer.Models.Clients
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? CompamyName { get; set; }
        public string? Adress { get; set; }
        public string? Negoriator { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
