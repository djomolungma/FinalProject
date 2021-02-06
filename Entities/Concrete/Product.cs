using Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Concrete
{
    //public bu class'a diğer katmanlar da erişebilsin
    //internal demek sadece Entities erişebilir demek, varsayılan erişim belirleyicisi Interla'dır
    public class Product : IEntity
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; }
        public short UnitsInStock { get; set; }//veritabanında smallint 
        public decimal UnitPrice { get; set; }
    }
}
