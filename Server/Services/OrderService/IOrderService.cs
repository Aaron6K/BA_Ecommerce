 
namespace BA_Ecommerce.Server.Services.OrderService
{
   public interface IOrderService
   {
      public Task<ServiceResponse<bool>> PlaceOrder();

      public Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders();

      public Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId);
   }
}
