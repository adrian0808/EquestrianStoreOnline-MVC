using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Infrastructure;
using Sklep_internetowy.Models;
using Sklep_internetowy.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Service
{
    public class ShoppingCartServices : IShoppingCartServices
    {
        private IProductDbContext db;
        private ISessionManager session;
        private IClock clock;

        public ShoppingCartServices(IProductDbContext db, ISessionManager session, IClock clock)
        {
            this.db = db;
            this.session = session;
            this.clock = clock;
        }

       
        public List<ShoppingCartPosition> GetShoppingCart()
        {
            List<ShoppingCartPosition> shoppingCart;

            if (session.Get<List<ShoppingCartPosition>>(Consts.ShoppingCartSessionKey) == null)
            {
                shoppingCart = new List<ShoppingCartPosition>();
            }
            else
            {
                shoppingCart = session.Get<List<ShoppingCartPosition>>(Consts.ShoppingCartSessionKey);
            }

            return shoppingCart;
        }

        public void Add(int productVariantId)
        {
            List<ShoppingCartPosition> shoppingCart = GetShoppingCart();
            ProductVariant productVariant = db.ProductsVariant.Where(p => p.ProductVariantId == productVariantId).SingleOrDefault();

            if (productVariant != null)
            {
                ShoppingCartPosition shoppingCartPosition = shoppingCart.Find(s => s.ProductVariant.ProductVariantId == productVariantId);

                if (shoppingCartPosition != null)
                {
                    shoppingCartPosition.Quantity++;
                }
                else
                {
                    ShoppingCartPosition shoppingCartPositionNew = new ShoppingCartPosition() { ProductVariant = productVariant, Quantity = 1, Price = productVariant.Product.Price };
                    shoppingCart.Add(shoppingCartPositionNew);
                }
            }

            session.Set<List<ShoppingCartPosition>>(Consts.ShoppingCartSessionKey, shoppingCart);
        }


        public int Remove(int productVariantId)
        {
            List<ShoppingCartPosition> shoppingCart = GetShoppingCart();

            if (shoppingCart.Count != 0)
            {
                ShoppingCartPosition shoppingCartPosition = shoppingCart.Find(s => s.ProductVariant.ProductVariantId == productVariantId);

                if (shoppingCartPosition != null)
                {
                    if (shoppingCartPosition.Quantity > 1)
                    {
                        shoppingCartPosition.Quantity--;
                        return shoppingCartPosition.Quantity;
                    }
                    else
                    {
                        shoppingCart.Remove(shoppingCartPosition);
                    }
                }
            }

            return 0;
        }

        public decimal GetValueOfShoppingCart()
        {
            List<ShoppingCartPosition> shoppingCart = GetShoppingCart();
            return shoppingCart.Sum(s => s.Price);
        }


        public int GetCountOfShoppingCartPositions()
        {
            List<ShoppingCartPosition> shoppingCarts = GetShoppingCart();
            return shoppingCarts.Sum(s => s.Quantity);
        }

        public int GetIdAddedProductVariantToShoppingCart(int sizeId, int colorId, int productId)
        {
            return db.ProductsVariant.Where(p => p.SizeId == sizeId && p.ColorId == colorId && p.ProductId == productId).Select(i => i.ProductVariantId).SingleOrDefault();
        }

        public void ClearShoppingCart()
        {
            session.Set<List<ShoppingCartPosition>>(Consts.ShoppingCartSessionKey, null);
        }

        public Order CreateOrder(Order newOrder, string userId)
        {
            List<ShoppingCartPosition> shoppingCarts = GetShoppingCart();
            newOrder.AddingDate = clock.Now;
            newOrder.UserId = userId;

            db.Orders.Add(newOrder);

            if (newOrder.OrderPositions == null)
            {
                newOrder.OrderPositions = new List<OrderPosition>();
            }

            foreach (var shoppingCartElement in shoppingCarts)
            {
                OrderPosition newOrderPosition = new OrderPosition()
                {
                    ProductVariantId = shoppingCartElement.ProductVariant.ProductVariantId,
                    Quantity = shoppingCartElement.Quantity,
                    Price = shoppingCartElement.Price
                };

                newOrder.OrderPositions.Add(newOrderPosition);
            }

            newOrder.Price = GetValueOfShoppingCart();
            db.SaveChangesWrapped();
            return newOrder;
        }


    }
}