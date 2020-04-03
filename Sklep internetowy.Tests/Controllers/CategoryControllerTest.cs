using System.Web.Mvc;
using Moq;
using Sklep_internetowy.Controllers;
using NUnit.Framework;
using Sklep_internetowy.DAL;

namespace Sklep_internetowy.Tests.Controllers
{
    class CategoryControllerTest
    {
        private Mock<IProductDbContext> moq1;
        private Mock<IContextServices> moq2;
        CategoryController controller;

        [SetUp]
        public void SetUp()
        {
            moq1 = new Mock<IProductDbContext>();
            moq2 = new Mock<IContextServices>();
            controller = new CategoryController(moq1.Object, moq2.Object);
        }

        [Test]
        [TestCase(1)]
        public void CategoryContent_Should_CallGetProductsForGivenCategoryOnlyOnce(int id)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.CategoryContent(id);

            //Assert
            moq2.Verify(v => v.GetProductsForGivenCategory(It.IsAny<int>()), Times.Once);
        }

        [Test]
        [TestCase(1)]
        public void CategoryContent_Should_ReturnViewResult(int id)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.CategoryContent(id);

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
            moq2.Verify(v => v.GetProductsForGivenMainCategoryWithFilter(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
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
            moq2.Verify(v => v.GetProductsWhichAreBestsellers(), Times.Once);

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
        [TestCase(1)]
        public void CategoriesMenu_Should_CallGetCategoriesForGivenMainCategoryOnlyOnce(int idCategory)
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.CategoriesMenu(idCategory);

            //Assert
            moq2.Verify(v => v.GetCategoriesForGivenMainCategory(It.IsAny<int>()), Times.Once);
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

        [Test]
        public void MainCategoriesMenu_Should_CallGetAllMainCategories()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.MainCategoriesMenu();

            //Assert
            moq2.Verify(v => v.GetAllMainCategories(), Times.Once);
        }

        [Test]
        public void MainCategoriesMenu_Should_ReturnPartialViewResult()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.MainCategoriesMenu();

            //Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<PartialViewResult>(result);
        }





    }
}
