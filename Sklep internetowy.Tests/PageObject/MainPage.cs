using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.PageObject
{
    public class MainPage
    {
        public IWebDriver Driver { get; set; }

        [FindsBy(How = How.Id, Using = "loginLink")]
        public IWebElement LoginButton { get; set; }

        [FindsBy(How = How.Id, Using = "registerLink")]
        public IWebElement RegisterButton { get; set; }

        [FindsBy(How = How.Id, Using = "logoutButton")]
        public IWebElement LogoutButton { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div/div[1]/div[1]/a[2]")]
        public IWebElement CategoryForHorseLink { get; set; }

        public MainPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this, new DefaultElementLocator(driver));
        }

        public void NavigateToLoginPage()
        {
            this.LoginButton.Click();
        }

        public void NavigateToRegisterPage()
        {
            this.RegisterButton.Click();
        }

        public void NavigateToHorseProduct()
        {
            this.CategoryForHorseLink.Click();
        }

        public bool IsLogoutButton()
        {
            return LogoutButton.Displayed;
        }

    }
}
