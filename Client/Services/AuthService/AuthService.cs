
namespace BA_Ecommerce.Client.Services.AuthService
{
   public class AuthService:IAuthService
   {
      private readonly HttpClient _http;
      private readonly AuthenticationStateProvider _authstateProvider;

      public AuthService(HttpClient http, AuthenticationStateProvider authStateProvide)
        {
            _http=http;
            _authstateProvider = authStateProvide;
        }

      public async Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request)
      {
          var result=await _http.PostAsJsonAsync("api/auth/change-password", request.Password);
          return await result.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
      }

      public async Task<bool> IsUserAuthenticated()
      {
         //IsUserAuthenticated
         return (await _authstateProvider.GetAuthenticationStateAsync()).User.Identity.IsAuthenticated;
      }

      public async Task<ServiceResponse<string>> Login(UserLogin request)
      {
         var results = await _http.PostAsJsonAsync("api/auth/login", request);

         return await results.Content.ReadFromJsonAsync<ServiceResponse<string>>();

      }

      public async Task<ServiceResponse<int>> Register(UserRegister request)
      {
         var results=await _http.PostAsJsonAsync("api/auth/register",request);
         return await results.Content.ReadFromJsonAsync<ServiceResponse<int>>();
      }

   }
}
