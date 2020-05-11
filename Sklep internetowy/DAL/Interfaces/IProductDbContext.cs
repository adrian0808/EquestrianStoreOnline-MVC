using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.DAL.Interfaces
{
    public interface IProductDbContext
    {
        IDbSet<Product> Products { get; }
        IDbSet<Category> Categories { get; }
        IDbSet<MainCategory> MainCategories { get; }
        IDbSet<Stock> Stocks { get; }
        IDbSet<Color> Colors { get; }
        IDbSet<Size> Sizes { get; }
        IDbSet<Brand> Brands { get; }
        IDbSet<Gender> Genders { get; }
        IDbSet<ProductVariant> ProductsVariant { get; }
        IDbSet<Order> Orders { get; }
        IDbSet<OrderPosition> OrderPositions { get; }
        void SaveChangesWrapped();
    }

}
