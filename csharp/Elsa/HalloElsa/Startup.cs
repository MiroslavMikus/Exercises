using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Elsa;
using ElsaDashboard.Backend.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace HalloElsa
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddElsaCore(elsa => elsa
                .AddConsoleActivities()
                // .AddJavaScriptActivities()
                .AddHttpActivities());
            
            services.AddRazorPages(); 
            services.AddElsaDashboardUI(options => options.ElsaServerUrl = new Uri("https://localhost:11000"));
            services.AddElsaDashboardBackend(options => options.ServerUrl = new Uri("https://localhost:11000"));
            // .AddElsa(elsa => elsa
                //      .AddEntityFrameworkStores<SqliteContext>(options =>
                //      {
                //          options.UseSqlite("Data Source=C:/temp/elsa.db;");
                //      }))
                // .AddActivity<TestActivity>()
                // .AddHttpActivities()
                // .AddConsoleActivities()
                // .AddTimerActivities()
                // .AddElsaDashboard();

            services.AddNotificationHandlers(typeof(SayHelloJavaScriptHandler));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseHttpActivities();
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HalloElsa v1"));
            }

            // app.UseHttpsRedirection();
            
            app.UseStaticFiles();
            
            app.UseRouting();

            app.UseElsaGrpcServices();
            
            app.UseEndpoints(endpoints =>
            {
                // Elsa Server uses ASP.NET Core Controllers.
                endpoints.MapControllers();
            });
        }
    }
}