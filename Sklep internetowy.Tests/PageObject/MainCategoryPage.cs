using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.PageObject
{
    public class MainCategoryPage
    {
        public IWebDriver Driver { get; set; }

        public MainCategoryPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this, new DefaultElementLocator(driver));
        }

        [FindsBy(How = How.XPath, Using = "/html/body/div/div/div/div[2]/div[2]/div[11]/div/div[1]/h4/a")]
        public IWebElement PrestigeSaddleLink { get; set; }

        public void NavigateToPrestigeSaddleDetails()
        {
            this.PrestigeSaddleLink.Click();
        }
    }
}
