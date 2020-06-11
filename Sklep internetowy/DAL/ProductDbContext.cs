namespace Sklep_internetowy
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Sklep_internetowy.DAL.Interfaces;
    using Sklep_internetowy.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;
    using System.Linq;


    public class ProductDbContext : IdentityDbContext<ApplicationUser>, IProductDbContext
    {
      
        public ProductDbContext()
            : base("name=ProductDbContext")
        { }

        static ProductDbContext()
        {
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<ProductDbContext, Sklep_internetowy.Migrations.Configuration>());
        }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Usuniecie konwencji dodawania 's' jako liczby mnogiej dla nazwy kazdej tabeli
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

       
        public static ProductDbContext Create()
        {
            return new ProductDbContext();
        }

        public void SaveChangesWrapped()
        {
            base.SaveChanges();
        }

        public IDbSet<Product> Products { get; set; }
        public IDbSet<Category> Categories { get; set; }
        public IDbSet<MainCategory> MainCategories { get; set; }
        public IDbSet<Stock> Stocks { get; set; }
        public IDbSet<Color> Colors { get; set; }
        public IDbSet<Size> Sizes { get; set; }
        public IDbSet<Brand> Brands { get; set; }
        public IDbSet<Gender> Genders { get; set; }
        public IDbSet<ProductVariant> ProductsVariant { get; set; }
        public IDbSet<Order> Orders { get; set; }
        public IDbSet<OrderPosition> OrderPositions { get; set; }
    }

    

}