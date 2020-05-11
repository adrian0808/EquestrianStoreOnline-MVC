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
    public class ProductControllerTest
    {
       
        private Mock<IContextServices> service;
        ProductController controller;

        [SetUp]
        public void SetUp()
        {          
            service = new Mock<IContextServices>();
            controller = new ProductController(service.Object);
        }

        [Test]
        [TestCase(1)]
        public void ProductDetails_Should_CallGetAllDetailsForGivenProductIdOnlyOnce(int id)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.ProductDetails(id);

            //Assert
            service.Verify(v => v.GetAllDetailsForGivenProductId(It.IsAny<int>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public void ProductDetails_Should_CallGetSizesForGivenProductOnlyOnce(int id)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.ProductDetails(id);

            //Arrange
            service.Verify(v => v.GetSizesForGivenProduct(It.IsAny<int>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public void ProductDetails_Should_CallGetColorsForGivenProductOnlyOnce(int id)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.ProductDetails(id);

            //Arrange
            service.Verify(v => v.GetColorsForGivenProduct(It.IsAny<int>()), Times.Once);
        }
 
        [Test]
        [TestCase(1)]
        public void ProductDetails_Should_ReturnViewResult(int id)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.ProductDetails(id);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        [TestCase(1, 1)]
        public void GetColorForSelectList_Should_CallGetColorsForGivenSizeOnlyOnce(int sizeId, int productId)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.GetColorForSelectList(sizeId, productId);

            //Assert
            service.Verify(v => v.GetColorsForGivenSize(It.IsAny<int>(), It.IsAny<int>()), Times.Once);
        }

        [Test]
        [TestCase(1, 1)]
        public void GetColorForSelectList_Should_ReturnJsonResult(int sizeId, int productId)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.GetColorForSelectList(sizeId, productId);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<JsonResult>(result);
        }
     
    }
}
