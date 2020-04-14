using System.Web.Mvc;
using Moq;
using Sklep_internetowy.Controllers;
using NUnit.Framework;
using Sklep_internetowy.DAL;
using AutoMoq;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.DAL.Interfaces;

namespace Sklep_internetowy.Tests.Controllers
{
    class ProductControllerTest
    {
        [Test]
        [TestCase(1)]
        public void ProductDetails_Should_CallGetAllDetailsForGivenProductIdOnlyOnce(int id)
        {
            //Arrange
            var moq1 = new Mock<IProductDbContext>();
            var moq2 = new Mock<IContextServices>();
            var controller = new ProductController(moq1.Object, moq2.Object);

            //Act
            var result = controller.ProductDetails(id);

            //Assert
            moq2.Verify(v => v.GetAllDetailsForGivenProductId(It.IsAny<int>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public void ProductDetails_Should_ReturnViewResult(int id)
        {
            //Arrange
            var moq1 = new Mock<IProductDbContext>();
            var moq2 = new Mock<IContextServices>();
            var controller = new ProductController(moq1.Object, moq2.Object);

            //Act
            var result = controller.ProductDetails(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void MainCategoriesMenu_Should_CallGetAllMainCategoriesOnlyOnce()
        {
            //Arrange
            var automoq = new AutoMoqer();
            var controller = automoq.Create<ProductController>();
            var moq1 = automoq.GetMock<IContextServices>();

            //Act
            var result = controller.MainCategoriesMenu();

            //Assert
            moq1.Verify(v => v.GetAllMainCategories(), Times.Once);
        }

        [Test]
        public void MainCategoriesMenu_Should_ReturnPartialViewResult()
        {
            //Arrange
            var automoq = new AutoMoqer();
            var controller = automoq.Create<ProductController>();

            //Act
            var result = controller.MainCategoriesMenu();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PartialViewResult>(result);
        }


    }
}
