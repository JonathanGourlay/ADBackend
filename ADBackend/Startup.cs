using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ADBackend.DAL;
using ADBackend.DAL.Interfaces;
using ADBackend.DAL.Repository;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace ADBackend
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
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddPolicy("CORS", builder =>
                {
                    builder.WithOrigins("http://localhost:3000", "https://ad-front-end-dot-ad-assignment-one.ey.r.appspot.com").AllowAnyHeader().AllowAnyMethod().AllowCredentials();
                });
            });

            services.AddSwaggerGen(c =>
            {
                c.UseOneOfForPolymorphism();

                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AD API",
                    Version = "1.0",
                    Description =
                        "An api for my AD assignment.",
                    Contact = new OpenApiContact
                    {
                        Name = "Jonathan",
                        Email = "Jonathan@theroad.uk"
                    },
                });

                

            });

            services.AddSingleton<IItemRepo, ItemRepo>();
            services.AddSingleton<IUserRepo, UserRepo>();
            services.Configure<ConnectionStrings>(option =>
                Configuration.GetSection("ConnectionStrings").Bind(option));
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", "C:\\Users\\acid_\\UserSecrets\\AD Assignment One secrets.json");
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseCors("CORS");

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("../swagger/v1/swagger.json", "AD Backend");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger");
                    return Task.FromResult(0);
                });
                endpoints.MapControllers();
            });

            FirebaseApp.Create(new AppOptions()
            {
                Credential = GoogleCredential.GetApplicationDefault(),
            });
        }
    }
}
