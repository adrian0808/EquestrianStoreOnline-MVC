using Moq;
using NUnit.Framework;
using Sklep_internetowy.Controllers;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Infrastructure;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Sklep_internetowy.Tests.Controllers
{
    public class ShoppingCartControllerTest
    {
        private Mock<IProductDbContext> db;
        private Mock<IContextServices> service;
        private Mock<ISessionManager> session;
        private Mock<IShoppingCartServices> shoppingService;
        private ShoppingCartController controller;

        [SetUp]
        public void SetUp()
        {
            db = new Mock<IProductDbContext>();
            service = new Mock<IContextServices>();
            session = new Mock<ISessionManager>();
            shoppingService = new Mock<IShoppingCartServices>();
            controller = new ShoppingCartController(db.Object, session.Object, shoppingService.Object, service.Object);
        }
        [Test]
        public void Index_Should_CallGetShoppingCartOnlyOnce()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.Index();

            //Assert
            shoppingService.Verify(v => v.GetShoppingCart(), Times.Once);
        }

        [Test]
        public void Index_Should_CallGetValueOfShoppingCartOnlyOnce()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.Index();

            //Assert
            shoppingService.Verify(v => v.GetValueOfShoppingCart(), Times.Once);
        }

        [Test]
        public void Index_Should_ReturnViewResult()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        [TestCase(1,1,1)]
        public void AddToShoppingCart_Should_CallGetIdAddedProductVariantToShoppingCartOnlyOnce(int sizeId, int colorId, int productId)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.AddToShoppingCart(sizeId, colorId, productId);

            //Assert
            shoppingService.Verify(v => v.GetIdAddedProductVariantToShoppingCart(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }


    }
}
