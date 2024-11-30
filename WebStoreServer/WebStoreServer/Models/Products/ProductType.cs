using System.ComponentModel.DataAnnotations;

namespace WebStoreServer.Models.Products
{
    public class ProductType
    {
        [Key]
        public Guid Id { get; set; }
        public string? Description { get; set; }
    }
}
