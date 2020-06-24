using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.PageObject
{
    public class ConfirmOrderPage
    {
        public IWebDriver Driver { get; set; }

        public ConfirmOrderPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this, new DefaultElementLocator(driver));
        }

        [FindsBy(How = How.Id, Using = "confirmText")]
        public IWebElement ConfirmText { get; set; }

        [FindsBy(How = How.XPath, Using = "/html/body/nav/div/div/ul/form/ul/li[1]/a")]
        public IWebElement ManageAccountLink { get; set; }

        public bool IsConfirmText()
        {
            return this.ConfirmText.Displayed;
        }

        public void NavigateToManageAccount()
        {
            this.ManageAccountLink.Click();
        }








    }
}
