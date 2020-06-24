using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Sklep_internetowy.Tests.PageObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.AutomaticTests.Login
{
    public class CheckLoginFunction
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
        public void CheckLogin_WhenDataIsCorrect_LoginToPage()
        {       
            driver.Navigate().GoToUrl(DataHelper.ServerUrl);
            
            var mainPage = new MainPage(driver);
            mainPage.NavigateToLoginPage();

            var loginPage = new LoginPage(driver);
            loginPage.LoginOnPage(DataHelper.Email, DataHelper.Password);

            mainPage = new MainPage(driver);
            bool isLogoutButton = mainPage.IsLogoutButton();

            Assert.True(isLogoutButton == true);
        }

        [Test]
        public void CheckLogin_WhenDataIsIncorrect_DisplayTextAboutInvalidLogin()
        {
            driver.Navigate().GoToUrl(DataHelper.ServerUrl);

            var mainPage = new MainPage(driver);
            mainPage.NavigateToLoginPage();

            var loginPage = new LoginPage(driver);
            loginPage.LoginOnPage(DataHelper.IncorrectEmail, DataHelper.IncorrectPassword);

            bool isStatement = loginPage.IsValidText();

            Assert.True(isStatement == true);
        }

        [TearDown]
        public void CloseAll()
        {
            driver.Quit();
            driver.Dispose();
            driver = null;
        }


    }
}
