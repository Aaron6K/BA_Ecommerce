using BA_Ecommerce.Server.Services.AuthService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BA_Ecommerce.Server.Controllers
{
   [Route("api/[controller]")]
   [ApiController]
   public class AuthController : ControllerBase
   {
        public readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
         _authService = authService;   
        }
      [HttpPost("register")]
      public async   Task<ActionResult<ServiceResponse<int>>> Register(UserRegister request)
      {
         var response = await _authService.Register(new Shared.User
         {
            Email = request.Email,

         },
         request.Password);

         if (!response.Success)
         {
            return BadRequest(response);

         }

         return Ok(response);
      }

      [HttpPost("login")]
      public async Task<ActionResult<ServiceResponse<string>>> Login(UserLogin request)
      {
         var response = await _authService.Login(request.Email, request.Password);
         if (!response.Success)
         {
            return BadRequest(response);
         }
         return Ok(response);
      }

    }
}
