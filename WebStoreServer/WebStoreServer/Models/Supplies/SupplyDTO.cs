using WebStoreServer.Models.Clients;
using WebStoreServer.Models.Products;

namespace WebStoreServer.Models.Supplies
{
    public class SupplyDTO
    {
        public int Id { get; set; }
        public string? Number { get; set; }
        public int Product { get; set; }
        public string? Date { get; set; }
        public string? Time { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public string? SupplyType { get; set; }
        public int Supplier { get; set; }
        public int Client { get; set; }


        public Supply ToSupply()
        {
            return new Supply
            {
                Number = Number,
                ProductId = Product,
                Date = Date,
                Time = Time,
                Count = Count,
                Description = Description,
                SupplyType = SupplyType,
                SupplierId = Supplier,
                ClientId = Client,
            };
        }
    }
}
