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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
