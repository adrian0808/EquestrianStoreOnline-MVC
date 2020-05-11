using System.Web.Mvc;
using Moq;
using Sklep_internetowy.Controllers;
using NUnit.Framework;
using Sklep_internetowy.DAL;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Service.Interfaces;
using Sklep_internetowy.Infrastructure;

namespace Sklep_internetowy.Tests.Controllers
{
    public class CategoryControllerTest
    {
       
        private Mock<IContextServices> contextService;     
        CategoryController controller;

        [SetUp]
        public void SetUp()
        {
            contextService = new Mock<IContextServices>();
            controller = new CategoryController(contextService.Object);
        }

        [Test]
        [TestCase(1, "abc")]
        public void CategoryContent_Should_CallGetProductsForGivenCategoryWithFilterOnlyOnce(int id, string searchTerm)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.CategoryContent(id, searchTerm);

            //Assert
            contextService.Verify(v => v.GetProductsForGivenCategoryWithFilter(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        [TestCase(1, "abc")]
        public void CategoryContent_Should_ReturnViewResult(int id, string searchTerm)
        {
            //Arrange
            //SetUp
            
            //Act
            var result = controller.CategoryContent(id, searchTerm);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        [TestCase(1, "abc")]
        public void MainCategoryContent_Should_CallGetProductsForGivenMainCategoryWithFilterOnlyOnce(int idMainCategory, string searchTerm)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.MainCategoryContent(idMainCategory, searchTerm);

            //Assert
            contextService.Verify(v => v.GetProductsForGivenMainCategoryWithFilter(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [Test]
        [TestCase(1, "abc")]
        public void MainCategoryContent_Should_ReturnViewResult(int idMainCategory, string searchTerm)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.MainCategoryContent(idMainCategory, searchTerm);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void BestsellerContent_Should_CallGetProductsWhichAreBestsellersOnlyOnce()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.BestsellerContent();

            //Assert
            contextService.Verify(v => v.GetProductsWhichAreBestsellers(), Times.Once);

        }

        
        [Test]
        public void BestsellerContent_Should_ReturnViewResult()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.BestsellerContent();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void NewsContent_Should_CallGetProductsWhichAreNewOnlyOnce()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.NewsContent();

            //Assert
            contextService.Verify(v => v.GetProductsWhichAreNew(), Times.Once);
        }

        [Test]
        public void NewsContent_Should_ReturnViewResult()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.NewsContent();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }


        [Test]
        [TestCase(1)]
        public void CategoriesMenu_Should_CallGetCategoriesForGivenMainCategoryOnlyOnce(int idCategory)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.CategoriesMenu(idCategory);

            //Assert
            contextService.Verify(v => v.GetCategoriesForGivenMainCategory(It.IsAny<int>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public void CategoriesMenu_Should_ReturnPartialViewResult(int idMainCategory)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.CategoriesMenu(idMainCategory);

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PartialViewResult>(result);
        }

       

    }
}
