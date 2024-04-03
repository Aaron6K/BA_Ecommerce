using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace BA_Ecommerce.Shared
{
   public class OrderItem
   {
        public Order Order { get; set; }
        public int OrderId { get; set; }

        public Product Product { get; set; }
        public int ProductId { get; set; }
        public ProductType ProductType { get; set; }

        public int ProductTypeId { get; set; }

        public int Qunatity { get; set; }

        [Column (TypeName ="decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
