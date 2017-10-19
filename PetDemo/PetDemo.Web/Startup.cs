using System.IO;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PetDemo.Proxy;
using PetDemo.Proxy.Interfaces;
using PetDemo.Service;
using PetDemo.Service.Interfaces;
using PetDemo.Web.ModelMappers;
using PetDemo.Web.ModelMappers.Inerfaces;
using Serilog;

namespace PetDemo.Web
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
            services.AddHttpClient(Configuration["aglWebApiBaseAddress"]);
            services.AddScoped(typeof(IJsonDeserializer<>), typeof(JsonDeserializer<>));
            services.AddScoped<IHttpHandler, HttpHandler>();
            services.AddScoped<IPeopleService, PeopleService>();
            services.AddScoped<ICatViewModelMapper, CatViewModelMapper>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env,
            ILoggerFactory loggerFactory,
            IApplicationLifetime applicationLifetime,
            HttpClient httpClient)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            //Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.RollingFile(Path.Combine(env.ContentRootPath, "PetDemo-{Date}.txt"))
                .CreateLogger();

            loggerFactory.AddSerilog();

            //dispose of httpClient which is registered as signleton when application is stopping
            applicationLifetime.ApplicationStopping.Register(httpClient.Dispose);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
