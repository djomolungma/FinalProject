using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Constants
{
    public static class Messages
    {
        public static string ProductAdded = "Ürün eklendi";//public değişkenler büyük harf ile başlar, privetlar küçük harf ile başlar
        public static string ProductNameInvalid = "Ürün ismi geçersiz";
        public static string MaintenanceTime = "Bakım zamanı";
        public static string ProductsListed = "Ürünler listelendi";
        public static string UnitPriceInvalid = "Birim fiyat geçersiz.";

        public static string ProductCountOfCategoryError = "Bir kategoride en fazla 10 ürün olabilir";
        public static string ProductUpdated = "Ürün güncellendi";
        public static string ProductNameAlreadyExists = "Aynı ürün isminde kayıt mevcut";
        public static string CaregoryLimitExcededError = "Categori limiti aşıldı";
    }
}
