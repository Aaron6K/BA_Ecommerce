namespace BA_Ecommerce.Client.Services.OrderService
{
   public interface IOrderService
   {
      public Task PlaceOrder();
      public Task<List<OrderOverviewResponse>> GetOrders();
      public Task<OrderDetailsResponse> GetOrderDetails(int orderId);
   }
}
   