using System.ComponentModel.DataAnnotations;

namespace WebStoreServer.Models.Supplies
{
    public class Supply
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? SupplyType { get; set; }
        public Guid SupplierId { get; set; }
        public Guid ClientId { get; set; }

    }
}
