using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using XeroProducts.Data.Repository;
using XeroProducts.Data.Context;
using XeroProducts.Data.UnitOfWork;
using XeroProducts.MediatR.Feature.ProductAggregate.Queries;
using MediatR;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;
using System.IO;

namespace XeroProducts
{
    /// <summary>
    /// Startup used to configure services for the application
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// 
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)        
        {
            services.AddControllers();
            services.AddHttpContextAccessor();
            services.AddSingleton(Configuration);
            //services.AddSingleton<ILoggerManager, LoggerManager>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMediatR(typeof(GetAllProductsQuery).Assembly);
            services.AddControllers().AddJsonOptions(opts=>opts.JsonSerializerOptions.PropertyNamingPolicy=null);
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddDbContext<ProductsContext>(options => options.UseSqlite(Configuration.GetConnectionString("Products"),b=>b.MigrationsAssembly("XeroProducts.API")));
           
            #region [ Swagger ]
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Xero Produts Web API",
                    Description = "Xero Produts ASP.NET Core Web API",
                   
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
            #endregion

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
           // app.ConfigureExceptionHandler(logger);

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            #region [ Swagger ]
            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Xero Products Web API");
                c.RoutePrefix = "docs";
            });
            #endregion

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
