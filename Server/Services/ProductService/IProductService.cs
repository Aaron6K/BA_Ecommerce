namespace BA_Ecommerce.Server.Services.ProductService
{
   public interface IProductService
   {
      Task<ServiceResponse<List<Product>>> GetProductsAsync(); 
   }
}
