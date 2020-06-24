using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.PageObject
{
    public class LoginPage
    {
        public IWebDriver Driver { get; set; }

        [FindsBy(How = How.Id, Using = "loginButton")]
        public IWebElement LoginButton { get; set; }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement EmailText { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement PasswordText { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div/div[2]/div/div[2]/form/div[1]/ul/li")]
        public IWebElement ValidText { get; set; }

        public LoginPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this, new DefaultElementLocator(driver));
        }

        public void LoginOnPage(string email, string password)
        {
            this.EmailText.SendKeys(email);
            this.PasswordText.SendKeys(password);
            this.LoginButton.Click();
        }

        public bool IsValidText()
        {
            return this.ValidText.Displayed;
        }

        public bool IsLoginButton()
        {
            return this.LoginButton.Displayed;
        }
    }
}
