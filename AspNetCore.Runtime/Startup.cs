using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCore.Runtime
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
			services.AddRazorPages();
			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

			app.Use(async (context, next) =>
			{
				context.Items["TIME_1"] = DateTime.Now;
				await next.Invoke();
			});

			app.UseHttpsRedirection();

			app.Use(async (context, next) =>
			{
				context.Items["TIME_2"] = DateTime.Now;
				await next.Invoke();
			});

			app.UseStaticFiles();

			app.Use(async (context, next) =>
			{
				context.Items["TIME_3"] = DateTime.Now;
				await next.Invoke();
			});

			app.UseRouting();

			app.Use(async (context, next) =>
			{
				context.Items["TIME_4"] = DateTime.Now;
				await next.Invoke();
			});

			app.UseAuthorization();

			app.Use(async (context, next) =>
			{
				context.Items["TIME_5"] = DateTime.Now;
				await next.Invoke();
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapRazorPages();
				endpoints.MapControllers();
			});
		}
	}
}
