
using Azure;
using BA_Ecommerce.Server.Migrations;
using BA_Ecommerce.Shared;
using System.Security.Claims;

namespace BA_Ecommerce.Server.Services.CartService
{
   public class CartService : ICartService
   {
      public readonly DataContext _context;
      // private readonly IHttpContextAccessor _httpContextAccessor;

      private readonly BA_Ecommerce.Server.Services.AuthService.IAuthService _authService;

      public CartService(DataContext context,IHttpContextAccessor httpContextAccessor, 
         BA_Ecommerce.Server.Services.AuthService.IAuthService authService) {
         _context = context;
         // _httpContextAccessor= httpContextAccessor;
         _authService = authService;


      }

      public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
      {
         var result= new ServiceResponse<List<CartProductResponse>>()
         {
            Data= new List<CartProductResponse>()
         };

         foreach (var cartItem in cartItems) { 
            var product=await _context.Products.Where(a=>a.Id==cartItem.ProductId).FirstOrDefaultAsync();
            if(product==null)
            {
               continue;
            }

            var productVariant=await _context.ProductVariants.Where(
               v=>
               v.ProductId==cartItem.ProductId && 
               v.ProductTypeId==cartItem.ProductTypeId).
               Include(v=>v.ProductType).FirstOrDefaultAsync();


            if(productVariant==null)
            {
               continue;
            }

            var cartProduct = new CartProductResponse()
            {
               ProductId = product.Id,
               Title=product.Title,
               ProductTypeId = productVariant.ProductTypeId, 
               ProductType= productVariant.ProductType.Name,
               ImageUrl =product.ImageUrl,
               Price=productVariant.Price,
               Quantity= cartItem.Quantity

            };
            result.Data.Add(cartProduct);
         }

         return result;
      }
    
      public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
      {

         var userId =_authService.GetUserId() ;

         cartItems.ForEach(cartItem => cartItem.UserId = userId);
         _context.CartItems.AddRange(cartItems);
         await _context.SaveChangesAsync();

 
         return await GetCartProducts(await _context.CartItems.Where(a => a.UserId == userId).ToListAsync());


      }

      public  async Task<ServiceResponse<int>> GetCartItemsCount()
      {

         var userId = _authService.GetUserId();
         var count=await _context.CartItems.Where(a=>a.UserId== userId).CountAsync();

         ServiceResponse<int> response = new ServiceResponse<int>{ Data = count };
         return response;

      }

      public async Task<ServiceResponse<List<CartProductResponse>>> GetDBCartProducts()
      {

         var userId = _authService.GetUserId();
         List<CartItem> cartItems = await _context.CartItems.Where(a => a.UserId == userId).ToListAsync();
         
         return await GetCartProducts(cartItems);

      }

      public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
      {
         var userId =_authService.GetUserId() ;
         cartItem.UserId = userId;
         var sameItem= await _context.CartItems.FirstOrDefaultAsync(a=>a.ProductId==cartItem.ProductId && a.ProductTypeId==cartItem.ProductTypeId && a.UserId== userId);
         if (sameItem == null)
         {
            if(cartItem.Quantity==0) cartItem.Quantity=1;
            _context.AddAsync(cartItem);
         }
         else
         {
            sameItem.Quantity += cartItem.Quantity;
         }
          await _context.SaveChangesAsync();


         return new ServiceResponse<bool> { Data = true };
      }

      public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
      {

         var userId = _authService.GetUserId();
         var dbCartItem= await _context.CartItems.FirstOrDefaultAsync(a => a.ProductId == cartItem.ProductId && a.ProductTypeId == cartItem.ProductTypeId && a.UserId == userId);

         if(dbCartItem == null)
         {
            return new ServiceResponse<bool> { Data= false ,Message="Item doesn't exist", Success=false};
         }
         dbCartItem.Quantity= cartItem.Quantity;

         _context.SaveChangesAsync();

         return new ServiceResponse<bool> { Data = true, Message="", Success=true};
      }

      public async Task<ServiceResponse<bool>> RemoveCartItem(int productId, int productTypeId)
      {

         var userId = _authService.GetUserId();
         var dbCartItem = await _context.CartItems.FirstOrDefaultAsync(a => a.ProductId == productId && a.ProductTypeId == productTypeId && a.UserId == userId);

         if (dbCartItem == null)
         {
            return new ServiceResponse<bool> { Data = false, Message = "Item doesn't exist", Success = false };
         }
         
         _context.CartItems.Remove(dbCartItem);

         _context.SaveChangesAsync();

         return new ServiceResponse<bool> { Data = true, Message = "", Success = true };
      }

     // private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue((ClaimTypes.NameIdentifier)));

   }
}
