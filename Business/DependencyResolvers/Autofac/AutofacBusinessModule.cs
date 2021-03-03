﻿using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.CCS;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.DependencyResolvers.Autofac
{
    //Burada Autofac IoC altyapısı kullanılmıştır (aşağıda kurulan ortam açıklanmıştır)!
    //Dot.Net in IoC si yerine Autofac i WebAPI Program.cs ye ekliyoruz bunun için
    // CreateHostBuilder fonuksiyonuna 
    //.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    //ve
    //.ConfigureContainer<ContainerBuilder>(builder=>
    //{
    //    builder.RegisterModule(new AutofacBusinessModule());
    //})
    //kodlarını ekliyoruz
    
    /// ///////////////////////////////////////////////////////////////////////////////////////////
    //Buradaki IoC yerine Manage nuget packages'dan Business katmanına Autofac ve Autofac.Extras
    //Autofac teknolojisini kuruyoruz "Autofac" ve "Autofac.Extras.DynamicProxy" yi kuruyoruz

    //Autofac,Ninject,CastleWindsor,StructureMap,LightInject,DryInject -->IoC Container için yapılardır bunlardan birini kullanacağız
    //Postrsharp (ücretli bir IoC)

    //Net.core ile Autofac i kullanabiliriz (bize IoC container altyapısı sunmakta)
    //Autofac bize AOP imkanı sunmakta ondan dolayı onu kullanacağız (2. ek çok kullanılan Ninject)

    //Biz AOP (Aspect Oriented Programming) yapacağız 
    //AOP bir methodun önünde, sonunda çalışan kod parçalıklarını AOP mimarisi ile vereceğiz

    //Biri constructorda IProductService isterse ProductManager() i new le ve ona ver !!!
    //services.AddSingleton<IProductService, ProductManager>();//Bana arka planda bir referans oluştur IoC bizim yerine new ler
    //services.AddSingleton<IProductDal, EFProductDal>();

    //services.AddTransient - datalı referanslar için
    //services.AddScoped  - datalı referanslar için

    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ProductManager>().As<IProductService>().SingleInstance();//Web Api deki karşılığı services.AddSingleton<IProductService, ProductManager>();            
            builder.RegisterType<EFProductDal>().As<IProductDal>().SingleInstance();//Web Api deki karşılığı services.AddSingleton<IProductDal, EFProductDal>(); 

            builder.RegisterType<CategoryManager>().As<ICategoryService>().SingleInstance();
            builder.RegisterType<EfCategoryDal>().As<ICategoryDal>().SingleInstance();

            builder.RegisterType<FileLogger>().As<ILogger>().SingleInstance();

            builder.RegisterType<UserManager>().As<IUserService>();
            builder.RegisterType<EfUserDal>().As<IUserDal>();

            builder.RegisterType<AuthManager>().As<IAuthService>();
            builder.RegisterType<JwtHelper>().As<ITokenHelper>();            

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
