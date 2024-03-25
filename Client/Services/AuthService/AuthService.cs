
namespace BA_Ecommerce.Client.Services.AuthService
{
   public class AuthService:IAuthService
   {
      private readonly HttpClient _http;

      public AuthService(HttpClient http)
        {
            _http=http;
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
