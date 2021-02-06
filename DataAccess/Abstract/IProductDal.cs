using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //Ürüne ait özel operasyonlar buraya yazılacak
        //Örn: List<ProductDto> GetDetails(); //sadece ProductDal için implementasyon isteyecek
        List<ProductDetailDto> GetProductDetails();
    }
}
//Code Refactoring = kodun iyileştirilmesi