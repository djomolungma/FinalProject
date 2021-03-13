using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("getall")]
        //[Authorize(Roles = "Product.List")]
        public IActionResult GetAll()
        {
            //Hazır dökümantasyon sunan ürünlere örnek Swagger
            //Dependancy chain --
            
            var result = _categoryService.GetAll();
            if (result.Success)
            {
                return Ok(result);//Ok() Message:200 //Created() Message:201
            }
            else return BadRequest(result);//Badrequest() Message:400
        }
    }
}
