
using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Orders;

namespace WebStore.Domain.Clients
{
    public class Client
    {
        public int Id { get; set; }
        public string? Mail { get; set; }
        public string? FullName { get; set; }
        public string? Phone { get; set; }
        public string? Password { get; set; }
        public List<Order>? Orders { get; set; }

    }
}
