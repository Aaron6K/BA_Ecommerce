namespace BA_Ecommerce.Client.Services.CartService
{
   public interface ICartService
   {
      event Action OnChange;
      Task AdToCart(CartItem cartItem);
      Task<List<CartItem>> GetCartItems();
      Task<List<CartProductResponse>> GetCartProducts();
      Task RemoveProductFromCart(int productId, int productTypeId);
      Task  UpdateQuantity(CartProductResponse prouct);
   }
}
