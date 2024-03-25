using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BA_Ecommerce.Shared
{
   public  class UserLogin
   {
      [Required(ErrorMessage ="Enter Email")]
      public string Email { get; set; } = "";
      [Required(ErrorMessage ="Enter password")]
      public string Password { get; set; } = "";
   }
}
