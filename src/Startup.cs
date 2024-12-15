﻿using MarketplaceBackend.Data;
using MarketplaceBackend.Helpers;
using MarketplaceBackend.Interfaces;
using MarketplaceBackend.Repositories;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.OpenApi.Models;

namespace MarketplaceBackend
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Chasman Shop API",
                    Version = "v1",
                    Description = "API for Chasman Shop"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' followed by a space and your JWT token.\n\nExample: Bearer eyJhbGciOiJIUzI1NiIsInR5..."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        []
                    }
                });
            });

            services.AddSingleton<MongoDbContext>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ProductService>();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<UserService>();

            services.AddScoped<IGoogleAuthService, GoogleAuthService>();

            services.AddScoped<ITokenService, AuthTokenProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseCors(policy =>
            {
                policy.WithMethods("POST", "GET", "PATCH", "PUT")
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(origin => new Uri(origin).Host == "your-frontend.com");
            });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Chasman API v1");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
