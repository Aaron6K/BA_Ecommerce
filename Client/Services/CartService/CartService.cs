
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
      //private readonly AuthenticationStateProvider _authStateProvider
      private readonly IAuthService _authService;

      public CartService(ILocalStorageService localStorage ,HttpClient http, AuthenticationStateProvider authStateProvider ,IAuthService authService) { 
         _localStorage = localStorage;
         _http = http;
         _authService = authService;
        // _authStateProvider=authStateProvider;
      }

      //private async Task<bool> IsAuthenticated()
      //{
      //   return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
      //}

   

      public async Task AdToCart(CartItem cartItem)
      {
         await GetCartItemCount();

         if (await _authService.IsUserAuthenticated())
         {
            
            var response = await _http.PostAsJsonAsync("api/cart/add", cartItem);

            var updated = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
         }
         else
         {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
               cart = new List<CartItem>();
            }

            var sameItem = cart.Find(a => a.ProductId == cartItem.ProductId && a.ProductTypeId == cartItem.ProductTypeId);
            if (sameItem == null)
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
         }

         await GetCartItemCount();

      }

      //public async Task<List<CartItem>> GetCartItems()
      //{
      //   await GetCartItemCount();

      //   var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
      //   if (cart == null)
      //   {
      //      cart = new List<CartItem>();
      //   }
      //   return cart;
      //}

      public async Task<List<CartProductResponse>> GetCartProducts()
      {

         if (await _authService.IsUserAuthenticated())
         {
            var response= await _http.GetFromJsonAsync<ServiceResponse<List<CartProductResponse>>>("api/cart");

            return response.Data;
         }
         else
         {

            var cartItems = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cartItems == null)
            {
               return new List<CartProductResponse>();
            }

            var response = await _http.PostAsJsonAsync("api/cart/products", cartItems);

            var cartProducts = await response.Content.ReadFromJsonAsync<ServiceResponse<List<CartProductResponse>>>();

            return cartProducts.Data;

         }


      }

      public async Task RemoveProductFromCart(int productId, int productTypeId)
      {
         if(await _authService.IsUserAuthenticated())
         {
            _http.DeleteAsync($"api/cart/{productId}/{productTypeId}");
         }
         else
         {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            if (cart == null)
            {
               return;
            }

            var item = cart.Find(a => a.ProductId == productId && a.ProductTypeId == productTypeId);
            if (item == null)
            {
               return;
            }

            cart.Remove(item);

            await _localStorage.SetItemAsync("cart", cart);
         }
       

       //  await GetCartItemCount();
      }

      public async Task UpdateQuantity(CartProductResponse prouct)
      {
         if (await _authService.IsUserAuthenticated())
         {
            var request = new CartItem
            {
               ProductId = prouct.ProductId,
               ProductTypeId = prouct.ProductTypeId,
               Quantity = prouct.Quantity
            };
            await _http.PutAsJsonAsync("api/cart/update-quantity",request);
         }
         else
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

      public async Task StoreCartItems(bool emptyLocalCart=false)
      {
         var localCart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
         if(localCart == null)
         {
            return;
         }
         await _http.PostAsJsonAsync("api/cart", localCart);

         if (emptyLocalCart)
         {
            await _localStorage.RemoveItemAsync("cart");
         }
      }

      public async Task  GetCartItemCount()
      {
         if (await _authService.IsUserAuthenticated())
         {
            var x = await _http.GetFromJsonAsync<ServiceResponse<int>>("api/cart/count");
            var count= x.Data;

            await _localStorage.SetItemAsync<int>("cartItemsCount", count);
         }
         else
         {
            var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
            await _localStorage.SetItemAsync<int>("cartItemsCount", cart!=null? cart.Count():0);
         }

         OnChange.Invoke();

      }
 
   }
}
