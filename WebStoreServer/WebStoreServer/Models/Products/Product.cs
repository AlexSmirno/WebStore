using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebStoreServer.Models.Products
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public int DevisionId { get; set; }
        public int Size { get; set; }
        public Guid ProductTypeId { get; set; }

    }
}
