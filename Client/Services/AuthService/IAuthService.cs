﻿namespace BA_Ecommerce.Client.Services.AuthService
{
   public interface IAuthService
   {
      Task<ServiceResponse<int>> Register(UserRegister request);
      Task<ServiceResponse<string>> Login(UserLogin userLogin);

      Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);

      Task<bool> IsUserAuthenticated();

   }
}
