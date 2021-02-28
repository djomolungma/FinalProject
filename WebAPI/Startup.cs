using Business.Abstract;
using Business.Concrete;
using Core.Utilities.IoC;
using Core.Utilities.Security.Encription;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
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
            
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            //Microsoft.AspNetCore.Authentication.JwtBearer
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = tokenOptions.Issuer,
                        ValidAudience = tokenOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
                    };
                });
            ServiceTool.Create(services);

            //Buradaki IoC yerine Manage nuget packages'dan Business katman�na Autofac ve Autofac.Extras
            //Autofac teknolojisini kuruyoruz "Autofac" ve "Autofac.Extras.DynamicProxy" yi kuruyoruz

            //Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DryInject -->IoC Container i�in yap�lard�r bunlardan birini kullanaca��z
            //Postrsharp (�cretli bir IoC)

            //Net.core ile Autofac i kullanabiliriz (bize IoC container altyap�s� sunmakta)
            //Autofac bize AOP imkan� sunmakta ondan dolay� onu kullanaca��z (2. ek �ok kullan�lan Ninject)

            //Biz AOP (Aspect Oriented Programming) yapaca��z 
            //AOP bir methodun �n�nde, sonunda �al��an kod par�al�klar�n� AOP mimarisi ile verece�iz

            //Biri constructorda IProductService isterse ProductManager() i new le ve ona ver !!!
            //services.AddSingleton<IProductService, ProductManager>();//Bana arka planda bir referans olu�tur IoC bizim yerine new ler
            //services.AddSingleton<IProductDal, EFProductDal>();

            //services.AddTransient - datal� referanslar i�in
            //services.AddScoped  - datal� referanslar i�in


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

            app.UseAuthentication();//Biz ektedik yetkilenditme i�in

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
