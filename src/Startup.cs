using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.ApplicationServices;
using Api.ApplicationServices.Interfaces;
using Api.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Cors.Internal;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using CustomConfigDockerSecrets;
using System.IO;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json")
                .AddDockerSecrets(opt => opt.Optional = true)
                .AddEnvironmentVariables()
                .Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {       
            //Console.WriteLine($"Conexao mongo {Configuration.GetConnectionString("MongoConnection")}");

            services.AddCors(opt => opt.AddPolicy("CorsPolicy",
            builder => 
                builder.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()));

            
            services.AddMvc();
            services.Configure<MvcOptions>(opt => 
            opt.Filters.Add(new CorsAuthorizationFilterFactory("CorsPolicy")));

            services.AddTransient<IProductApplicationService, ProductApplicationService>();
            services.AddSingleton<IProductContext>(new ProductContext(Configuration.GetConnectionString("MongoConnection")));      

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Price Store Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors("CorsPolicy");

            var option = new RewriteOptions();             
            option.AddRedirect("^$", "swagger");              
            app.UseRewriter(option);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Price Store Api V0.1");
            });

            app.UseMvc();            
        }
    }
}
