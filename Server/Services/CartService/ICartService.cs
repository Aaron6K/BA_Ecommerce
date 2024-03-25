namespace BA_Ecommerce.Server.Services.CartService
{
   public interface ICartService
   {
      public Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems);
   }
}
