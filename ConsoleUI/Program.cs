using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using System;

namespace ConsoleUI
{
    //SOLID
    //Open Closed principle (yaptığın yazılıma yeni bir özellik ekliyor isen mevcuttaki hiç bir koda dokunmamalısın !!!)
    class Program
    {
        static void Main(string[] args)
        {
            //DTO = Data Transformation Object
            //IoC

            ProductTest();
            //CategoryTest();


        }

        private static void CategoryTest()
        {
            //IoC
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EFProductDal());
            //ProductManager productManager = new ProductManager(new InMemoryDal());

            //foreach (var product in productManager.GetAll())
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //foreach (var product in productManager.GetAllByCategoryId(2))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            //foreach (var product in productManager.GetByUnitPrice(40,100))
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            foreach (var product in productManager.GetProductDetails())
            {
                Console.WriteLine(product.ProductName + "/" + product.CategoryName);
            }
        }
    }
}
