﻿using System.Web.Mvc;
using Moq;
using Sklep_internetowy.Controllers;
using NUnit.Framework;
using Sklep_internetowy.DAL;
using Sklep_internetowy.Models;
using System.Collections.Generic;
using Sklep_internetowy.DAL.Interfaces;
using Sklep_internetowy.Service.Interfaces;

namespace Sklep_internetowy.Tests.Controllers
{
    public class HomeControllerTest
    {
        private Mock<IProductDbContext> db;
        private Mock<IContextServices> contextService;
        HomeController controller;

        [SetUp]
        public void SetUp()
        {
            db = new Mock<IProductDbContext>();
            contextService = new Mock<IContextServices>();
            controller = new HomeController(db.Object, contextService.Object);
        }
        
        [Test]
        public void Index_Should_GetBestsellersAndNewsForMainPageCallOnlyOnce()
        {
            //Arrange
            //SetUp

            //Act
            var result = controller.Index();

            //Assert
            contextService.Verify(v => v.GetBestsellersAndNewsForMainPage(), Times.Once);
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
        [TestCase("abc")]
        public void StaticPages_Should_ReturnViewResult(string name)
        {
            //Arrange
            //SetUp

            // Act
            var result = controller.StaticPages(name);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<ViewResult>(result);
        }

    
    }
}
