﻿namespace BA_Ecommerce.Server.Services.CartService
{
   public interface ICartService
   {
      Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems);
   }
}
