using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI
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


            //Autofac,Ninject,CastleWindsor,StructureMap,DryInject -->Ioc Container için yapýlardýr bunlardan birini kullanacaðýz
            //Net.core ile Autofac i kullanabiliriz (bize IoC container altyapýsý sunmakta)
            //Autofac bize AOP imkaný sunmakta ondan dolayý onu kullanacaðýz (2. ek çok kullanýlan Ninject)

            //Biz AOP (Aspect Oriented Programming) yapacaðýz 
            //AOP bir methodun önünde, sonunda çalýþan kod parçalýklarýný AOP mimarisi ile vereceðiz

            //Biri constructorda IProductService isterse ProductManager() i new le ve ona ver !!!
            services.AddSingleton<IProductService, ProductManager>();//Bana arka planda bir referans oluþtur IoC bizim yerine new ler
            services.AddSingleton<IProductDal, EFProductDal>();
            
            //services.AddTransient - datalý referanslar için
            //services.AddScoped  - datalý referanslar için
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
