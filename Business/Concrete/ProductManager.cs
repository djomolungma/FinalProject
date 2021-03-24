using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConserns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        //Bir iş sınıfı başka sınıfları new lemez !!!
        //Business imn bildiği tek şey IProductDal !!! new yok
        IProductDal _productDal;
        ICategoryService _categoryService;
        //ILogger _logger;

        //public ProductManager(IProductDal productDal,ILogger logger)
        //{
        //    _productDal = productDal;
        //    _logger = logger;
        //}

        //Bir entity manager kendidi hariç başka bir dal i enjekte edemez ****
        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        //Hashing - Geri dönüşünü olmayan şifreleme
        //Encription - Geri dönüşü olan şifreleme; Decription - şifre çözme
        //Slting - Kullanıcının girdiği parolayı biraz daha güçlendirmek
        //Claim - admin veya editor yetkilerinden birine sahip olduğu iddaası
        [SecuredOperation("product.add,admin")]
        //4.Refactor edilmiş son kod
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)//Result döndürecek RestFull api de
        {
            //1. CleanCode spaggerri yöntem bir kategoride en fazla 10 ürün olabilir örneği
            //var result = _productDal.GetAll(p => p.CategoryId == product.CategoryId).Count;
            //if (result >= 10)
            //{
            //    return new ErrorResult(Messages.ProductCountOfCategoryError);
            //}



            //business codes
            //validations
            //businness kodu ayrı validation kodu ayrı yapılmalı !!!
            //iş kurallarını CleanCode olarak yazolmalı

            //1.kötü kod örneği
            //if (product.UnitPrice <= 0)
            //{
            //    return new ErrorResult(Messages.UnitPriceInvalid);
            //}
            //if (product.ProductName.Length < 2)
            //{
            //    //Antipatern == kötü kullanım

            //    //Magic strings
            //    return new ErrorResult(Messages.ProductNameInvalid);
            //}

            //2.biraz daha iyi kod örneği spagetti
            //var context = new ValidationContext<Product>(product);
            //ProductValidator productValidator = new ProductValidator();
            //var result = productValidator.Validate(context);
            //if (!result.IsValid)
            //{
            //    throw new ValidationException(result.Errors);
            //}

            //3.Refactor edilmiş daha iyi kod örneği
            //ValidationTool.Validate(new ProductValidator(), product);

            //1.loglama örnegi 2. adımda LoggingAspect eklenmeli
            //_logger.Log();
            //try
            //{
            //    _productDal.Add(product);

            //    return new SuccessResult(Messages.ProductAdded);
            //}
            //catch (Exception exception)
            //{
            //    _logger.Log();
            //}
            //return new ErrorResult();

            //2.CleanCode örneği 
            //if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            //{
            //    if (CheckIfSameProductNameExists(product.ProductName).Success)
            //    {
            //        _productDal.Add(product);

            //        return new SuccessResult(Messages.ProductAdded);
            //    }
            //}
            //return new ErrorResult();

            //3. CleanCode refactored
            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId), 
                CheckIfSameProductNameExists(product.ProductName),
                CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }
        
        [CacheAspect(duration: 10)] //key, value
        [PerformanceAspect(5)]
        public IDataResult<List<Product>> GetAll()//DataResult döndürecek RestFull api de
        {
            //İş kodları
            //Yetkisi var mı ?

            if (DateTime.Now.Hour == 2)
            {
                return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            //Parametre olarak expression alıyor
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.CategoryId==id));
        }

        [CacheAspect(duration: 10)] //key, value
        [PerformanceAspect(5)]
        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p=>p.ProductId == productId));
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p=>p.UnitPrice>=min && p.UnitPrice<=max));
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 23)//test ederken hata mesajı göntermesi için 
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {

            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {                
                _productDal.Update(product);

                return new SuccessResult(Messages.ProductUpdated);                             
            }
            return new ErrorResult();
        }

        //Bir kategoride 10 dan fazla ürün olamaz
        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //arka planda çalışan kod //Bu örnekte Link kodu oluşur ve çalışır
            //select count(*) from products where categoryId = 1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        //Ayı isimde ürün eklenemez
        private IResult CheckIfSameProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        //Eğer mevcur kategori sayısı 15 i geçti ise sisteme yeni ütün eklenemez
        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count >= 15)
            {
                return new ErrorResult(Messages.CaregoryLimitExcededError);
            }
            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult TransactionalOperation(Product product)
        {            
            _productDal.Add(product);//test
            _productDal.Update(product);//test
            return new SuccessResult(Messages.ProductUpdated);
        }

        //public IDataResult<List<Product>> GetByCategoryId(int categoryId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
