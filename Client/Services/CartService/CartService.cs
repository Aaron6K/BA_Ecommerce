﻿
using Blazored.LocalStorage;

namespace BA_Ecommerce.Client.Services.CartService
{
   public class CartService : ICartservice
   {
      public event Action OnChange;

      public readonly ILocalStorageService _localStorage;
      public CartService(ILocalStorageService localStorage) { 
         _localStorage = localStorage;
      }

      public async Task AdToCart(CartItem cartItem)
      {
         var cart = await _localStorage.GetItemAsync<List<CartItem>>("cart");
         if(cart== null)
         {
            cart = new List<CartItem>();
         }
         cart.Add(cartItem);

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
   }
}