using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;

namespace Pho84SnackMVC
{
   public class Startup
   {
      public Startup(IConfiguration configuration)
      {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         services.Add(new ServiceDescriptor(typeof(Pho84SnackContext), new Pho84SnackContext(Configuration.GetConnectionString(Settings.DefaultConnectionString))));
         services.AddScoped<ICompanyInfoRepository, CompanyInfoRepository>();
         services.AddScoped<ICategoryRepository, CategoryRepository>();
         services.AddScoped<IProductRepository, ProductRepository>();
         services.AddScoped<IPriceRepository, PriceRepository>();
         services.AddScoped<IUserRepository, UserRepository>();

         var deCulture = new CultureInfo(Settings.CultureGerman);
         var enCulture = new CultureInfo(Settings.CultureEnglish);

         enCulture.NumberFormat.CurrencyDecimalSeparator = ".";
         enCulture.NumberFormat.NumberDecimalSeparator = ".";
         deCulture.NumberFormat.CurrencyDecimalSeparator = ".";
         deCulture.NumberFormat.NumberDecimalSeparator = ".";
         services.Configure<RequestLocalizationOptions>(o =>
         {
            o.SupportedCultures = new List<CultureInfo> { deCulture, enCulture };
            o.SupportedUICultures = new List<CultureInfo> { deCulture, enCulture };
            o.DefaultRequestCulture = new RequestCulture(deCulture);
         });

         services
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie()
            .AddCookie(Settings.SchemeExternal)
            .AddFacebook(o =>
            {
               o.SignInScheme = Settings.SchemeExternal;
               o.AppId = Configuration["Facebook:AppId"];
               o.AppSecret = Configuration["Facebook:AppSecret"];
               o.SaveTokens = true;
            });

         services.ConfigureApplicationCookie(o =>
         {
            o.LoginPath = "/Account/Login";
            o.LogoutPath = "/Account/Logout";
            o.SlidingExpiration = true;
         });

         services
            .AddMvc()
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env)
      {
         if (env.IsDevelopment())
         {
            app.UseDeveloperExceptionPage();
         }
         else
         {
            app.UseExceptionHandler("/Error");
            app.UseHsts();
         }

         app.UseRequestLocalization();
         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseCookiePolicy();
         app.UseAuthentication();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Category}/{action=Index}/{id?}");
         });
      }
   }
}
