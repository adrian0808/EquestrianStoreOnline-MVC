using Amazon.DynamoDBv2;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklep_internetowy.Tests.PageObject
{
    public class ProductDetailsPage
    {
        public IWebDriver Driver { get; set; }

        public ProductDetailsPage(IWebDriver driver)
        {
            this.Driver = driver;
            PageFactory.InitElements(this, new DefaultElementLocator(driver));
        }

        [FindsBy(How = How.Id, Using = "sizeId")]
        public IWebElement SizeDropDownlist { get; set; }

        [FindsBy(How = How.Id, Using = "colorId")]
        public IWebElement ColorDropDownlist { get; set; }

        [FindsBy(How = How.Id, Using = "addToShoppingCart")]
        public IWebElement AddToShoppingCartButton { get; set; }

        public void ChooseOptions()
        {
            var selectElement = new SelectElement(this.SizeDropDownlist);
            selectElement.SelectByText("M");
            selectElement = new SelectElement(this.ColorDropDownlist);
            selectElement.SelectByText("Bialy");
        }
   

        public void AddElementToShoppingCartAndNavigateToIt()
        {
            this.AddToShoppingCartButton.Click();
        }



    }
}
