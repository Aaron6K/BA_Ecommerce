
using BA_Ecommerce.Client.Shared;
using BA_Ecommerce.Shared;
using Blazored.LocalStorage;

namespace BA_Ecommerce.Client.Services.CartService
{
   public class CartService : ICartService
   {
      public event Action OnChange;

      public readonly ILocalStorageService _localStorage;
      public readonly HttpClient _http;

      public CartService(ILocalStorageService localStorage ,HttpClient http) { 
         _localStorage = localStorage;
         _http = http;
      }

      public async Task AdToCart(CartItem cartItem)
      {
         var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
         if(cart== null)
         {
            cart = new List<CartItem>();
         }

         var sameItem=cart.Find(a=>a.ProductId==cartItem.ProductId && a.ProductTypeId==cartItem.ProductTypeId);
         if(sameItem==null)
         {
            cartItem.Quantity = 1;
            cart.Add(cartItem);
         }
         else
         {
            sameItem.Quantity += cartItem.Quantity;
            cart.Add(sameItem);
         }

         await _localStorage.SetItemAsync("cart", cart);
         OnChange.Invoke();

      }

      public async Task<List<CartItem>> GetCartItems()
      {
         var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
         if (cart == null)
         {
            cart = new List<CartItem>();
         }
         return cart;
      }

      public async Task<List<CartProductResponse>> GetCartProducts()
      {
         var cartItems = await  _localStorage.GetItemAsync<List<CartItem>>("cart");

         var response = await _http.PostAsJsonAsync("api/cart/products", cartItems);

         var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();
         
          return  cartProducts.Data;

      }

      public async Task RemoveProductFromCart(int productId, int productTypeId)
      {
         var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
         if (cart==null)
         {
            return;
         }

         var item=cart.Find(a=>a.ProductId==productId && a.ProductTypeId==productTypeId);
         if(item == null)
         {
            return;
         }

         cart.Remove(item);

         await _localStorage.SetItemAsync("cart",cart);

         OnChange.Invoke();
      }

      public async Task UpdateQuantity(CartProductResponse prouct)
      {
         var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
         if (cart == null)
         {
            return;
         }

         var item = cart.Find(a => a.ProductId == prouct.ProductId && a.ProductTypeId == prouct.ProductTypeId);
         if (item == null)
         {
            return;
         }

         item.Quantity = prouct.Quantity;

         await _localStorage.SetItemAsync("cart", cart);

      }
   }
}
