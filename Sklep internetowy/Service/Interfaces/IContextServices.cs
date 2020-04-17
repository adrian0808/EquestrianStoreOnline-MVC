using Sklep_internetowy.Models;
using Sklep_internetowy.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sklep_internetowy.Service.Interfaces
{
    public interface IContextServices
    {
        List<Product> GetProductsForGivenCategory(int idCategory);
        HomeViewModel GetBestsellersAndNewsForMainPage();
        List<MainCategory> GetAllMainCategories();
        List<Category> GetCategoriesForGivenMainCategory(int idMainCategory);
        List<Product> GetProductsForGivenMainCategoryWithFilter(int idMainCategory, string searchTerm);
        List<Product> GetProductsForGivenCategoryWithFilter(int idCategory, string searchTerm);
        List<Product> GetProductsWhichAreBestsellers();
        List<Product> GetProductsWhichAreNew();
        ProductViewModel GetAllDetailsForGivenProductId(int id);
        SelectList GetSizesForGivenProduct(int idProduct);
        SelectList GetColorsForGivenProduct(int idProduct);
        SelectList GetColorsForGivenSize(int sizeId, int productId);

    }
}
