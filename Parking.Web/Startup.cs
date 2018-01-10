using System.Collections.Generic;
using System.Globalization;
using System.IO;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Parking.Domain.Services;
using Parking.Interfaces;
using Parking.Interfaces.Application;
using Parking.Interfaces.ApplicationLayer_Interface;
using Parking.Repository;
using Parking.Repository.DataStore;

namespace Web
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Setting up the Culture
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("en-AU");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("en-AU"), new CultureInfo("en-AU") };
                options.RequestCultureProviders.Clear();
            });


            // Add framework services.
            services.AddMvc();
            

            //Configuration Registration
            services.AddSingleton<IConfiguration>(Configuration);

            //Registering Application Dependencis            
            services.AddTransient<IParkingRates, ParkingRatesReporsitory>();            
            services.AddTransient<IParkingRatesCalculator, ParkingRatesCalculator>();


            services.AddTransient<IHourlyRates, HourlyRates>();
            services.AddTransient<IFlatRates, FlatRates>();


            //AutoMapper initialization and configuration
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
                cfg.AllowNullDestinationValues = true;
                cfg.AllowNullCollections = true;
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

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



            app.UseRequestLocalization();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");            
            });
         
        }
    }
}
