using BA_Ecommerce.Server.Services.CartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BA_Ecommerce.Server.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class CartController : ControllerBase
   {
      private readonly ICartService _cartService;
      public CartController(ICartService cartService)
      {
         _cartService = cartService;
      }
      //[FromBody]
      [HttpPost("products")]
      public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartProduct (List<CartItem> cartItems)
      {
         var result= await _cartService.GetCartProducts(cartItems);
         return Ok(result);
      }
   }
}
