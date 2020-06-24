using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.PageObject
{
    public class RegisterPage
    {
        public IWebDriver Driver { get; set; }

        [FindsBy(How = How.Id, Using = "Email")]
        public IWebElement EmailText { get; set; }

        [FindsBy(How = How.Id, Using = "Password")]
        public IWebElement PasswordText { get; set; }

        [FindsBy(How = How.Id, Using = "ConfirmPassword")]
        public IWebElement ConfirmPasswordText { get; set; }

        [FindsBy(How = How.Id, Using = "registerButton")]
        public IWebElement RegisterButton { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div/div[2]/div/div[2]/form/div[1]/ul/li")]
        public IWebElement ValidText { get; set; }
        
        public RegisterPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this, new DefaultElementLocator(driver));
        }

        public void RegisterOnPage(string email, string password, string confirmPassword)
        {
            this.EmailText.SendKeys(email);
            this.PasswordText.SendKeys(password);
            this.ConfirmPasswordText.SendKeys(confirmPassword);
            this.RegisterButton.Click();
        }

        public bool IsValidText()
        {
            return this.ValidText.Displayed;
        }
    }
}
