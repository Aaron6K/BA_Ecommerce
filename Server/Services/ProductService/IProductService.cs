﻿namespace BA_Ecommerce.Server.Services.ProductService
{
   public interface IProductService
   {
      Task<ServiceResponse<List<Product>>> GetProductsAsync(); 
      Task<ServiceResponse<Product>> GetProductByIdAsync(int productId);
      Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
   }
}
