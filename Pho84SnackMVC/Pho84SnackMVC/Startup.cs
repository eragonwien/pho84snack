﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
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
         services.AddScoped<IErrorService, ErrorService>();

         var enCulture = new CultureInfo("en");
         var deCulture = new CultureInfo("de");

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

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
         }

         app.UseRequestLocalization();
         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseCookiePolicy();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Category}/{action=Index}/{id?}");
         });
      }
   }
}
