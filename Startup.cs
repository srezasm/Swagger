using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Swagger {
   public class Startup {
      public Startup(IConfiguration configuration) {
         Configuration = configuration;
      }

      public IConfiguration Configuration { get; }

      public void ConfigureServices(IServiceCollection services) {
         services.AddControllersWithViews();

         services.AddSwaggerGen(p => {
            p.SwaggerDoc("v1", new OpenApiInfo {
               Title = "Library Api",
               Version = "v1",
               Description = "this is a test project fot testing swagger usage!",
               Contact = new OpenApiContact {
                  Email = "reza.seyed.dev@gmail.com",
                  Name = "SrezaS",
                  Url = new Uri("https://www.srezas.com")
               },
               License = new OpenApiLicense {
                  Name = "License",
                  Url = new Uri("https://www.srezas.com")
               }
            });
         });

         services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Latest);
      }

      public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
         if (env.IsDevelopment()) {
            app.UseDeveloperExceptionPage();
         } else {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
         }
         app.UseHttpsRedirection();
         app.UseStaticFiles();
         app.UseRouting();
         app.UseAuthorization();

         app.UseSwagger();
         app.UseSwaggerUI(p => {
            p.SwaggerEndpoint("/swagger/v1/swagger.json", "my api v1");
         });

         app.UseEndpoints(endpoints => {
            endpoints.MapControllerRoute(
             "default",
             "{controller=Home}/{action=Index}/{id?}");
         });
      }
   }
}
