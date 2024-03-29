﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA_Ecommerce.Shared
{
   public class Product
   {
      public int Id { get; set; }
      public string Title { get; set; } = string.Empty;
      public string Description { get; set; } = string.Empty;
      public string ImageUrl { get; set; } = string.Empty;
      [Column(TypeName="decimal(18,2)")]
     // public decimal Price { get; set; } = decimal.MinValue;
      public Category? Category { get; set; }
      public int CategoryId { get; set; }
      //not nessary it works out relationship
      public bool Featured { get; set; } = false;

        public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();

    }
}
