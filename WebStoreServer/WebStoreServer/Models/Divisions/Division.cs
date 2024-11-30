using System.ComponentModel.DataAnnotations;

namespace WebStoreServer.Models.Divisions
{
    public class Division
    {
        [Key]
        public Guid Id { get; set; }
        public string? DivisionName { get; set; }
        public int Volume { get; set; }

    }
}
