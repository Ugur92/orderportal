using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NLog.Web;
using System;
using System.IO;

namespace LedSiparisModulu
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
			try
			{
				CreateHostBuilder(args).Build().Run();
			}
			catch (Exception ex)
			{
				logger.Error(ex, "Stopped program because of exception");
			}
			finally
			{
				NLog.LogManager.Shutdown();
			}
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureLogging(logging =>
				{
					logging.ClearProviders()
						.SetMinimumLevel(LogLevel.Trace)
						.AddDebug()
						.AddConsole();
				})
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseContentRoot(Directory.GetCurrentDirectory())
						.UseStartup<Startup>()
						.UseNLog();
				});
	}
}
