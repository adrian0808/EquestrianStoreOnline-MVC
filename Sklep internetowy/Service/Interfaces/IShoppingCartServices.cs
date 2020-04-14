using Sklep_internetowy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Service.Interfaces
{
    public interface IShoppingCartServices
    {
        List<ShoppingCartPosition> GetShoppingCart();
        void Add(int productVariantId);
        int Remove(int productVariantId);
        decimal GetValueOfShoppingCart();
        int GetCountOfShoppingCartPositions();
        void ClearShoppingCart();
        Order CreateOrder(Order newOrder, string userId);

    }
}
