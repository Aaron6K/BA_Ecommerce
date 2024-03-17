using BA_Ecommerce.Server.Migrations;
namespace BA_Ecommerce.Server.Services.CategoryService
{
   public class CategoryService: ICategoryService
   {
      private readonly DataContext _context;
         public CategoryService(DataContext context)
         {
            _context = context;
         }

         public async Task<ServiceResponse<List<Category>>> GetCategories()
         {
            var categories = await _context.Categories.ToListAsync();

            var response = new ServiceResponse<List<Category>>() { 
               Data= categories,
               Success=true,
               Message=""
            };

            return response;
         }
  
 
   }
}
