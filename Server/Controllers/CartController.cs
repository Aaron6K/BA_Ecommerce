using BA_Ecommerce.Server.Services.CartService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
      public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> GetCartProduct(List<CartItem> cartItems)
      {
         var result = await _cartService.GetCartProducts(cartItems);
         return Ok(result);
      }

      [HttpPost]
      public async Task<ActionResult<ServiceResponse<List<CartProductResponse>>>> StoreCartItems(List<CartItem> cartItems)
      {
         //var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
         var result = await _cartService.StoreCartItems(cartItems);
         return Ok(result);
      }
      [HttpGet("count")]
      public async Task<ActionResult<ServiceResponse<int>>> GetCartItemsCount()
      {
         return await _cartService.GetCartItemsCount();
      }
      [HttpGet]
      public async Task<ActionResult<ServiceResponse<int>>> GetDBCartProducts()
      {
         var result = await _cartService.GetDBCartProducts();
         return Ok(result);

      }

      [HttpPost("add")]
      public async Task<ActionResult<ServiceResponse<bool>>> AddToCart(CartItem cartItem)
      {

         var result = await _cartService.AddToCart(cartItem);

         return Ok(result);
      }

      [HttpPut("update-quantity")]
      public async Task<ActionResult<ServiceResponse<bool>>> UpdateToCart(CartItem cartItem)
      {

         var result = await _cartService.UpdateQuantity(cartItem);

         return Ok(result);
      }

      [HttpDelete("{productId}/{productTypeId}")]
      public async Task<ActionResult<ServiceResponse<bool>>> RemoveCartItem(int productId,int productTypeId)
      {
         var result = await _cartService.RemoveCartItem(productId,productTypeId);
         return Ok(result);
      }
   }
}
