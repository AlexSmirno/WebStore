using System.ComponentModel.DataAnnotations;
using WebStore.Domain.Clients;

namespace WebStore.Domain.Orders
{
    public class Order
    {
        public int Id { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int OrderTypeId { get; set; }
        public OrderType? OrderType { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
        public List<ProductOrderInfo>? ProductOrderInfos { get; set; }
    }
}
