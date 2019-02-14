using System;
using AutoMapper;
using FleetMgmt.Data;
using FleetMgmt.Repository.Implementations;
using FleetMgmt.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Binder;

namespace FleetMgmt.Web
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
            services.AddCors(options =>
            {
                options.AddPolicy("default", policy =>
                {
                    policy.WithOrigins(Configuration.GetSection("CORSOrigins").Get<string[]>())
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });

            // Configure DbContext
            services
                .AddDbContext<FmDbContext>(options =>
                   options
                   .UseSqlServer(Configuration
                       .GetConnectionString("DefaultConnection")));

            //            services.AddMvc();
            services
                .AddAutoMapper()
                .AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services
                .AddTransient<IVehicleRepository, VehicleRepository>()
                .AddTransient<IDriverRepository, DriverRepository>()
                .AddTransient<ITripRepository, TripRepository>();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetSection("AuthAuthority").Value;
                    options.RequireHttpsMetadata = Convert.ToBoolean(Configuration.GetSection("AuthRequireHttps").Value);
                    options.ApiName = Configuration.GetSection("AuthApiName").Value;
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("default");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
