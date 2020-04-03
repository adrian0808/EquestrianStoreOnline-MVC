using MvcSiteMapProvider;
using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Infrastructure
{
    public class MainCategoryDynamicNodeProvider : DynamicNodeProviderBase
    {
        private ProductDbContext db = new ProductDbContext();
        public override IEnumerable<DynamicNode> GetDynamicNodeCollection(ISiteMapNode n)
        {
            var returnValue = new List<DynamicNode>();

            foreach (MainCategory i in db.MainCategories)
            {
                DynamicNode node = new DynamicNode();
                node.Title = i.Name;
                node.Key = "MainCategory_" + i.MainCategoryId;
                node.RouteValues.Add("idMainCategory", i.MainCategoryId);
                returnValue.Add(node);
            }
            return returnValue;
        }
    }
}
