
using BA_Ecommerce.Client.Services.AuthService;
using BA_Ecommerce.Server.Services.CartService;
using System.Security.Claims;

namespace BA_Ecommerce.Server.Services.OrderService
{
   public class OrderService : IOrderService
   {
      private readonly DataContext _dataContext;
      private readonly ICartService _cartService;
      private readonly BA_Ecommerce.Server.Services.AuthService.IAuthService   _authService;
     // private readonly IHttpContextAccessor _contextAccessor;

      public OrderService(
               DataContext dataContext,
               ICartService cartService,
               //IHttpContextAccessor contextAccessor,
               BA_Ecommerce.Server.Services.AuthService.IAuthService   authService
         )
      {

         _dataContext = dataContext;
         _cartService = cartService;
         _authService = authService;
         //_contextAccessor = contextAccessor;

      }

      public async Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders()
      {
         var userId=_authService.GetUserId();
         var orderResponse= new ServiceResponse<List<OrderOverviewResponse>>();

         var orders= await _dataContext.Orders.
                     Include(a=> a.OrderItems).
                     ThenInclude(b=>b.Product).
                     Where(a=>a.UserId== userId).OrderByDescending(o=>o.OrderDate).ToListAsync();
         var OrderOverviewResponses= new List<OrderOverviewResponse>();

         foreach ( var order in orders )
         {
            OrderOverviewResponses.Add(
                new OrderOverviewResponse()
                {
                   Id= order.Id,
                   OrderDate=order.OrderDate,
                    TotalPrice=order.TotalPrice,
                    Product=order.OrderItems.Count>1 ? 
                    $"{order.OrderItems.First().Product.Title} and {order.OrderItems.Count -1} more" :order.OrderItems.First().Product.Title ,
                     ProductImageUrl=order.OrderItems.First().Product.ImageUrl 
                }
             );

         }
         orderResponse.Data = OrderOverviewResponses;
         return orderResponse;
      }

      public async Task<ServiceResponse<bool>> PlaceOrder()
      {
         var userId =_authService.GetUserId();
         var products = (await _cartService.GetDBCartProducts()).Data;
         decimal totalPrice = 0;
         products.ForEach(product => totalPrice += product.Quantity * product.Price);

         var orderItems = new List<OrderItem>();
         foreach (var product in products)
         {
            orderItems.Add(
               new OrderItem
               {
                  ProductId = product.ProductId,
                  ProductTypeId = product.ProductTypeId,
                  Qunatity = product.Quantity,
                  TotalPrice = product.Price * product.Quantity

               }
            );
         }

         var order = new Order
         {
            UserId = userId,
            OrderDate = DateTime.Now,
            TotalPrice = totalPrice,
            OrderItems = orderItems
         };

         _dataContext.Orders.Add(order);

         _dataContext.CartItems.RemoveRange(_dataContext.CartItems.Where(ci => ci.UserId == userId));

         await _dataContext.SaveChangesAsync();

         return new ServiceResponse<bool> { Data = true };

      }
   }
}
