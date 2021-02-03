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
            ProductManager productManager = new ProductManager( new EFProductDal());
            //ProductManager productManager = new ProductManager(new InMemoryDal());

            //foreach (var product in productManager.GetAll())
            //{
            //    Console.WriteLine(product.ProductName);
            //}

            foreach (var product in productManager.GetAllByCategoryId(2))
            {
                Console.WriteLine(product.ProductName);
            }

            //foreach (var product in productManager.GetByUnitPrice(40,100))
            //{
            //    Console.WriteLine(product.ProductName);
            //}
        }
    }
}
