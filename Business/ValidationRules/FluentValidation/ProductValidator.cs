using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.ValidationRules.FluentValidation
{
    //Manage Nuget packages add FluentValidation project
    //Kullanım dökümanlarına FluentValidation ın kendi sitesinden erişebiliriz
    public class ProductValidator : AbstractValidator<Product>
    {
        //kurallar Constructor ın yazılır
        public ProductValidator()
        {
            //Ctrl + K sonra Ctrl + D kodları hizalamak için
            RuleFor(p => p.ProductName).MinimumLength(2);
            RuleFor(p => p.ProductName).NotEmpty();
            RuleFor(p => p.UnitPrice).NotEmpty();
            RuleFor(p => p.UnitPrice).GreaterThan(0);
            RuleFor(p => p.UnitPrice).GreaterThan(10).When(p => p.CategoryId == 1);//kategori 1 olduğunda fiyat 10 dan büyük olmalı
            
            //Olmayan bir kural eklemek istersek, bir patterna uymasını istersek
            RuleFor(p => p.ProductName).Must(StartWithA).WithMessage("Ürünler A harfi ile başlamalı!");//Kendimiz mesaj verebiliriz
        }

        private bool StartWithA(string arg)
        {
            return arg.StartsWith("A");//arg burada ProductName i temsil eder
        }
    }
}
