using System;
using AutoMapper;
using FleetMgmt.Data;
using FleetMgmt.Domain;
using FleetMgmt.Repository.Implementations;
using FleetMgmt.Repository.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

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
            
            services
                .AddAutoMapper(typeof(Mappings))
                .AddMvcCore()
                .AddAuthorization()
                .AddNewtonsoftJson();

            services
                .AddScoped<IVehicleRepository, VehicleRepository>()
                .AddScoped<IDriverRepository, DriverRepository>()
                .AddScoped<ITripRepository, TripRepository>()
                .AddScoped<IAccidentRepository, AccidentRepository>()
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IUserSession, UserSession>()
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = Configuration.GetSection("AuthAuthority").Value;
                    options.RequireHttpsMetadata = Convert.ToBoolean(Configuration.GetSection("AuthRequireHttps").Value);
                    options.ApiName = Configuration.GetSection("AuthApiName").Value;
                });
            
            services.AddHealthChecks();
            
            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Fleet Management API", Version = "v1" });
            });
            
            services.AddSwaggerGenNewtonsoftSupport();
            
            services.AddMvc();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("default");
            app.UseAuthentication();
            
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Fleet Management API");
                // c.RoutePrefix = string.Empty;
            });
            
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "api/{controller}/{id}");
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
