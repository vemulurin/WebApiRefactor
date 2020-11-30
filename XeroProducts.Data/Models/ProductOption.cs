using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace XeroProducts.Data.Models
{
    /// <summary>
    /// Model for ProductOption
    /// </summary>
    public partial class ProductOption
    {
        [Key]
        public Guid Id { get; set; }
        //[JsonIgnore]
        public Guid ProductId { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Model for Productoptions
    /// </summary>
    public class ProductOptions
    {
        public IEnumerable<ProductOption> Items { get; set; }
    }
}
