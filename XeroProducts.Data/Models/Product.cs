using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XeroProducts.Data.Models
{
    /// <summary>
    /// Model for Product 
    /// </summary>
    public partial class Product
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal DeliveryPrice { get; set; }
    }
    /// <summary>
    /// Model for Products 
    /// </summary>

    public class Products { 

        public IEnumerable<Product> Items { get; set; }
    }
}
