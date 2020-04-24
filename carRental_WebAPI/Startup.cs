using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using carRental_WebAPI.Models;
using carRental_WebAPI.Repositories;

namespace carRental_WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private const string AllowAllOriginsPolicy = "AllowAllOriginsPolicy";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<CarRentalContext>(options => options.UseSqlServer(Configuration.GetConnectionString("CarRentalDatabase")));
            services.AddTransient<IAuthRepository, AuthRepository>();
            services.AddTransient<ICarBrandsRepository, CarBrandsRepository>();
            services.AddTransient<ICarModelsRepository, CarModelsRepository>();
            services.AddTransient<ICarsRepository, CarsRepository>();
            services.AddTransient<ICarTypesRepository, CarTypesRepository>();
            services.AddTransient<IRentalTransactionsRepository, RentalTransactionsRepository>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["TokenKey"])),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors(options =>
            {
                options.AddPolicy(AllowAllOriginsPolicy,
                builder =>
                {
                    builder.AllowAnyOrigin();
                });
            });
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {
                    Title = "Car Rental API",
                    Version = "v1",
                    Description = "ASP.NET Core 3.1 Web API as a practical test for Altomobile LLC.",
                    Contact = new OpenApiContact
                    {
                        Name = "Julio Moncisbays",
                        Email = "juliomoncisbays@gmail.com"
                    }
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Car Rental API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(AllowAllOriginsPolicy);

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
