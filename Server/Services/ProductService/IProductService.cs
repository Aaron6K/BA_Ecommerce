namespace BA_Ecommerce.Server.Services.ProductService
{
   public interface IProductService
   {
      Task<ServiceResponse<List<Product>>> GetProductsAsync(); 
      Task<ServiceResponse<Product>> GetProductByIdAsync(int productId);
      Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
      Task<ServiceResponse<ProductSearchResult>> SearchProducts(string serachText,int page);
      Task<ServiceResponse<List<string>>> GetProductSerachSuggestion(string serachtext);
      Task<ServiceResponse<List<Product>>> GetFeaturedProducts();
   }
}
