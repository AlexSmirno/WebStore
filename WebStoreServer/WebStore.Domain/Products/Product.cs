﻿
namespace WebStore.Domain.Products
{
    public class Product
    {
        public int Id { get; set; }
        public string? ProductName { get; set; }
        public decimal Price { get; set; }
        public int Count { get; set; }
        public string? Description { get; set; }
        public int Size { get; set; }
    }
}