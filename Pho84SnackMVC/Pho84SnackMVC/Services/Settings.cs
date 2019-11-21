using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pho84SnackMVC.Services
{
   public class Settings
   {
      public const string NlogConfigFileName = "nlog.config";
      public const string DefaultConnectionString = "Default";

      public const string CultureEnglish = "en";
      public const string CultureGerman = "de";

      public const string SchemeApplication = "Application";
      public const string SchemeExternal = "External";
   }

   public class AuthenticationSettings
   {
      public const string ClaimTypeAccessToken = "AccessToken";
      public const string ClaimTypeAccessTokenExpiredAt = "AccessTokenExpiredAt";
      public const string ClaimTypeActive = "Active";

      public const string TokenAccessToken = "access_token";
      public const string TokenExpiredAt = "expires_at";
   }

   public class PolicySettings
   {
      public const string AdminOnly = "AdminOnly";
      public const string Active = "Active";
   }
}
