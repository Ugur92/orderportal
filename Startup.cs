using DataAccess;
using DataAccess.LogoModels;
using DataAccess.Models;
using DataTables.AspNet.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;

namespace LedSiparisModulu
{
	public class Startup
	{
		private readonly IConfiguration _configuration;
		private readonly IWebHostEnvironment _environment;

		public Startup(IConfiguration configuration, IWebHostEnvironment environment)
		{
			_configuration = configuration;
			_environment = environment;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.RegisterDataTables((modelBindingContext) =>
			{
				var modelKeys = modelBindingContext.HttpContext.Request.Form.Keys
					.Where(m => !m.StartsWith("columns"))
					.Where(m => !m.StartsWith("order"))
					.Where(m => !m.StartsWith("search"))
					.Where(m => m != "draw")
					.Where(m => m != "start")
					.Where(m => m != "length")
					.Where(m => m != "_"); //Cache bust

				var dictionary = new Dictionary<string, object>();
				foreach (string key in modelKeys)
				{
					var value = modelBindingContext.ValueProvider.GetValue(key).FirstValue;
					if (value.Length > 0)
						dictionary.Add(key, value);
				}
				return dictionary;
			}, false);
			var builder = services.AddControllersWithViews();

#if DEBUG
			if (_environment.IsDevelopment())
			{
				builder.AddRazorRuntimeCompilation();
			}
#endif
            services.AddDbContextPool<LAF_BUPILICContext>(x => x.UseSqlServer(_configuration.GetSection("ConnectionString:DefaultConnection").Value));
            services.AddDbContextPool<LogoDbContext>(x => x.UseSqlServer(_configuration.GetSection("ConnectionString:LogoConnection").Value));
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.LoginPath = "/oturum-ac";
				});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Account}/{action=Login}/{id?}");
			});
		}
	}
}
