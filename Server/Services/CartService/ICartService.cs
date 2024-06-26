﻿namespace BA_Ecommerce.Server.Services.CartService
{
   public interface ICartService
   {
      Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems);
      Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems);

      Task<ServiceResponse<int>> GetCartItemsCount();

      Task<ServiceResponse<List<CartProductResponse>>> GetDBCartProducts();

      Task<ServiceResponse<bool>> AddToCart(CartItem cartItem);
      Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem);

 
      Task<ServiceResponse<bool>> RemoveCartItem(int productId, int productTypeId);
   }
}
