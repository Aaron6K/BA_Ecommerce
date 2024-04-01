using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;

namespace BA_Ecommerce.Client
{
   public class CustomAuthStateProvider : AuthenticationStateProvider
   {
      private readonly ILocalStorageService _localStorageService;
      private readonly HttpClient _http;

      public CustomAuthStateProvider(ILocalStorageService localStorageService, HttpClient http)
      {
         _localStorageService = localStorageService;
         _http = http;
      }
      public override async Task<AuthenticationState> GetAuthenticationStateAsync()
      {

         string authToken = await _localStorageService.GetItemAsStringAsync("authToken");

         var identity = new ClaimsIdentity();
         _http.DefaultRequestHeaders.Authorization = null;

         if (!string.IsNullOrEmpty(authToken))
         {
            try
            {
               identity = new ClaimsIdentity(ParseClaimsFromJwt(authToken), "jwt");

               _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken.Replace("\"", ""));
            }
            catch (Exception ex)
            {
               await _localStorageService.RemoveItemAsync("authToken");
               identity = new ClaimsIdentity();
            }
         }

         var user = new ClaimsPrincipal(identity);
         var state = new AuthenticationState(user);

         NotifyAuthenticationStateChanged(Task.FromResult(state));

         return state;

      }

      /*
         Each character is used to represent 6 bits (log2(64) = 6).
         Therefore 4 chars are used to represent 4 * 6 = 24 bits = 3 bytes.
         So you need 4*(n/3) chars to represent n bytes, and this needs to be rounded up to a multiple of 4.
         The number of unused padding chars resulting from the rounding up to a multiple of 4 will obviously be 0, 1, 2 or 3.

       */

      private byte[] ParseBase64WithoutPadding(string base64)
      {
         //Given a string with length of n , the base64 length will be enter  4[n/3]  
         switch (base64.Length % 4)
         {
            //The max number of padding for a sequence can be = or ==.
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
         }
         return Convert.FromBase64String(base64);
      }

      private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
      {
         var payload = jwt.Split('.')[1];
         var jsonBytes = ParseBase64WithoutPadding(payload);

         var keyValuePairs = JsonSerializer
               .Deserialize<Dictionary<string, object>>(jsonBytes);

         var claims = keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));

         return claims;
      }
   }
}
