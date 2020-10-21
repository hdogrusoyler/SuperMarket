using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SuperMarket.Project.BusinessLogic;
using SuperMarket.Project.BusinessLogic.Concrete;
using SuperMarket.Project.DataAccess;
using SuperMarket.Project.DataAccess.EntityFramework;

namespace SuperMarket.Project.WebAPI
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
            services.AddDbContext<DataContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                options.UseSqlServer(Configuration.GetConnectionString("DataConnection"), b => b.MigrationsAssembly("Solution.Project.WebApplication.Presentation"));
            });

            services.AddControllers();

            services.AddScoped<IUserDal, EfUserDal>();
            services.AddScoped<IProductDal, EfProductDal>();
            services.AddScoped<ICategoryDal, EfCategoryDal>();
            services.AddScoped<ICartDal, EfCartDal>();
            services.AddScoped<ICartProductDal, EfCartProductDal>();
            services.AddScoped<ISalesInformationDal, EfSalesInformationDal>();
            services.AddScoped<IPaymentTypeDal, EfPaymentTypeDal>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IUserService, UserManager>();
            services.AddScoped<IProductService, ProductManager>();
            services.AddScoped<ICategoryService, CategoryManager>();
            services.AddScoped<ICartService, CartManager>();
            services.AddScoped<ICartProductService, CartProductManager>();
            services.AddScoped<ISalesInformationService, SalesInformationManager>();
            services.AddScoped<IPaymentTypeService, PaymentTypeManager>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
