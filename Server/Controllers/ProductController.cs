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
   }
}
