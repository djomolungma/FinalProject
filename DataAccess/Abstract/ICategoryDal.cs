using Core.DataAccess;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Abstract
{
    //interface lerin operasyonları public dir
    //Generic Repository Design Pattern
    public interface ICategoryDal : IEntityRepository<Category>
    {        
        
    }
}
