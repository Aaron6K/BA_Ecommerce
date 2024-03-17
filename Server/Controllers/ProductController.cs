using BA_Ecommerce.Server.Services.ProductService;
using Microsoft.AspNetCore.Mvc;
namespace BlazorEcommerce.Server.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class ProductController : ControllerBase
   {
      private readonly IProductService _productService;

      public ProductController(IProductService productService) {
 
         this._productService = productService;
      }

      [HttpGet]
      public async Task<ActionResult<ServiceResponse<List<Product>>>>GetProducts()
      {
         var products = await _productService.GetProductsAsync();
         return   Ok(products);
      }

      [HttpGet("{productId}")]
      public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProduct(int productId)
      {
         var product = await _productService.GetProductByIdAsync(productId);
         return Ok(product);
      }

      [HttpGet("category/{categoryUrl}")]
      public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductByCategory(string categoryUrl)
      {
         var result = await _productService.GetProductsByCategory(categoryUrl);
         return Ok(result);
      }


      [HttpGet("search/{searchText}/{page}")]
      public async Task<ActionResult<ServiceResponse<ProductSearchResult>>> SearchProducts(string searchText,int page=1)
      {
         var result = await _productService.SearchProducts(searchText,page);
         return Ok(result);
      }

      [HttpGet("searchsuggestions/{searchText}")]
      public async Task<ActionResult<ServiceResponse<List<Product>>>> GetProductSerachSuggestion(string searchText)
      {
         var result = await _productService.GetProductSerachSuggestion(searchText);
         return Ok(result);
      }
      [HttpGet("featured")]
      public async Task<ActionResult<ServiceResponse<List<Product>>>> GetFeaturedProduct()
      {
         var result = await _productService.GetFeaturedProducts();
         return Ok(result);
      }
 
   }
}
