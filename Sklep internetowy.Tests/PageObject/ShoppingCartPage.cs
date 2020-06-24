using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.PageObject
{
    public class ShoppingCartPage
    {
        public IWebDriver Driver { get; set; }

        [Obsolete]
        public ShoppingCartPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this, new DefaultElementLocator(driver));
        }

        [FindsBy(How = How.Id, Using = "payOrder")]
        public IWebElement PayOrderButton { get; set; }

        public void NavigateToOrderData()
        {
            this.PayOrderButton.Click();
        }





    }
}
