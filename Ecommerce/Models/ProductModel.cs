using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [DisplayName("Product name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Product Description is required.")]
        public string Description { get; set; }

        [Range(0.01, 100.00, ErrorMessage = "Price must be between 0.01 and 100.00")]
        [Required(ErrorMessage = "Product Price is required.")]
        public decimal Price { get; set; }

        [Range(0, 100, ErrorMessage = "Stock quantity must be between 0 and 100")]
        [Required(ErrorMessage = "Stock quantity is required.")]
        [DisplayName("Stock quantity")]
        public int StockQty { get; set; }
    }
}