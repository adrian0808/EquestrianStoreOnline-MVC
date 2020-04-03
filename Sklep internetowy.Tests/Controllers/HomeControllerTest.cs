using System.Web.Mvc;
using Moq;
using Sklep_internetowy.Controllers;
using NUnit.Framework;
using Sklep_internetowy.DAL;
using Sklep_internetowy.Models;
using System.Collections.Generic;

namespace Sklep_internetowy.Tests.Controllers
{
    public class HomeControllerTest
    {
        
        [Test]
        public void HomeIndexTest()
        {
            //Arrange
            var moq1 = new Mock<IProductDbContext>();
            var moq2 = new Mock<IContextServices>();

            var controller = new HomeController(moq1.Object, moq2.Object);

            //Act
            var result = controller.Index();

            //Assert
            Assert.IsInstanceOf<ViewResult>(result);
            moq2.Verify(v => v.GetBestsellersAndNewsForMainPage(), Times.Once);
        }

        [Test]
        public void About()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.StaticPages("AboutUs") as ViewResult;

            // Assert
            Assert.IsInstanceOf<ViewResult>(result);
        }

        [Test]
        public void Contact()
        {
            // Arrange
            HomeController controller = new HomeController();

            // Act
            ViewResult result = controller.StaticPages("Contact") as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
