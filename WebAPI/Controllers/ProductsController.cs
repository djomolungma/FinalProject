using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]//https://localhost:44338/api/Products //domainIsmi/ali/controllerIsmi
    [ApiController] //Attribute (Javada annotation denmekte) //Bir class ile ilgili bize bilgi vermekte (bu örnekte bu class ın bir controller oldugunu söyler )
    public class ProductsController : ControllerBase
    {
        //Http isteklerinde yanıt 400 lü ise yetki yok yanıtı
        //Http isteklerinde yanıt 200 lü sıkıntı yok

        //SOLID prensiplerine uy
        //Soyut sınıflar üzerinden diğer katmanlara bağlanıyoruz, soyut sınıflar ile bağımlılık kurmamalıyız!
        //private global fieldleri alt çizgi ("_") ile başlat
        //naming converntion

        //IoC Container - Inversion of Control (program içerisinde bir kutunun içerisine bellekte new leyip kullandığımız yapılar)
        //Bana arka planda bir referans oluştur IoC bizim yerine new ler, bu yapıyı her yerde kullanabiliriz WebApi ve başka  yerler
        //Solution//WebApi/Startup/ConfigureServices...

        //Burada IoC kullanacağız 
        //Loosely coupled = gevşek bağımlılık        
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        //[Authorize(Roles = "Product.List")]
        public IActionResult GetAll()
        {
            //Hazır dökümantasyon sunan ürünlere örnek Swagger
            //Dependancy chain --
            //
            //Thread.Sleep(5000);
            var result = _productService.GetAll();
            if (result.Success)
            {
                return Ok(result);//Ok() Message:200 //Created() Message:201
            }
            else return BadRequest(result);//Badrequest() Message:400
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            else return BadRequest(result);
        }

        [HttpGet("getbycategory")]
        public IActionResult GetByCategory(int categoryId)
        {
            var result = _productService.GetAllByCategoryId(categoryId);
            if (result.Success)
            {
                return Ok(result);
            }
            else return BadRequest(result);
        }

        //silme ve güncellemeler için de HttpPost kullanılabilir ama istersen HttpDelete ve HttpPut kullanılabiliriz
        [HttpPost("add")]
        public IActionResult Add(Product product)
        {
            var result = _productService.Add(product);
            if (result.Success)
            {
                return Ok(result);
            }
            else return BadRequest(result.Message);
        }
    }
}
