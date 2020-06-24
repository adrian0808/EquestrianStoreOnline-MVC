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

namespace Sklep_internetowy.Tests.AutomaticTests.Register
{
    public class CheckRegisterFunction
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
        public void CheckRegister_WhenDataIsCorrect_CreateAccount()
        {
            Random rnd = new Random();

            driver.Navigate().GoToUrl(DataHelper.ServerUrl);

            var mainPage = new MainPage(driver);
            mainPage.NavigateToRegisterPage();

            var registerPage = new RegisterPage(driver);
            registerPage.RegisterOnPage("robert" + rnd.Next() +"@wp.pl", "3kd87K!sa", "3kd87K!sa");

            mainPage = new MainPage(driver);
            bool isLogoutButton = mainPage.IsLogoutButton();

            Assert.True(isLogoutButton == true);
        }

        [Test]
        public void CheckRegister_WhenConfirmPasswordIsIncorrect_DisplayTextAboutNotSuccess()
        {
            driver.Navigate().GoToUrl(DataHelper.ServerUrl);

            var mainPage = new MainPage(driver);
            mainPage.NavigateToRegisterPage();

            var registerPage = new RegisterPage(driver);
            registerPage.RegisterOnPage(DataHelper.Email, DataHelper.Password, "1234");
            bool isStatement = registerPage.IsValidText();
            
            Assert.True(isStatement == true);
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
