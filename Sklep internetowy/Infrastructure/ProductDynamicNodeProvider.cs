using MvcSiteMapProvider;
using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Infrastructure
{
    public class ProductDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ProductDbContext db = new ProductDbContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode n)
        {
            var returnValue = new List<DynamicNode>();

            foreach(Product i in db.Products)
            {
                DynamicNode node = new DynamicNode();
                node.Title = i.Name;
                node.Key = "Product_" + i.ProductId;
                node.ParentKey = "Category_" + i.CategoryId;
                node.RouteValues.Add("id", i.ProductId);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}