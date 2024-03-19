
namespace BA_Ecommerce.Server.Services.CartService
{
   public class CartService : ICartService
   {
      public readonly DataContext _context;
      public CartService(DataContext context) {
         _context = context;
      }

      public Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
      {
         var result= new ServiceResponse<List<CartProductResponse>>()
         {
            Data= new List<CartProductResponse>()
         };


         foreach (var cartItem in cartItems) { }

         return Task.FromResult(result);

      }
   }
}
