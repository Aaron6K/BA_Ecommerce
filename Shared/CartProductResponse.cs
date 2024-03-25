using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BA_Ecommerce.Shared
{
   public class CartProductResponse
   {
      public int ProductId { get; set; }
      public string Title { get; set; } = "";


      public int ProductTypeId { get; set; }
      public string ProductType { get; set; } = string.Empty;
      public string Name { get; set; } = "";
      public string ImageUrl { get; set; } = "";
      public decimal Price { get; set; }

      public int Quantity { get; set; } = 1;

   }
}
