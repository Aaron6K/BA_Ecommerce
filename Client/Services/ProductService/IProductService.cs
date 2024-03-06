namespace BA_Ecommerce.Client.Services.ProductService
{
   public interface IProductService
   {

      List<Product> Products { get; set; }
      Task GetProducts();
   }
}
