using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    //Context : Db tabloları ile proje sınıflarını bağlamak
    public class NorthwindContext : DbContext
    {
        //Projenin hangi veritabanı ile ilişkili olduğu yer
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //@ ile ters slash ı normal ters slash ile algıla
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb; Database=Northwind; Trusted_Connection=true;");
        }

        //Entity ile veritabanındaki tabloları eşleştir
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }

    }
}
