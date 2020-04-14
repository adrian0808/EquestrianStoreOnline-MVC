using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Infrastructure;
using Sklep_internetowy.Models;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace Sklep_internetowy.DAL
{
    public class ContextServices : IContextServices
    {
        private IProductDbContext db;
        private ICacheProvider cache;
        private IClock clock;

        public ContextServices(IProductDbContext _db, ICacheProvider _cache, IClock _cl)
        {
            db = _db;
            cache = _cache;
            clock = _cl;
        }

        public ContextServices()
        {
            db = new ProductDbContext();
            cache = new DefaultCacheProvider();
            clock = new SystemClock();
        }

        public List<Product> GetProductsForGivenCategory(int idCategory)
        {
            return db.Products.Where(p => p.CategoryId == idCategory).ToList();
        }

        public HomeViewModel GetBestsellersAndNewsForMainPage()
        {      
            List<Product> news = new List<Product>();

            if (cache.isSet(Consts.NewsCacheKey))
            {
                news = cache.Get(Consts.NewsCacheKey) as List<Product>;
            }
            else
            {
                news = db.Products.OrderByDescending(p => p.AddingDate).Take(3).ToList();
                cache.Set(Consts.NewsCacheKey, news, 1);
            }

            List<Product> bestsellers = new List<Product>();

            if (cache.isSet(Consts.BestsellerCacheKey))
            {
                bestsellers = cache.Get(Consts.BestsellerCacheKey) as List<Product>;
            }
            else
            {
                bestsellers = db.Products.Where(p => p.isBestseller).OrderBy(p => Guid.NewGuid()).Take(3).ToList();
                cache.Set(Consts.BestsellerCacheKey, bestsellers, 60);
            }

            return new HomeViewModel() { Bestsellers = bestsellers, New = news };
        }

        public List<MainCategory> GetAllMainCategories()
        {
            List<MainCategory> MainCategories = new List<MainCategory>();

            if (cache.isSet(Consts.MainCategoriesCacheKey))
            {
                MainCategories = cache.Get(Consts.MainCategoriesCacheKey) as List<MainCategory>;
            }
            else
            {
                MainCategories = db.MainCategories.ToList();
                cache.Set(Consts.MainCategoriesCacheKey, MainCategories, 60);
            }

            return MainCategories;
        }

        public List<Product> GetProductsForGivenMainCategoryWithFilter(int idMainCategory, string searchTerm)
        {
            List<Product> Products = new List<Product>();
            
            if (!String.IsNullOrEmpty(searchTerm))
            {
                Products = db.Products.Where(p => p.Name.ToLower().Contains(searchTerm.ToLower()) || p.Brand.brand.ToLower().Contains(searchTerm.ToLower()) && p.Category.MainCategoryId == idMainCategory).ToList();
                return Products;
            }
            else
            {
                Products = db.Products.Where(p => p.Category.MainCategoryId == idMainCategory).ToList(); 
                return Products;
            }   
        }

        public List<Product> GetProductsForGivenCategoryWithFilter(int idCategory, string searchTerm)
        {
            List<Product> Products = new List<Product>();
            
            if(!String.IsNullOrEmpty(searchTerm))
            {
                Products = db.Products.Where(p => p.CategoryId == idCategory && (p.Name.ToLower().Contains(searchTerm.ToLower()) || p.Brand.brand.ToLower().Contains(searchTerm.ToLower()))).ToList();
                return Products;
            }
            else
            {
                Products = db.Products.Where(p => p.CategoryId == idCategory).ToList();
                return Products;
            }

        }

        public List<Product> GetProductsWhichAreBestsellers()
        {
            return db.Products.Where(p => p.isBestseller == true).ToList();
        }

        public List<Product> GetProductsWhichAreNew()
        {
          
            return db.Products.Where(p => (clock.Now.Month - p.AddingDate.Month) == 0 || (p.AddingDate.Day > clock.Now.Day && (clock.Now.Month - p.AddingDate.Month) == 1)).ToList();
        }

        public List<Category> GetCategoriesForGivenMainCategory(int idMainCategory)
        {  
            return db.Categories.Where(c => c.MainCategory.MainCategoryId == idMainCategory).ToList();
        }

        public ProductViewModel GetAllDetailsForGivenProductId(int id)
        {
            var product = db.Products.Where(p => p.ProductId == id).Single();
            var productVariants = db.ProductsVariant.Where(p => p.ProductId == id).ToList();      
            return new ProductViewModel() { Product = product, productVariants = productVariants};
        }

       
    }

    
}