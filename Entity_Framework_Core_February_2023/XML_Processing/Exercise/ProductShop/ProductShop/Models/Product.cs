﻿namespace ProductShop.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Product
    {
        public Product()
        {
            this.CategoryProducts = new List<CategoryProduct>();
        }

        public int Id { get; set; }

        public string Name { get; set; } = null!;

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        [ForeignKey(nameof(Seller))]
        public int SellerId { get; set; }
        public virtual User Seller { get; set; } = null!;

        [ForeignKey(nameof(Buyer))]
        public int? BuyerId { get; set; }
        public virtual User? Buyer { get; set; } = null!;

        public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
    }
}