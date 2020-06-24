using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Sklep_internetowy.Tests.AutomaticTests.Login;
using Sklep_internetowy.Tests.PageObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.AutomaticTests
{
    public class CheckShoppingCartFunction
    {
        private IWebDriver driver;
        private ChromeOptions options;

        [SetUp]
        public void SetUp()
        {
            options = new ChromeOptions();
            options.AddArgument("--window-size=1300,1000");
            driver = new ChromeDriver(options);
        }

        [Test]
        [Obsolete]
        public void CheckPayForOrder_WhenUserIsNotLog_RedirectToLoginPage()
        {
            driver.Navigate().GoToUrl(DataHelper.ServerUrl);

            var mainPage = new MainPage(driver);
            mainPage.NavigateToHorseProduct();
            
            var mainCategoryPage = new MainCategoryPage(driver);
            mainCategoryPage.NavigateToPrestigeSaddleDetails();

            var productDetailsPage = new ProductDetailsPage(driver);
            productDetailsPage.ChooseOptions();
            productDetailsPage.AddElementToShoppingCartAndNavigateToIt();

            var shoppingCartPage = new ShoppingCartPage(driver);
            shoppingCartPage.NavigateToOrderData();

            var loginPage = new LoginPage(driver);
            bool isLoginButton = loginPage.IsLoginButton();

            Assert.True(isLoginButton == true);
        }

        [Test]
        [Obsolete]
        public void CheckPayForOrder_WhenUserIsLog_PlaceOrder()
        {
            driver.Navigate().GoToUrl(DataHelper.ServerUrl);

            var mainPage = new MainPage(driver);
            mainPage.NavigateToLoginPage();

            var loginPage = new LoginPage(driver);
            loginPage.LoginOnPage(DataHelper.Email, DataHelper.Password);

            mainPage = new MainPage(driver);
            mainPage.NavigateToHorseProduct();

            var mainCategoryPage = new MainCategoryPage(driver);
            mainCategoryPage.NavigateToPrestigeSaddleDetails();

            var productDetailsPage = new ProductDetailsPage(driver);
            productDetailsPage.ChooseOptions();
            productDetailsPage.AddElementToShoppingCartAndNavigateToIt();

            var shoppingCartPage = new ShoppingCartPage(driver);
            shoppingCartPage.NavigateToOrderData();

            var dataOrderPage = new DataOrderPage(driver);
            dataOrderPage.FillForm();
            dataOrderPage.NavigateToConfirmOrder();


        }

        [TearDown]
        public void CleanUp()
        {
            driver.Close();
            driver.Dispose();
            driver = null;
        }
        
    }
}
