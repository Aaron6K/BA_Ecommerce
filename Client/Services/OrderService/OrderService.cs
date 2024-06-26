﻿
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace BA_Ecommerce.Client.Services.OrderService
{
   public class OrderService : IOrderService
   {
      private readonly HttpClient _http;
      private readonly AuthenticationStateProvider _authStateProvider;
      private readonly NavigationManager _navigationManager;

      public OrderService(HttpClient http,
         AuthenticationStateProvider authStateProvider ,
         NavigationManager navigationManager)
        {
         _http = http;
         _authStateProvider = authStateProvider;
         _navigationManager = navigationManager;
        }

      private async Task<bool> IsAuthenticated()
      {
         return (await _authStateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
      }
      public async Task PlaceOrder()
      {
         if(await IsAuthenticated())
         {
            await  _http.PostAsync("api/order", null);
         }
         else
         {
            _navigationManager.NavigateTo("login");
         }
      }

      public async Task<List<OrderOverviewResponse>> GetOrders()
      {
         if (await IsAuthenticated())
         {
            var result = await _http.GetFromJsonAsync<ServiceResponse<List<OrderOverviewResponse>>>("api/order");
            
             return result.Data;
              
         }
         else
         {
            return  new List<OrderOverviewResponse>();
         }
      }

      public async Task<OrderDetailsResponse> GetOrderDetails(int orderId)
      {
         var response = await _http.GetFromJsonAsync<ServiceResponse<OrderDetailsResponse>>($"api/order/{orderId}");
         
         return response.Data;
      }
   }
}
