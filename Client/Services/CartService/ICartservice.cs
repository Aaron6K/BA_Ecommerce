namespace BA_Ecommerce.Client.Services.CartService
{
   public interface ICartservice
   {
      event Action OnChange;
      Task AdToCart(CartItem cartItem);
      Task<List<CartItem>> GetCartItems();
   }
}
