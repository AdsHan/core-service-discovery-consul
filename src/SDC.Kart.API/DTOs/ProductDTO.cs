using System;

namespace SDC.Kart.API.DTOs
{
    public class ProductDTO
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }
}