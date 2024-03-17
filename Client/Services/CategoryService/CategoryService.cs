
namespace BA_Ecommerce.Client.Services.CategoryService
{
   public class CategoryService : ICategoryService
   {
      private readonly HttpClient _httpClient;
 
      public List<Category> Categories { get; set; }= new List<Category>();

      public CategoryService(HttpClient http)
      {
         _httpClient = http;
      }
      public async Task GetCategories()
      {
         var result = await _httpClient.GetFromJsonAsync<ServiceResponse<List<Category>>>("api/Category");
         if(result != null && result.Data!=null)
         {
            Categories = result.Data;
         }
      }
   }
}
