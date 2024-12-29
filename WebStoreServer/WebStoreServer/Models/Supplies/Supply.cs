using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using WebStoreServer.Models.Clients;
using WebStoreServer.Models.Products;

namespace WebStoreServer.Models.Supplies
{
    public class Supply
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Number { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? SupplyType { get; set; }
        public int SupplierId { get; set; }
        public Client? Supplier { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
