using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Web;
using Pho84SnackMVC.Models;
using Pho84SnackMVC.Services;
using System;

namespace Pho84SnackMVC
{
   public class Program
   {
      public static void Main(string[] args)
      {
         NLog.Logger logger = null; 
         try
         {
            logger = NLogBuilder.ConfigureNLog(Settings.NlogConfigFileName).GetCurrentClassLogger();
            logger.Info("Program's Initialization starts");
            var host = CreateWebHostBuilder(args).Build();
            InitializeDb(host);
            host.Run();
            logger.Info("Program's Initialization completed without error");
         }
         catch (Exception ex)
         {
            logger.Error(ex, "Program stopped due to exception");
            throw;
         }
         finally
         {
            NLog.LogManager.Shutdown();
         }
      }

      private static void InitializeDb(IWebHost host)
      {
         using (var scope = host.Services.CreateScope())
         {
            var services = scope.ServiceProvider;
            var companyInfoService = services.GetRequiredService<ICompanyInfoService>();
            var categoryService = services.GetRequiredService<ICategoryService>();
            DbInitializer.Initialize(companyInfoService, categoryService);
         }
      }

      public static IWebHostBuilder CreateWebHostBuilder(string[] args)
      {
         return WebHost
            .CreateDefaultBuilder(args)
            .UseStartup<Startup>()
            .ConfigureLogging(logging =>
            {
               logging.ClearProviders();
               logging.SetMinimumLevel(LogLevel.Debug);
            })
            .UseNLog();
      }
   }
}
