using Moq;
using NUnit.Framework;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Infrastructure;
using Sklep_internetowy.Models;
using Sklep_internetowy.Service;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.Service
{
    public class ShoppingServiceTest
    {
        private Mock<IProductDbContext> db;
        private Mock<ISessionManager> session;
        private Mock<IClock> clock;

        [SetUp]
        public void SetUp()
        {
            db = new Mock<IProductDbContext>();
            session = new Mock<ISessionManager>();
            clock = new Mock<IClock>();
        }

        [Test]
        public void GetShoppingCart_Should_ReturnAllShoppingCartPositions()
        {
            //Assert
            var productVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = null, Size = null},
                new ProductVariant() { ProductVariantId = 2, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = null, Size = null},
            };

            var shoppingCart = new List<ShoppingCartPosition>()
            {
                new ShoppingCartPosition() { ProductVariant = productVariants.Where(p => p.ProductVariantId == 1).SingleOrDefault(), Quantity = 2, Price = 400M},
                new ShoppingCartPosition() { ProductVariant = productVariants.Where(p => p.ProductVariantId == 2).SingleOrDefault(), Quantity = 2, Price = 400M}
            };

            session.Setup(s => s.Get<List<ShoppingCartPosition>>(It.IsAny<string>())).Returns(shoppingCart);
            var shoppingService = new ShoppingCartServices(db.Object, session.Object, clock.Object);

            //Act
            var result = shoppingService.GetShoppingCart();

            //Assert
            Assert.AreEqual(2, result.Count());

        }

        [Test]
        [TestCase(3)]
        public void Add_WhenQuantityAddingProductIsZero_Should_AddProductVariantToShoppingCartWithNewShoppingCartPosition(int productVariantId)
        {
            var productVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = null, Size = null},
                new ProductVariant() { ProductVariantId = 2, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = null, Size = null},
            };

            var shoppingCart = new List<ShoppingCartPosition>()
            {
                new ShoppingCartPosition() { ProductVariant = productVariants.Where(p => p.ProductVariantId == 1).SingleOrDefault(), Quantity = 2, Price = 400M},
                new ShoppingCartPosition() { ProductVariant = productVariants.Where(p => p.ProductVariantId == 2).SingleOrDefault(), Quantity = 2, Price = 400M}
            };

            var listOfProducts = new List<Product>()
            { 
                new Product() { ProductId = 1, Price = 400M},
                new Product() { ProductId = 2, Price = 200M},
            };

            var listOfProductVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = listOfProducts.Where(p => p.ProductId == 1).SingleOrDefault(), Size = null},
                new ProductVariant() { ProductVariantId = 2, ColorId = 1, ProductId = 2, SizeId = 1, Color = null, Product = listOfProducts.Where(p => p.ProductId == 2).SingleOrDefault(), Size = null},
                new ProductVariant() { ProductVariantId = 3, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = listOfProducts.Where(p => p.ProductId == 1).SingleOrDefault(), Size = null},
                new ProductVariant() { ProductVariantId = 4, ColorId = 1, ProductId = 2, SizeId = 1, Color = null, Product = listOfProducts.Where(p => p.ProductId == 2).SingleOrDefault(), Size = null},
            }.AsQueryable();

            var mockSet = new Mock<IDbSet<ProductVariant>>();
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.Provider).Returns(listOfProductVariants.Provider);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.Expression).Returns(listOfProductVariants.Expression);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.ElementType).Returns(listOfProductVariants.ElementType);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.GetEnumerator()).Returns(listOfProductVariants.GetEnumerator());

          
            db.Setup(s => s.ProductsVariant).Returns(mockSet.Object);

            session.Setup(s => s.Get<List<ShoppingCartPosition>>(It.IsAny<string>())).Returns(shoppingCart);
            var shoppingService = new ShoppingCartServices(db.Object, session.Object, clock.Object);

            shoppingService.Add(productVariantId);

            Assert.AreEqual(3, shoppingCart.Count);
            Assert.AreEqual(5, shoppingCart.Sum(s => s.Quantity));

        }


        [Test]
        [TestCase(2)]
        public void Add_WhenQuantityAddingProductIsNotZero_Should_AddProductVariantToShoppingCartWithoutNewShoppingCartPosition(int productVariantId)
        {
            var productVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = null, Size = null},
                new ProductVariant() { ProductVariantId = 2, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = null, Size = null},
            };

            var shoppingCart = new List<ShoppingCartPosition>()
            {
                new ShoppingCartPosition() { ProductVariant = productVariants.Where(p => p.ProductVariantId == 1).SingleOrDefault(), Quantity = 2, Price = 400M},
                new ShoppingCartPosition() { ProductVariant = productVariants.Where(p => p.ProductVariantId == 2).SingleOrDefault(), Quantity = 2, Price = 400M}
            };

            var listOfProducts = new List<Product>()
            {
                new Product() { ProductId = 1, Price = 400M},
                new Product() { ProductId = 2, Price = 200M},
            };

            var listOfProductVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = listOfProducts.Where(p => p.ProductId == 1).SingleOrDefault(), Size = null},
                new ProductVariant() { ProductVariantId = 2, ColorId = 1, ProductId = 2, SizeId = 1, Color = null, Product = listOfProducts.Where(p => p.ProductId == 2).SingleOrDefault(), Size = null},
                new ProductVariant() { ProductVariantId = 3, ColorId = 1, ProductId = 1, SizeId = 1, Color = null, Product = listOfProducts.Where(p => p.ProductId == 1).SingleOrDefault(), Size = null},
                new ProductVariant() { ProductVariantId = 4, ColorId = 1, ProductId = 2, SizeId = 1, Color = null, Product = listOfProducts.Where(p => p.ProductId == 2).SingleOrDefault(), Size = null},
            }.AsQueryable();

            var mockSet = new Mock<IDbSet<ProductVariant>>();
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.Provider).Returns(listOfProductVariants.Provider);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.Expression).Returns(listOfProductVariants.Expression);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.ElementType).Returns(listOfProductVariants.ElementType);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.GetEnumerator()).Returns(listOfProductVariants.GetEnumerator());

        
            db.Setup(s => s.ProductsVariant).Returns(mockSet.Object);

            session.Setup(s => s.Get<List<ShoppingCartPosition>>(It.IsAny<string>())).Returns(shoppingCart);
            var shoppingService = new ShoppingCartServices(db.Object, session.Object, clock.Object);

            shoppingService.Add(productVariantId);

            Assert.AreEqual(2, shoppingCart.Count);
            Assert.AreEqual(5, shoppingCart.Sum(s => s.Quantity));

        }

        [Test]
        [TestCase(3)]
        public void Remove_WhenQuantityAfterRemovingProductIsNotZero_Should_RemoveProductVariantFromShoppingCartPosition(int productVariantId)
        {
            var listOfProductVariant = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1},
                new ProductVariant() { ProductVariantId = 2},
                new ProductVariant() { ProductVariantId = 3},
                new ProductVariant() { ProductVariantId = 4}
            };

            var listOfShoppingCartPositions = new List<ShoppingCartPosition>()
            {
                new ShoppingCartPosition() { ProductVariant = listOfProductVariant.Where(p => p.ProductVariantId == 1).SingleOrDefault(), Quantity = 2, Price = 300M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariant.Where(p => p.ProductVariantId == 2).SingleOrDefault(), Quantity = 1, Price = 500M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariant.Where(p => p.ProductVariantId == 3).SingleOrDefault(), Quantity = 2, Price = 200M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariant.Where(p => p.ProductVariantId == 4).SingleOrDefault(), Quantity = 1, Price = 400M},
            };

            session.Setup(s => s.Get<List<ShoppingCartPosition>>(It.IsAny<string>())).Returns(listOfShoppingCartPositions);

            var shoppingService = new ShoppingCartServices(db.Object, session.Object, clock.Object);

            var result = shoppingService.Remove(productVariantId);

            Assert.AreEqual(4, listOfShoppingCartPositions.Count);
            Assert.AreEqual(5, listOfShoppingCartPositions.Sum(s => s.Quantity));

        }

        [Test]
        [TestCase(2)]
        public void Remove_WhenQuantityAfterRemovingProductIsZero_Should_RemoveProductVariantAndShoppingCartPositionFromShoppingCart(int productVariantId)
        {
            var listOfProductVariant = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1},
                new ProductVariant() { ProductVariantId = 2},
                new ProductVariant() { ProductVariantId = 3},
                new ProductVariant() { ProductVariantId = 4}
            };

            var listOfShoppingCartPositions = new List<ShoppingCartPosition>()
            {
                new ShoppingCartPosition() { ProductVariant = listOfProductVariant.Where(p => p.ProductVariantId == 1).SingleOrDefault(), Quantity = 2, Price = 300M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariant.Where(p => p.ProductVariantId == 2).SingleOrDefault(), Quantity = 1, Price = 500M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariant.Where(p => p.ProductVariantId == 3).SingleOrDefault(), Quantity = 2, Price = 200M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariant.Where(p => p.ProductVariantId == 4).SingleOrDefault(), Quantity = 1, Price = 400M},
            };

            session.Setup(s => s.Get<List<ShoppingCartPosition>>(It.IsAny<string>())).Returns(listOfShoppingCartPositions);

            var shoppingService = new ShoppingCartServices(db.Object, session.Object, clock.Object);

            var result = shoppingService.Remove(productVariantId);

            Assert.AreEqual(3, listOfShoppingCartPositions.Count);
            Assert.AreEqual(5, listOfShoppingCartPositions.Sum(s => s.Quantity));

        }

        [Test]
        public void GetValueOfShoppingCart_Should_ReturnValueOfShoppingCart()
        {
            //Arrange
            var listOfProducts = new List<Product>()
            { 
               new Product() { ProductId = 1, Price = 50M},
               new Product() { ProductId = 2, Price = 100M},
            };

            var listOfProductVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, Product = listOfProducts.Where(p => p.ProductId == 1).FirstOrDefault() },
                new ProductVariant() { ProductVariantId = 2, Product = listOfProducts.Where(p => p.ProductId == 2).FirstOrDefault() },
            };

            var listOfShoppingCartPositions = new List<ShoppingCartPosition>()
            {
                new ShoppingCartPosition() { ProductVariant = listOfProductVariants.Where(p => p.ProductVariantId == 1).SingleOrDefault(), Quantity = 3, Price = 150M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariants.Where(p => p.ProductVariantId == 2).SingleOrDefault(), Quantity = 2, Price = 200M}
            };

            session.Setup(s => s.Get<List<ShoppingCartPosition>>(It.IsAny<string>())).Returns(listOfShoppingCartPositions);

            var shoppingCartService = new ShoppingCartServices(db.Object, session.Object, clock.Object);

            //Act
            var result = shoppingCartService.GetValueOfShoppingCart();

            //Assert
            Assert.AreEqual(350, result);
        }

  
        [Test]
        public void GetCountOfShoppingCartPositions_Should_ReturnAllQuantitiesFromShoppingCart()
        {
            var listOfProductVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1},
                new ProductVariant() { ProductVariantId = 2},
                new ProductVariant() { ProductVariantId = 3},
            };

            var shoppingCartPositions = new List<ShoppingCartPosition>()
            { 
                new ShoppingCartPosition() { ProductVariant = listOfProductVariants.Where(p => p.ProductVariantId == 1).FirstOrDefault(), Quantity = 3, Price = 20M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariants.Where(p => p.ProductVariantId == 2).FirstOrDefault(), Quantity = 2, Price = 60M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariants.Where(p => p.ProductVariantId == 3).FirstOrDefault(), Quantity = 4, Price = 80M},
            };

            session.Setup(s => s.Get<List<ShoppingCartPosition>>(It.IsAny<string>())).Returns(shoppingCartPositions);

            var shoppingCartService = new ShoppingCartServices(db.Object, session.Object, clock.Object);

            var result = shoppingCartService.GetCountOfShoppingCartPositions();

            Assert.AreEqual(9, result);
        }
      

        [Test]
        [TestCase(1, 1, 10)]
        public void GetIdAddedProductVariantToShoppingCart_Should_ReturnProductVariantId(int sizeId, int colorId, int productId)
        {
            var listOfProductVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, SizeId = 1, ColorId = 1, ProductId = 10},
                new ProductVariant() { ProductVariantId = 2, SizeId = 5, ColorId = 3, ProductId = 8},
                new ProductVariant() { ProductVariantId = 3, SizeId = 4, ColorId = 4, ProductId = 9},
                new ProductVariant() { ProductVariantId = 4, SizeId = 2, ColorId = 2, ProductId = 11},
                new ProductVariant() { ProductVariantId = 5, SizeId = 3, ColorId = 1, ProductId = 13},
            }.AsQueryable();

            var mockSet = new Mock<IDbSet<ProductVariant>>();
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.Provider).Returns(listOfProductVariants.Provider);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.Expression).Returns(listOfProductVariants.Expression);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.ElementType).Returns(listOfProductVariants.ElementType);
            mockSet.As<IQueryable<ProductVariant>>().Setup(m => m.GetEnumerator()).Returns(listOfProductVariants.GetEnumerator());

            db.Setup(s => s.ProductsVariant).Returns(mockSet.Object);

            var shoppingCartService = new ShoppingCartServices(db.Object, session.Object, clock.Object);
            var result = shoppingCartService.GetIdAddedProductVariantToShoppingCart(sizeId, colorId, productId);

            Assert.AreEqual(1, result);

        }


        [Test]
        [TestCase("1")]
        public void CreateOrder_Should_ReturnOrder(string userId)
        {

            var listOfOrders = new List<Order>()
            {
                new Order() { OrderId = 1},
                new Order() { OrderId = 2},
                new Order() { OrderId = 3},
            }.AsQueryable();

            var listOfProducts = new List<Product>()
            { 
                new Product() { ProductId = 1, Price = 30M},
                new Product() { ProductId = 2, Price = 50M},
                new Product() { ProductId = 3, Price = 80M},
            };

            var listOfProductVariants = new List<ProductVariant>()
            {
                new ProductVariant() { ProductVariantId = 1, Product = listOfProducts.Where(p => p.ProductId == 1).FirstOrDefault() },
                new ProductVariant() { ProductVariantId = 2, Product = listOfProducts.Where(p => p.ProductId == 2).FirstOrDefault() },
            };

            var listOfShoppingCartPostions = new List<ShoppingCartPosition>()
            {
                new ShoppingCartPosition() { ProductVariant = listOfProductVariants.Where(p => p.ProductVariantId == 1).FirstOrDefault(), Quantity = 3, Price = 90M},
                new ShoppingCartPosition() { ProductVariant = listOfProductVariants.Where(p => p.ProductVariantId == 2).FirstOrDefault(), Quantity = 2, Price = 100M},
            };

            session.Setup(s => s.Get<List<ShoppingCartPosition>>(It.IsAny<String>())).Returns(listOfShoppingCartPostions);
            clock.Setup(s => s.Now).Returns(new DateTime(2020, 6, 12));

            var mockSet = new Mock<IDbSet<Order>>();
            mockSet.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(listOfOrders.Provider);
            mockSet.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(listOfOrders.Expression);
            mockSet.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(listOfOrders.ElementType);
            mockSet.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(listOfOrders.GetEnumerator());

            db.Setup(s => s.Orders).Returns(mockSet.Object);

            var serviceShoppingCart = new ShoppingCartServices(db.Object, session.Object, clock.Object);
            
            var result = serviceShoppingCart.CreateOrder(new Order(), userId);

            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Order>(result);
            Assert.AreEqual(190, result.Price);
            db.Verify(v => v.Orders.Add(It.IsAny<Order>()), Times.Once);
            db.Verify(v => v.SaveChangesWrapped(), Times.Once);
         
           
        }
    }
}
