using MvcSiteMapProvider;
using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Infrastructure
{
    public class CategoryDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ProductDbContext db = new ProductDbContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode n)
        {
            var returnValue = new List<DynamicNode>();

            foreach (Category i in db.Categories)
            {
                DynamicNode node = new DynamicNode();
                node.Title = i.Name;
                node.Key = "Category_" + i.CategoryId;
                node.ParentKey = "MainCategory_" + i.MainCategoryId;
                node.RouteValues.Add("idCategory", i.CategoryId);
                node.RouteValues.Add("idMainCategory", i.MainCategoryId);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}