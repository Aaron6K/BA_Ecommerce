using BA_Ecommerce.Server.Migrations;

namespace BA_Ecommerce.Server.Services.CategoryService
{
   public interface ICategoryService
   {
      Task<ServiceResponse<List<Category>>> GetCategories();
      
   }
}
