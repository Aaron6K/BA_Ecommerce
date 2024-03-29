﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BA_Ecommerce.Shared
{
   public class ProductVariant
   {
      [JsonIgnore] // to avoid the circular reference do not include product in the variant
      public Product Product { get; set; }
      public int ProductId { get; set; }

      public ProductType ProductType { get; set; }
      public int ProductTypeId { get; set; }

      [Column(TypeName = "decimal(18,2)")]
      public decimal Price { get; set; }

      [Column(TypeName = "decimal(18,2)")]
      public decimal OriginalPrice { get; set; }

   }
}
